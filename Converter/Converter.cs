/*
HtmlOsisConverter - Converts NeÜ Bible HTML files to OSIS XML.
Copyright (C) 2022 PhysXCoder

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation version 3 of the License.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using HtmlAgilityPack;
using NeueHtmlOsisConverter.Bible;
using NeueHtmlOsisConverter.Extensions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace NeueHtmlOsisConverter.Converter;

public class Converter : IConverter
{
    // Injected parameters/dependencies:
    public ICanon Canon { get; set; }
    public INamingScheme NamingScheme { get; set; }
    public IFilenames Filenames { get; set; }
    public string WorkName { get; set; }
    public string WorkTitle { get; set; }

    // Temporary variables (will change during conversion):
    protected BookInfo BookInfo;
    protected uint ChapterNumber = 0;
    protected uint? DeviatingChapterNumber = null;
    protected uint VerseNumber = 0;
    protected Book? ReferenceBook = null;
    protected string? ReferenceBookName = null;
    protected int NumberOfCharactersToIgnore = 0;
    protected bool StartedMajorSection = false;
    protected bool StartedSection = false;
    protected bool StartedSubSection = false;
    protected bool StartedFootnote = false;
    protected bool StartedParagraph = false;
    protected bool StartedChapter = false;
    protected bool StartedVerse = false;
    protected bool StartedIntroduction = false;
    protected ISet<HtmlNode> NodesAlreadyHandled = new HashSet<HtmlNode>();
    protected IDictionary<Verse, IList<HtmlNode>> FootnoteByVerse = new Dictionary<Verse, IList<HtmlNode>>();    
    
    protected OsisFormatter OsisFormatter;

    protected FileTextWriter FileWriter;
    protected StringBuilderWriter StringWriter;    

    // Constants / readonly    
    protected readonly char[] MultipleVerseReferencesSeparators = new char[] {';'};
    protected readonly char[] ChapterVerseSeparators = new char[] {':', ','};
    protected readonly char[] RangeSeparators = new char[] { '-', '–' };
    protected readonly char[] AdditionalVerseSeparators = new char[] { '.' };
    protected readonly char[] ValidReferenceCharacters = new char[] { '.', ':', ',', '-', ' ', '\t', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    protected readonly string[] HyperLinkTokens = new string[] { "http", "https", "ftp", "www", ".com", ".org", ".de", ".gif" };
    protected readonly string[] SelfReferenceBooknames = new string[] { "kapitel", "kap", "vers", "verse" };    
    protected readonly string[] IncludingFollowingVerseString =  new string[] { "ff", "ff.", "ff .", "f", "f.", "f ."};
    protected readonly string[] IncludingFollowingVersesString = new string[] { "ff", "ff.", "ff ."};

    protected const string FootnoteIndicator = "*";
    protected const string FootnoteReferenceSeparator = ":";
    protected const string TextToIgnore =  "&nbsp;";
    protected const string SectionTextToIgnore = "/^\\";

    protected const string HtmlClassVerse = "vers";
    protected const string HtmlClassChapter = "kap";
    protected const string HtmlClassFootnote = "fn";
    protected const string HtmlClassIntroduction = "e";

    public Converter(ICanon canon, IFilenames filenames, 
        INamingScheme namingScheme, string workTitle, string workName)
    {        
        Canon = canon;
        BookInfo = Canon.Books.First();
        Filenames = filenames;
        NamingScheme = namingScheme;
        WorkTitle = workTitle;
        WorkName = workName;    

        FileWriter = new FileTextWriter(TextWriter.Null);
        StringWriter = new StringBuilderWriter();
        OsisFormatter = new OsisFormatter(NamingScheme, FileWriter);

        ResetTempBookVar();  
    }

    public uint GetCurrentChapterNumber() =>
        DeviatingChapterNumber.HasValue? DeviatingChapterNumber.Value : ChapterNumber;    
    

    public void Convert(DirectoryInfo htmlFolder, FileInfo outputFile)
    {        
        OsisFormatter.Restart();
        OsisFormatter.NamingScheme = NamingScheme;

        var fsOptions = new FileStreamOptions() 
        {
            Access = FileAccess.ReadWrite,
            Mode = FileMode.Create,
            Share = FileShare.None
        };

        using (StringWriter = new StringBuilderWriter())
        using (var streamWriter = new StreamWriter(outputFile.FullName, System.Text.Encoding.UTF8, fsOptions))        
        {
            if (Debugger.IsAttached) 
            {
                streamWriter.AutoFlush = true;
            }
            
            using (FileWriter = new FileTextWriter(streamWriter)) 
            {
                OsisFormatter.Writer = FileWriter;

                OsisFormatter.WriteOsisBeginning(WorkName, WorkTitle);

                ConvertForeword(htmlFolder);

                foreach(var bookInfo in Canon.Books)
                { 
                    ResetTempBookVar();
                    ConvertBook(bookInfo, htmlFolder);
                }

                OsisFormatter.WriteOsisEnding();
                OsisFormatter.Writer = new FileTextWriter(TextWriter.Null);
            }
        }        
    }

    protected void ResetTempBookVar()
    {        
        BookInfo = Canon.Books.First();
        ChapterNumber = 0;
        DeviatingChapterNumber = null;
        VerseNumber = 0;
        NumberOfCharactersToIgnore = 0;

        StartedMajorSection = false;
        StartedSection = false;
        StartedSubSection = false;
        StartedFootnote = false;
        StartedParagraph = false;
        StartedChapter = false;
        StartedVerse = false; 
        StartedIntroduction = false;
        
        NodesAlreadyHandled.Clear();
    }

    protected string GetCurrentVerseAsString()
    {
        return $"Current verse: {BookInfo.BookNames.First()}, Chapter={GetCurrentChapterNumber()} Verse={VerseNumber}";
    }

    protected string GetCurrentBookAsString()
    {
        return $"Current book: {BookInfo.BookNames.First()}";
    }

    protected void ConvertForeword(DirectoryInfo htmlFolder)
    {                
        var forewordFilename = htmlFolder.FullName + Path.DirectorySeparatorChar + Filenames.ForewordFilename;
        var htmlDoc = new HtmlDocument();
        htmlDoc.Load(forewordFilename);

        var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//h2");
        var titleStr = titleNode.InnerText;
        var mainNode = titleNode.ParentNode;
        var nodesList = mainNode.ChildNodes.ToList();
        int iTitle;
        for (iTitle=0; iTitle<nodesList.Count; iTitle++)
        {
            if (nodesList[iTitle] == titleNode) 
            {
                break;
            }
        }

        StartIntroduction();
        StartMajorSection(titleStr);

        for (int iNode=iTitle+1; iNode<nodesList.Count; iNode++)
        {
            var currentNode= nodesList[iNode];
            var end = ConvertNode(currentNode, true, false);
            if (end) break;
        }

        EndSectionsChapterVerse();        
    }

    protected void EndSectionsChapterVerse()
    {
        if (StartedVerse)
        {
            EndVerse();            
        }
        if (StartedChapter)
        {
            EndChapter();            
        }
        if (StartedParagraph)
        {
            EndParagraph();
        }
        if (StartedFootnote)
        {
            EndFootnote();
        }
        if (StartedSubSection)
        {
            EndSubSection();
        }
        if (StartedSection)
        {
            EndSection();
        }
        if (StartedMajorSection)
        {
            EndMajorSection();            
        }
        if (StartedIntroduction)
        {
            EndIntroduction();            
        }
    }

    protected bool IsSuccessorNodeVerseOrChapter(HtmlNode currentNode, bool onlyChapter = false) => 
        GetSuccessorNodeIfVerseOrChapter(currentNode, onlyChapter) != null;

    protected HtmlNode? GetSuccessorNodeIfVerseOrChapter(HtmlNode currentNode, bool onlyChapter = false)
    {
        var nextNode = currentNode.NextSibling;
        var nodesToCheck = new List<HtmlNode>();
        nodesToCheck.Add(nextNode);

        while (nodesToCheck.Any())
        {
            var node = nodesToCheck.RemoveFirst();
            if (node == null) break;          

            if (node.Name.ToLower() == "span" && node.HasClass(HtmlClassVerse) && !onlyChapter)
            {
                // Another verse will be started
                return node;
            }
            else if (node.Name.ToLower() == "span" && node.HasClass(HtmlClassChapter))
            {
                // Another chapter will be started.

                // Before returning it, check if it would start the same chapter already started. (necessary for 1Chr 22,1)
                uint nextChapter;
                if (onlyChapter && uint.TryParse(node.InnerText, out nextChapter))
                {
                    if (nextChapter == ChapterNumber)
                    {
                        // It is already started!
                        return null;
                    }
                }

                return node;
            }
            else if(node.Name.ToLower() == "div" && node.HasClass(HtmlClassFootnote))
            {
                // Skip footnotes, also skipping its children
                nodesToCheck.AddUnique(node.NextSibling);
            }
            else if ((node.Name.ToLower().StartsWith('h') && node.Name.Length == 2)
                || (node.Name.ToLower() == "span" && node.HasClass("u2")))
            {
                // Skip headers, also skipping its children
                nodesToCheck.AddUnique(node.NextSibling);
            }
            else if (node.Name.ToLower() == "#text")
            {
                // Skip "\r\n" lines. But not rergular text.
                if (string.IsNullOrWhiteSpace(node.InnerText))  
                {
                    nodesToCheck.PrependRangeUnique(node.ChildNodes);   // Should be empty, just in case ...
                    nodesToCheck.AddUnique(node.NextSibling);
                }
                else 
                {
                    // We have regular text for the current verse -> don't start the next verse yet
                    return null;
                }
            }
            else
            {
                nodesToCheck.PrependRangeUnique(node.ChildNodes);
                nodesToCheck.AddUnique(node.NextSibling);
            }
        }

        return null;
    }

    protected Verse CurrentVerse() => 
        new Verse(BookInfo.Book, GetCurrentChapterNumber(), VerseNumber);

    protected bool ConvertNode(HtmlNode currentNode, bool foreword, bool emphasis)
    {
        if (NodesAlreadyHandled.Contains(currentNode))
        {
            return false;
        }

        switch (currentNode.Name.ToLower())
        {
            case "p":
                if (currentNode.HasClass("u1"))
                {
                    // Special case for sort-of-summary-sentence after introduction of a book. Process as text, but in italic.
                    OsisFormatter
                        .StartParagraph()
                        .Line(OsisTextStyle.Italic, currentNode.InnerText)
                        .EndParagraph();
                    return false;
                }
                if (IsOnlyEmptyChildren(currentNode)) break;                
                
                if (!StartedParagraph)
                {
                    StartParagraph();
                }
                foreach(var childNode in currentNode.ChildNodes)
                {                    
                    var end = ConvertNode(childNode, foreword, emphasis);
                    if (end) return end;
                }
                if (StartedParagraph) OsisFormatter.EndLine();                
                if (StartedVerse && IsSuccessorNodeVerseOrChapter(currentNode))
                {
                    EndVerse();
                }
                if (StartedChapter && IsSuccessorNodeVerseOrChapter(currentNode, true))
                {
                    EndChapter();
                }
                if (StartedParagraph)
                {
                    EndParagraph();
                }
                break;
                        
            case "#text":
            case "em":
                var localEmphasis = currentNode.Name.ToLower() == "em";
                if (currentNode.HasChildNodes && currentNode.ChildNodes.Count > 1)
                {
                    foreach (var childNode in currentNode.ChildNodes)
                    {
                        var end = ConvertNode(childNode, foreword, emphasis || localEmphasis);
                        if (end) return end;
                    }
                    break;
                }

                var text = currentNode.InnerText;
                if (NumberOfCharactersToIgnore > 0)
                {
                    var toIgnore = Math.Min(NumberOfCharactersToIgnore, text.Length);
                    text = text.Substring(toIgnore);
                    NumberOfCharactersToIgnore -= toIgnore;
                }
                text = text.Replace(TextToIgnore, " ");

                if (OsisFormatter.LastWriteEndedWithNewline)                
                {
                    text = text.TrimStart();
                }

                if (!string.IsNullOrWhiteSpace(text)) 
                {
                    // Check for footnotes and convert them
                    int iFootnote = -1;                    
                    while ((iFootnote = text.IndexOf(FootnoteIndicator)) >= 0 && !StartedFootnote)  // Nested footnotes are not supported
                    {
                        // Output text until the footnote indicator
                        OsisFormatter.Text(text.Substring(0, iFootnote));
                        text = text.Substring(iFootnote + FootnoteIndicator.Length);    // Remove footnote indicator

                        // Get footnote HTML node
                        var verse = CurrentVerse();
                        if (!FootnoteByVerse.ContainsKey(verse) || !FootnoteByVerse[verse].Any()) 
                        {
                            throw new FormatException("Footnote missing! " + GetCurrentVerseAsString());
                        }
                        var footnote = FootnoteByVerse[verse].RemoveFirst();

                        // Output footnote
                        ConvertFootnote(footnote);
                    }

                    // Output the (remaining) text
                    var style = localEmphasis? OsisTextStyle.Bold : OsisTextStyle.None;
                    OsisFormatter.Text(style, text);
                }
                break;

            case "div":
                if (currentNode.HasClass(HtmlClassIntroduction))
                {
                    if (!StartedIntroduction)
                    {
                        StartIntroduction();                        
                    }
                    StartParagraph();
                    foreach(var childNode in currentNode.ChildNodes)
                    {
                        ConvertNode(childNode, foreword, emphasis);
                    }
                    EndParagraph();
                }
                else if (foreword)
                {
                    StartParagraph();
                    foreach(var childNode in currentNode.ChildNodes)
                    {
                        ConvertNode(childNode, foreword, emphasis);
                    }                    
                    OsisFormatter.EndLine();
                    EndParagraph();                    
                }
                else if (currentNode.HasClass(HtmlClassFootnote))
                {
                    // Ignore footnotes here. Instead, insert them where marked with '*'.
                }
                else
                {
                    goto default;
                }
                break;

            case "b":
                var bold = currentNode.InnerText;
                OsisFormatter.Text(OsisTextStyle.Bold, bold);
                break;

            case "i":
                var italics = currentNode.InnerText;
                OsisFormatter.Text(OsisTextStyle.Italic, italics);
                break;

            case "h2":
            case "h3":
            case "h4":
                if (StartedIntroduction)
                {
                    EndSectionsChapterVerse();                    
                }
                switch (foreword)
                {
                    case true:
                        if (StartedSection)
                        {
                            EndSection();
                        }
                        StartSection();
                        break;

                    case false:                        
                        switch (currentNode.Name.ToLower())
                        {
                            case "h2":                              
                                if (StartedParagraph)
                                {
                                    EndParagraph();
                                }
                                if (StartedSubSection)
                                {
                                    EndSubSection();
                                }
                                if (StartedSection)
                                {
                                    EndSection();
                                }
                                if (StartedMajorSection)
                                {
                                    EndMajorSection();
                                }

                                // Check if next node is chapter. Often, a chapter has a (sub)section as title. But in HTML file, the title comes before the chapter number.
                                // If yes, first start the chapter. Sequence must be switched, looks better in Bible readers (because chapters start their own page).
                                var nextChapterNode2 = GetSuccessorNodeIfVerseOrChapter(currentNode, true);
                                if (nextChapterNode2 != null)
                                {
                                    ConvertNode(nextChapterNode2, foreword, emphasis);
                                    NodesAlreadyHandled.Add(nextChapterNode2);
                                }

                                StartMajorSection();
                                break;

                            case "h3":
                                if (StartedParagraph)
                                {
                                    EndParagraph();
                                }
                                if (StartedSubSection)
                                {
                                    EndSubSection();
                                }
                                if (StartedSection)
                                {
                                    EndSection();
                                }

                                // Check if next node is chapter. Often, a chapter has a (sub)section as title. But in HTML file, the title comes before the chapter number.
                                // If yes, first start the chapter. Sequence must be switched, looks better in Bible readers (because chapters start their own page).
                                var nextChapterNode3 = GetSuccessorNodeIfVerseOrChapter(currentNode, true);
                                if (nextChapterNode3 != null)
                                {
                                    ConvertNode(nextChapterNode3, foreword, emphasis);
                                    NodesAlreadyHandled.Add(nextChapterNode3);
                                }

                                StartSection();
                                break;

                            case "h4":
                            case "span":
                                if (StartedParagraph)
                                {
                                    EndParagraph();
                                }
                                if (StartedSubSection)
                                {
                                    EndSubSection();
                                }

                                // Check if next node is chapter. Often, a chapter has a (sub)section as title. But in HTML file, the title comes before the chapter number.
                                // If yes, first start the chapter. Sequence must be switched, looks better in Bible readers (because chapters start their own page).
                                var nextChapterNode4 = GetSuccessorNodeIfVerseOrChapter(currentNode, true);
                                if (nextChapterNode4 != null)
                                {
                                    ConvertNode(nextChapterNode4, foreword, emphasis);
                                    NodesAlreadyHandled.Add(nextChapterNode4);
                                }

                                StartSubSection();
                                break;
                        }                        
                        break;
                }
                
                var sectionTitle = string.Empty;
                if (currentNode.HasChildNodes)
                {
                    foreach (var childNode in currentNode.ChildNodes)
                    {
                        if (childNode.Name.ToLower() == "#text" || childNode.Name.ToLower() == "a")     // The latter is necesarry for Psalm 119s title "I Alef*", the star is in an anchor element.
                        {
                            sectionTitle += childNode.InnerText;
                        }
                    }
                }
                else
                {
                    sectionTitle = currentNode.InnerText;
                }

                // Remove "/^\" (is used in foreword)
                sectionTitle = sectionTitle.TrimStart();
                if (sectionTitle.StartsWith(SectionTextToIgnore)) 
                {
                    sectionTitle = sectionTitle.Substring(SectionTextToIgnore.Count());
                }

                // In seldom cases (e.g. Psalm 119) a subsection title can contain footnotes.                
                var title = sectionTitle.TrimStart();
                int iFootnoteTitle = title.IndexOf(FootnoteIndicator);
                if (iFootnoteTitle < 0)
                {
                    // Usual case: No tootnote in title
                    OsisFormatter.Title(title.TrimStart());
                }           
                else 
                {
                    // Title contais footnote
                    OsisFormatter.StartTitle();
                    while (iFootnoteTitle >= 0 && !StartedFootnote)  // Nested footnotes are not supported)
                    {
                        // Output text until the footnote indicator
                        OsisFormatter.Text(title.Substring(0, iFootnoteTitle));
                        title = title.Substring(iFootnoteTitle + FootnoteIndicator.Length);    // Remove footnote indicator
                        // Get footnote HTML node
                        var verse = CurrentVerse();                        
                        if ((!FootnoteByVerse.ContainsKey(verse) || !FootnoteByVerse[verse].Any()) && verse.VerseNumber == 0) 
                        { 
                            // Fallback: Try to take footnote of first verse (necessary for Psalm 119: "I Alef*" is a subsection title not yet in verse 1, but the footnote refers to verse 1)
                            verse.VerseNumber = 1;
                        }
                        if ((!FootnoteByVerse.ContainsKey(verse) || !FootnoteByVerse[verse].Any())) 
                        { 
                            // Fallback: Try to take footnote for verse 0 (title) (necessary for Proverbs subsection between 31:9 and 31:10)
                            verse.VerseNumber = 0;
                        }
                        if ((!FootnoteByVerse.ContainsKey(verse) || !FootnoteByVerse[verse].Any())) 
                        { 
                            // Fallback: Try to take footnote for next chapter (necessary for Lamentations 3:66 to 4:1)
                            ++verse.ChapterNumber;
                            verse.VerseNumber = 0;
                        }
                        if (!FootnoteByVerse.ContainsKey(verse) || !FootnoteByVerse[verse].Any()) 
                        { 
                            throw new FormatException("Footnote missing! " + GetCurrentVerseAsString());
                        }
                        var footnote = FootnoteByVerse[verse].RemoveFirst();
                        // Output footnote
                        ConvertFootnote(footnote);

                        iFootnoteTitle = title.IndexOf(FootnoteIndicator);
                    }
                    // Output the (remaining) text
                    OsisFormatter.Line(title);
                    OsisFormatter.EndTitle();
                }
                break;

            case "a":
                var refText = currentNode.InnerText;
                var uriText = refText;
                if (currentNode.Attributes.Contains("href"))
                {
                    uriText = currentNode.Attributes["href"].Value;
                }
                if (!IsHyperLink(uriText))
                {
                    if (!uriText.ToLower().Contains("index.htm")
                        && !uriText.ToLower().Trim().StartsWith("#top"))
                    {
                        string? successorText = null;
                        if (currentNode.NextSibling?.Name.ToLower() == "#text"
                            && currentNode.NextSibling.ParentNode == currentNode.ParentNode)
                        {
                            successorText = currentNode.NextSibling.InnerText;
                        }
                        ConvertReference(refText, successorText);
                    }
                    else
                    {
                        foreach (var childNodes in currentNode.ChildNodes)
                        {
                            var end = ConvertNode(childNodes, foreword, emphasis);
                            if (end) break;
                        }
                    }
                }
                else
                {
                    if (!uriText.ToLower().Contains("www"))
                    {
                        System.Console.Out.WriteLine($"Warning: Ignoring Hyperlink to '{uriText}'! " + GetCurrentVerseAsString());
                        var linkText = currentNode.InnerText.Trim();
                        if (!string.IsNullOrEmpty(linkText))
                        {
                            OsisFormatter.Text(linkText);
                        }
                    }
                    else
                    {
                        OsisFormatter.HyperLink(refText, new Uri(uriText));
                    }
                }
                break;

            case "span":
                if (currentNode.HasClass(HtmlClassChapter))
                {
                    uint chapterNumber;
                    if (!uint.TryParse(currentNode.InnerText, out chapterNumber)) 
                    {
                        throw new FormatException($"Invalid chapter number in node '{currentNode.InnerHtml}'! " + GetCurrentVerseAsString());
                    }
                    if (!StartedChapter || chapterNumber != GetCurrentChapterNumber())  // Only start chapter if it isn't already started
                    {
                        if (StartedChapter)
                        {
                            EndChapter();
                        }
                        StartChapter(chapterNumber);
                    }
                }
                else if (currentNode.HasClass(HtmlClassVerse))
                {
                    // Usually just the verse number is given, e.g. "3".
                    // Sometimes, at the beginning of a chapter there's a verse of the last chapter, e.g. Num 25:19 is contained in chapter 26.
                    // Sometimes, at the end of a chapter there's a verse of the next chapter, e.g. 1 Chr 22:1 is in chapter 21.
                    // Sometimes, it's contained in paranthesis e.g. (12,18) in Revelations
                    var verseText = currentNode.InnerText.Trim();
                    if (verseText.Contains('(') && verseText.Contains(')'))
                    {
                        if (verseText.Count(c => c == '(') != verseText.Count(c => c == ')'))
                        {
                            throw new FormatException($"Uneven number of opening and closing paranthesis in reference '{verseText}'! " + GetCurrentVerseAsString());
                        }
                        verseText = verseText.Replace("(", "").Replace(")", "").Trim();
                    }
                    int iChapterVerseSeparator = verseText.IndexOfAny(ChapterVerseSeparators);
                    uint verseNumber;
                    if (iChapterVerseSeparator < 0)
                    {
                        // Just single verse number given. Parse it and start the verse.                        
                        if (!uint.TryParse(verseText, out verseNumber)) 
                        {
                            throw new FormatException($"Invalid verse number in node '{verseText}'! " + GetCurrentVerseAsString());
                        }

                        if (StartedVerse)
                        {
                            EndVerse();
                        }                    
                        StartVerse(verseNumber);
                    }
                    else
                    {
                        // Verse number and chapter number explicitly given.                        
                        var chapterNumberString = verseText.Substring(0, iChapterVerseSeparator);
                        var verseNumberString = verseText.Substring(iChapterVerseSeparator+1);
                        if (!uint.TryParse(chapterNumberString, out uint chapterNumber)) 
                        {
                            throw new FormatException($"Invalid chapter number in node '{verseText}'! " + GetCurrentVerseAsString());
                        }
                        if (!uint.TryParse(verseNumberString, out verseNumber)) 
                        {
                            throw new FormatException($"Invalid verse number in node '{verseText}'! " + GetCurrentVerseAsString());
                        }
                        if (StartedVerse)
                        {
                            EndVerse();
                        }
                        StartVerse(verseNumber, chapterNumber);
                    }
                }
                else if (currentNode.HasClass("u2"))
                {
                    goto case "h4";
                }
                else
                {
                    throw new NotImplementedException($"Unknown span class in node '{currentNode.InnerHtml}'! " + GetCurrentVerseAsString());
                }
                break;

        case "img":
            if (!currentNode.Attributes.Contains("src")) 
            { 
                throw new FormatException($"Unkown image without source in '{currentNode.InnerHtml}'! " + GetCurrentVerseAsString());
            }
            var source = currentNode.Attributes["src"].Value;
            switch (source)
            {
                case "note.png":
                    // Selah
                    OsisFormatter.Text("♪");       // U266A        Enter in VSCode on Linux: press and release Return, Shift (upper arrow) and U together, then enter 2 6 6 A, then press Return
                    break;

                default:
                    throw new NotImplementedException($"Unkown image src '{source}' in '{currentNode.InnerHtml}'! " + GetCurrentVerseAsString());
            }
            
            break;

        case "hr":
            // Terminate conversion of this file
            return true;

        case "ul":
            // Ignore
            break;

        case "br":
            OsisFormatter.LineBreak();
            break;

         default:
            System.Console.Out.WriteLine($"Warning: Unrecognized HTML element '{currentNode.Name}'! " + GetCurrentBookAsString());            
            break;
        }

        return false;
    }

    protected void ConvertReference(string refText, string? successorText)
    {
        string text = refText;

        // Take beginning of successor text (if it exists) and add it to the text that shall be parsed as a reference. E.g. "<a href=...>Lk 1</a>,3 ..." not only "Lk 1" shall be parsed but "Lk 1,3"
        if (!string.IsNullOrWhiteSpace(successorText))
        {
            // Only take valid reference characters
            int iSuccessorTextStart = successorText.IndexOfFirstInvalid(ValidReferenceCharacters);
            if (iSuccessorTextStart == -1) 
            {
                iSuccessorTextStart = successorText.Length; // If there are only valid characters in the string, take the whole string
            }
            if (iSuccessorTextStart > 0)
            {
                string successorReferencePart = successorText.Substring(0, iSuccessorTextStart).TrimEnd();

                // Sometimes the successor reference part ends with a character that could be part of a reference, but is part of the footnote prose.
                // E.g. "<a href=...>Esra 6</a>,10, offenbar auch Ataxerxes." Only "Esra 6,10" shall be parsed, not "Esra 6,10,".
                int iSuccessorReferencePartEnd = successorReferencePart.IndexOfLastDigit();
                if (iSuccessorReferencePartEnd >= 0)
                {
                    successorReferencePart = successorReferencePart.Substring(0, iSuccessorReferencePartEnd+1);

                    if (!string.IsNullOrWhiteSpace(successorReferencePart))
                    {                        
                        text += successorReferencePart;
                        NumberOfCharactersToIgnore = successorReferencePart.Length;
                    }
                }
            }
        }

        var references = ParseReference(text);

        foreach(var reference in references)
        {
            OsisFormatter.Reference(reference);
        }
    }

    protected IList<VerseReference> ParseReference(string refText)
    {
        // Check if parsing none or multiple references:
        var references = refText.Trim().Split(MultipleVerseReferencesSeparators, StringSplitOptions.RemoveEmptyEntries);
        if (references.Length < 1) 
        {
            return new List<VerseReference>();
        }
        if (references.Length > 1) 
        {
            return references.SelectMany(reference => ParseReference(reference)).ToList();
        }
        var reference = references[0].Trim();
        if (string.IsNullOrWhiteSpace(reference)) return new List<VerseReference>();

        // Parse excactly 1 reference.
        // German Bible references: "Mt 1,2" "Mt 1,2-4" "Mt 1,2.3" "Mt 1,2 f." "Mt 1,2 ff." "Mt 1,2;4,3" "31,18ff"
        // English references:      "Mt 1:2" "Mt 1:2-4"

        // First, split between book name and the rest. Then check if whe have only a book name.
        // But if a chapter/verse separator is found, at the max until the separator (e.g. "Jesaja 40,13f")
        int iFirstChapterVerseSeparator = reference.IndexOfAny(ChapterVerseSeparators);
        int iLastBookLetter = -1;
        if (iFirstChapterVerseSeparator > 0)  
        {
            iLastBookLetter = reference.Substring(0, iFirstChapterVerseSeparator-1).IndexOfLastLetter();
        }
        else
        {
            iLastBookLetter = reference.IndexOfLastLetter();
        }
        var afterLastBookLetter = reference.Substring(iLastBookLetter+1);
        if (string.IsNullOrWhiteSpace(afterLastBookLetter))
        {
            // No chapter or verse number -> complete book is referenced
            ParseReferenceBookText(reference);
            return new List<VerseReference>() 
            {
                new SingleVerseReference(new Verse((Book)ReferenceBook, 0, 0), reference)
            };
        }

        // There is a chapter number. First get the book name (maybe empty string).
        var referenceBookText = reference.Substring(0, iLastBookLetter+1).Trim();
        var rest = reference.Substring(iLastBookLetter+1).Trim();
        if (SelfReferenceBooknames.Contains(referenceBookText.ToLower()))
        {
            // self-reference
            referenceBookText = BookInfo.BookNames.First();
        }
        ParseReferenceBookText(referenceBookText);

        // Then parse the rest.
        // The rest could contain ranges, e.g. "Mt 1,2-4" or "Mk 1,1-3,2", or "Lk 1-3". Check for ranges or chapter-verse-separators first.        
        // Special case: The rest might start with '.' due to "Kap." being a self-referential book name. Remove it. (Necessary for John 1:28)
        while (rest[0] == '.') rest = rest.Substring(1);
        var iFirstSeparator = rest.IndexOfAny(ChapterVerseSeparators.Union(RangeSeparators).ToArray());
        if (iFirstSeparator < 0)
        {
            // Only 1 chapter, e.g. "Lk 13"
            uint chapter;
            if (!uint.TryParse(rest, out chapter)) throw new FormatException($"Invalid chapter number in reference '{reference}'! " + GetCurrentVerseAsString());
            return new List<VerseReference>() {
                new SingleVerseReference(new Verse((Book)ReferenceBook, chapter, 0), reference)
            };
        }
        else if (RangeSeparators.Contains(rest[iFirstSeparator]))
        {
            // Range of chapters, e.g. "Lk 1-3"
            var chapters = rest.Split(RangeSeparators, 2, StringSplitOptions.RemoveEmptyEntries);
            if (chapters.Length != 2) throw new FormatException($"Invalid chapter range specified in reference '{reference}'! " + GetCurrentVerseAsString());
            uint chapterStart, chapterEnd;
            if (!uint.TryParse(chapters[0], out chapterStart)) throw new FormatException($"Invalid chapter start number in reference '{reference}'! " + GetCurrentVerseAsString());
            if (!uint.TryParse(chapters[1], out chapterEnd)) throw new FormatException($"Invalid chapter end number in reference '{reference}'! " + GetCurrentVerseAsString());
            return new List<VerseReference>() {
                new RangeVerseReference(
                    new Verse((Book)ReferenceBook, chapterStart, 0), 
                    new Verse((Book)ReferenceBook, chapterEnd, 0), 
                    reference)
            };
        }
        else
        {
            // Chapter and verse separator ',' found, e.g. in "Mt 1,2", "Mk 1,1-3,2", or "Lk 1,8.9.10"
            // First get the chapter
            uint chapter;
            var chapterString = rest.Substring(0, iFirstSeparator);
            if (!uint.TryParse(chapterString, out chapter)) throw new FormatException($"Invalid chapter number in reference '{reference}'! " + GetCurrentVerseAsString());
            
            // Then check the rest after the separator - is it a single verse, or verse range, or multiple verses?
            var restAfterSeparator = rest.Substring(iFirstSeparator+1).Trim();
            int iRangeSeparator = restAfterSeparator.IndexOfAny(RangeSeparators);
            int iAdditionalVerseSeparator = restAfterSeparator.IndexOfAny(AdditionalVerseSeparators);
            if (iRangeSeparator < 0 && iAdditionalVerseSeparator < 0)
            {
                // Just a single verse number, e.g. "Mt 1,2" or "Jesaja 40,13f" (restAfterSeparator is "2" or "13f")
                // Check for "f" or similar indicating that the following verse(s) shall be included
                bool includeFollowing = false;
                bool includeMultiple = false;
                foreach(var inclusionString in IncludingFollowingVerseString)
                {
                    if (restAfterSeparator.EndsWith(inclusionString))
                    {
                        includeFollowing = true;
                        includeMultiple = IncludingFollowingVersesString.Contains(inclusionString);
                        restAfterSeparator = restAfterSeparator
                            .Remove(restAfterSeparator.Length - inclusionString.Length, inclusionString.Length)
                            .Trim();
                        break;
                    }
                }
                uint verse;
                if (!uint.TryParse(restAfterSeparator, out verse)) throw new FormatException($"Invalid verse number in reference '{reference}'! " + GetCurrentVerseAsString());
                var targetVerseRef = new SingleVerseReference(new Verse((Book)ReferenceBook, chapter, verse), reference);
                if (!includeFollowing)
                {
                    return new List<VerseReference>() 
                    {
                        targetVerseRef
                    };
                } 
                else
                {
                    var nextVerse = Canon.GetSuccessorVerse(targetVerseRef.Verse);                    
                    // For "ff." include the next verse, too:
                    if (includeMultiple)
                    {
                        nextVerse = Canon.GetSuccessorVerse(nextVerse);
                    }
                    return new List<VerseReference>()
                    {
                        new RangeVerseReference(targetVerseRef.Verse, nextVerse, reference)
                    };
                }
            }
            else if (iRangeSeparator >= 0 && iAdditionalVerseSeparator >= 0)
            {
                // Not supported, e.g. "Mt 1,2-3.8" or "2. Mose 3,8.13-15"
                // First, split by '.', then check each item for range  '-' and calculate the range numbers itself.
                // At the end, create reference for each line.
                var verseNumbers = new List<uint>();
                var items = restAfterSeparator.Split(AdditionalVerseSeparators, StringSplitOptions.RemoveEmptyEntries);                
                foreach (var item in items)
                {
                    int iInnerRangeSeparator = item.IndexOfAny(RangeSeparators);
                    if (iInnerRangeSeparator < 0)
                    {
                        uint verseNumber;
                        if (!uint.TryParse(item, out verseNumber)) throw new FormatException($"Invalid verse number '{item}' in reference '{reference}'! " + GetCurrentVerseAsString());
                        verseNumbers.AddUnique(verseNumber);
                    }
                    else
                    {
                        uint verseNumberStart, verseNumberEnd;
                        var numberStartString = item.Substring(0, iInnerRangeSeparator);
                        var numberEndString = item.Substring(iInnerRangeSeparator+1);                        
                        if (!uint.TryParse(numberStartString, out verseNumberStart)) throw new FormatException($"Invalid start verse number '{numberStartString}' in reference '{reference}'! " + GetCurrentVerseAsString());
                        if (!uint.TryParse(numberEndString, out verseNumberEnd)) throw new FormatException($"Invalid end verse number '{numberEndString}' in reference '{reference}'! " + GetCurrentVerseAsString());
                        for (uint verseNumber=verseNumberStart; verseNumber<=verseNumberEnd; verseNumber++)                        
                        {
                            verseNumbers.AddUnique(verseNumber);
                        }
                    }
                }
                verseNumbers.Sort();
                var verses = verseNumbers.Select(vn => 
                    new Verse((Book)ReferenceBook, chapter, vn));
                return new List<VerseReference>() { new MultipleVerseReference(verses, reference) };
            }
            else if (iRangeSeparator >= 0)
            {
                // Verse range, e.g. "2-3" in "Acts 1,2-3", or "1-3,2" in "Mk 1,1-3,2"
                // First, get the start verse number
                uint verseStart;
                var verseStartString = restAfterSeparator.Substring(0, iRangeSeparator);
                if (!uint.TryParse(verseStartString, out verseStart)) throw new FormatException($"Invalid start verse number in verse range. Reference: '{reference}'! " + GetCurrentVerseAsString());

                // Then, check for another chapter-verse-separator in the rest after the range separator
                var afterRangeString = restAfterSeparator.Substring(iRangeSeparator+1).Trim();
                int iChapterVerseSeparator = afterRangeString.IndexOfAny(ChapterVerseSeparators);
                if (iChapterVerseSeparator < 0)
                {
                    // No chapter-verse-separator, so we have just a verse range, e.g. "2-3" in "Acts 1,2-3"
                    uint verseEnd;                    
                    if (!uint.TryParse(afterRangeString, out verseEnd)) throw new FormatException($"Invalid end verse number in verse range. Reference: '{reference}' " + GetCurrentVerseAsString());
                    return new List<VerseReference>() {
                        new RangeVerseReference(
                            new Verse((Book)ReferenceBook, chapter, verseStart), 
                            new Verse((Book)ReferenceBook, chapter, verseEnd), 
                            reference)
                    };
                }
                else
                {
                    // End verse is in another chapter/verse, e.g. "3,2" in "Mk 1,1-3,2"
                    var chapterEndString = afterRangeString.Substring(0, iChapterVerseSeparator);
                    var verseEndString = afterRangeString.Substring(iChapterVerseSeparator+1);
                    uint chapterEnd, verseEnd;
                    if (!uint.TryParse(chapterEndString, out chapterEnd)) throw new FormatException($"Invalid end chapter number in verse range. Reference: '{reference}'! " + GetCurrentVerseAsString());
                    if (!uint.TryParse(verseEndString, out verseEnd)) throw new FormatException($"Invalid end verse number in verse range. Reference: '{reference}'! " + GetCurrentVerseAsString());
                    return new List<VerseReference>() {
                        new RangeVerseReference(
                            new Verse((Book)ReferenceBook, chapter, verseStart),
                            new Verse((Book)ReferenceBook, chapterEnd, verseEnd), 
                            reference)
                    };
                }
            }
            else
            {
                // Additional verses, e.g. "Lk 1,8.9.10"
                var verseNumberStrings = restAfterSeparator.Split(AdditionalVerseSeparators, StringSplitOptions.RemoveEmptyEntries);
                var verseNumbers = verseNumberStrings.Select(str => 
                {
                    uint verse;
                    if (!uint.TryParse(str, out verse)) throw new FormatException($"Invalid verse number in multiple verse numbers. Reference: '{reference}'! " + GetCurrentVerseAsString());
                    return new Verse((Book)ReferenceBook, chapter, verse);
                }).ToList();                
                return new List<VerseReference>() { new MultipleVerseReference(verseNumbers, reference) };
            }
        }
    }

    [MemberNotNull(nameof(ReferenceBook))]
    [MemberNotNull(nameof(ReferenceBookName))]
    protected void ParseReferenceBookText(string referenceBookText)
    {
        if (string.IsNullOrWhiteSpace(referenceBookText))
        {
            if (ReferenceBook == null || string.IsNullOrWhiteSpace(ReferenceBookName)) 
            {
                // No book given -> reference is to a verse in the current book.
                ReferenceBook = BookInfo.Book;
                ReferenceBookName = BookInfo.BookNames.First();
            }
            else
            {
                // No book given -> reference is for the previous book. Usefol for "Mt 1,2;4,3" and likewise references.            
            }
        } 
        else 
        {
            var match = Canon.Books.FirstOrDefault(bookInfo => 
                bookInfo.BookNames.Any(name => 
                    name.ToUpper() == referenceBookText.ToUpper())
                )?.Book;
            if (match == null) 
                throw new FormatException($"Unkown book name '{referenceBookText}'! " + GetCurrentVerseAsString());

            ReferenceBook = (Book)match;
            ReferenceBookName = (string)referenceBookText;
        }
    }

    protected void ClearReferenceBookCache()
    {
        ReferenceBook = null;
        ReferenceBookName = null;
    }

    protected void ConvertBook(BookInfo bookInfo, DirectoryInfo htmlFolder)
    {
        BookInfo = bookInfo;

        var fileName = htmlFolder.FullName + Path.DirectorySeparatorChar + Filenames.FilenameByBook[bookInfo.Book];
        var htmlDoc = new HtmlDocument();
        htmlDoc.Load(fileName);

        StartBook(bookInfo);

        var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='u0']");
        var titleStr = titleNode.InnerText;
        var mainNode = titleNode.ParentNode;
        var nodesList = mainNode.ChildNodes.ToList();
        // Advance to title node
        int iTitle;
        for (iTitle=0; iTitle<nodesList.Count; iTitle++)
        {
            if (nodesList[iTitle] == titleNode) 
            {
                break;
            }
        }
        OsisFormatter.Title(titleStr);
        
        PreScanFootnotes(nodesList, iTitle+1);

        for (var iNode=iTitle+1; iNode<nodesList.Count; iNode++)
        {
            var currentNode = nodesList[iNode];
            var end = ConvertNode(currentNode, false, false);
            if (end) break;
        }

        CheckAllFootnotesUsed();

        EndSectionsChapterVerse();
        EndBook();
    }

    protected void PreScanFootnotes(IList<HtmlNode> nodesList, int iStart)
    {
        FootnoteByVerse.Clear();
        for (int i=iStart; i<nodesList.Count; i++)
        {
            var curNode = nodesList[i];
            try
            {                
                TryScanFootnote(curNode);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while trying to prescan footnotes in node '{curNode.InnerHtml}'!", ex);
            }
        }
    }

    protected void TryScanFootnote(HtmlNode node)
    {
        if (node.Name.ToLower() == "div" && node.HasClass(HtmlClassFootnote))
        {
            var childNodes = node.ChildNodes.ToList();

            // First node contains the verse reference
            string referenceWithBooknameText;
            if (!childNodes.Any()) throw new FormatException($"Empty footnote in '{node.InnerHtml}'! " + GetCurrentBookAsString());
            var firstNode = childNodes.RemoveFirst();
            if (firstNode.Name == "#text")
            {
                // Prepend book name to reference text
                var firstNodeText = firstNode.InnerText.Trim();
                var iReferenceEnd = firstNodeText.IndexOf(FootnoteReferenceSeparator);
                if (iReferenceEnd < 0) throw new FormatException($"Unknown footnote reference format '{firstNode.InnerHtml}'! " + GetCurrentBookAsString());
                var referenceText = firstNodeText.Substring(0, iReferenceEnd);

                // Check if bookname is already prepended (e.g. "Psalm 119")
                var iFirstDigit = referenceText.IndexOfFirstDigit();
                var textBeforeDigits = referenceText.Substring(0, iFirstDigit).Trim();
                if (!string.IsNullOrWhiteSpace(textBeforeDigits) 
                    && Canon.Books.Any(book => book.BookNames.Contains(textBeforeDigits)))
                {
                    // Yes, it's already a book name. Just take the string as reference.
                    referenceWithBooknameText = referenceText;
                }
                else 
                {
                    // Prepend book name.
                    referenceWithBooknameText = $"{BookInfo.BookNames.First()} {referenceText}";
                }
            }
            else if (firstNode.Name == "a")
            {
                // E.g. '<a href="#119">Psalm 119</a>' -> Book name is already prepended
                referenceWithBooknameText = firstNode.InnerText;
            }
            else
            {
                throw new FormatException($"Invalid footnote reference format '{firstNode.InnerHtml}'! " + GetCurrentBookAsString());
            }

            // Parse the verse reference
            var references = ParseReference(referenceWithBooknameText);
            if (!references.Any()) throw new FormatException($"Verse reference is empty in '{node.InnerHtml}'! " + GetCurrentBookAsString());
            foreach (var foundRef in references) 
            {
                foreach (var verse in foundRef.GetAllSingleVerses(Canon))
                {
                    // Save converted footnote
                    if (!FootnoteByVerse.ContainsKey(verse))
                    {
                        FootnoteByVerse.Add(verse, new List<HtmlNode>());
                    }
                    FootnoteByVerse[verse].Add(node);
                }
            }
            
            return;
        }
        
        foreach (var childNode in node.ChildNodes)
        {
            TryScanFootnote(childNode);
        }        
    }

    protected void CheckAllFootnotesUsed()
    {
        foreach (var (verse, htmlNodeList) in FootnoteByVerse)
        {
            if (htmlNodeList.Any())
            {
                foreach (var node in htmlNodeList)
                {
                    System.Console.Out.WriteLine($"Warning: Unused footnote in verse {verse}! Footnote: {node.InnerHtml}");
                }
            }
        }
    }

    protected void ConvertFootnote(HtmlNode footnote)
    {
        StartFootnote(OsisFootnoteType.Explanation);

        bool first = true, second = false;
        foreach (var childNode in footnote.ChildNodes)
        {
            if (first)
            {
                // The first node contains the reference. Perhaps more than that.
                // Skip the reference, output the rest (if any).
                var firstNodeText = childNode.InnerText.TrimStart();
                var iReferenceEnd = firstNodeText.IndexOf(FootnoteReferenceSeparator);
                var remainderText = firstNodeText.Substring(iReferenceEnd + FootnoteReferenceSeparator.Length).TrimStart();
                if (!string.IsNullOrWhiteSpace(remainderText))
                {
                    OsisFormatter.Text(remainderText);
                }

                first = false;
                second = true;
            } 
            else if (second)
            {
                if (childNode.Name.ToLower() == "a")
                { 
                    OsisFormatter.Text(""); // Prepend some text to have an indentation
                }
                ConvertNode(childNode, false, false);

                second = false;
            }
            else
            {
                ConvertNode(childNode, false, false);
            }
        }

        OsisFormatter.EndLine();
        EndFootnote();
    }

    protected bool IsHyperLink(string text)
    {
        text = text.ToLower();
        foreach (var hyperLinkToken in HyperLinkTokens)
        {
            if (text.Contains(hyperLinkToken)) return true;
        }

        return false;
    }

    protected bool IsOnlyEmptyChildren(HtmlNode currentNode)
    {
        foreach (var child in currentNode.ChildNodes)
        {
            if (!string.IsNullOrWhiteSpace(child.InnerText)
                && child.InnerText.Trim().ToLower() != TextToIgnore) 
            {
                return false;
            }
        }

        return true;
    }

    protected OsisFormatter StartBook(BookInfo bookInfo)
    {
        BookInfo = bookInfo;
        ChapterNumber = 0;
        DeviatingChapterNumber = null;
        VerseNumber = 0;

        OsisFormatter.StartBook(bookInfo.Book);

        return OsisFormatter;
    }

    protected OsisFormatter EndBook()
    {
        OsisFormatter.EndBook();

        return OsisFormatter;
    }

    protected OsisFormatter StartIntroduction()
    {
        OsisFormatter.StartIntroduction();
        StartedIntroduction = true;

        return OsisFormatter;
    }

    protected OsisFormatter EndIntroduction()
    {
        OsisFormatter.EndIntroduction();
        StartedIntroduction = false;

        return OsisFormatter;
    }

    protected OsisFormatter StartMajorSection(string? title = null)
    {
        OsisFormatter.StartMajorSection();
        StartedMajorSection = true;

        if (!string.IsNullOrWhiteSpace(title))
        {
            OsisFormatter.Title(title);
        }

        return OsisFormatter;
    }

    protected OsisFormatter EndMajorSection()
    {
        OsisFormatter.EndMajorSection();
        StartedMajorSection = false;

        return OsisFormatter;
    }

    protected OsisFormatter StartSection(string? title = null)
    {
        OsisFormatter.StartSection();
        StartedSection = true;

        if (!string.IsNullOrWhiteSpace(title))
        {
            OsisFormatter.Title(title);
        }

        return OsisFormatter;
    }

    protected OsisFormatter EndSection()
    {
        OsisFormatter.EndSection();
        StartedSection = false;

        return OsisFormatter;
    }

    protected OsisFormatter StartSubSection(string? title = null)
    {
        OsisFormatter.StartSubSection();
        StartedSubSection = true;

        if (!string.IsNullOrWhiteSpace(title))
        {
            OsisFormatter.Title(title);
        }

        return OsisFormatter;
    }

    protected OsisFormatter EndSubSection()
    {
        OsisFormatter.EndSubSection();
        StartedSubSection = false;

        return OsisFormatter;
    }    

    protected OsisFormatter StartFootnote(OsisFootnoteType type)
    {
        OsisFormatter.StartFootnote(type);
        StartedFootnote = true;

        return OsisFormatter;
    }

    protected OsisFormatter EndFootnote()
    {
        OsisFormatter.EndFootnote();
        StartedFootnote = false;

        ClearReferenceBookCache();

        return OsisFormatter;
    }    

    protected OsisFormatter StartParagraph()
    {
        OsisFormatter.StartParagraph();
        StartedParagraph = true;

        return OsisFormatter;
    }

    protected OsisFormatter EndParagraph()
    {
        OsisFormatter.EndParagraph();
        StartedParagraph = false;

        return OsisFormatter;
    }

    protected OsisFormatter StartChapter(uint? chapterNumber = null)
    {
        VerseNumber = 0;
        if (!chapterNumber.HasValue)
        {
            ChapterNumber = ChapterNumber+1;
        }
        else 
        {
            ChapterNumber = chapterNumber.Value;
        }                
        DeviatingChapterNumber = null;        

        OsisFormatter.StartChapter(BookInfo.Book, ChapterNumber);
        StartedChapter = true;

        return OsisFormatter;
    }

    protected OsisFormatter EndChapter()
    {
        OsisFormatter.EndChapter(BookInfo.Book, ChapterNumber);
        StartedChapter = false;

        return OsisFormatter;
    }

    protected OsisFormatter StartVerse(uint? verseNumber = null, uint? chapterNumber = null)
    {
        if (!verseNumber.HasValue)
        {
            VerseNumber++;
        } 
        else
        {
            VerseNumber = verseNumber.Value;
        }

        // Will in most cases be null. Except for verses that are "out of chapter" (Num 25:19, Neh 7,72, 1 Chr 22:1, Rev 12:18)
        DeviatingChapterNumber = chapterNumber;
        
        OsisFormatter.StartVerse(BookInfo.Book, GetCurrentChapterNumber(), VerseNumber);
        StartedVerse = true;

        return OsisFormatter;
    }

    protected OsisFormatter EndVerse()
    {
        OsisFormatter.EndVerse(BookInfo.Book, GetCurrentChapterNumber(), VerseNumber);
        StartedVerse = false;

        return OsisFormatter;
    }
}
