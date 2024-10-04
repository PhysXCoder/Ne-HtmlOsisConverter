/*
HtmlOsisConverter - Converts NeÜ Bible HTML files to OSIS XML.
Copyright (C) 2022-2024 PhysXCoder

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

using NeueHtmlOsisConverter.Bible;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NeueHtmlOsisConverter.Converter;

public class OsisFormatter
{
    // Injected parameters/dependencies:    
    public INamingScheme NamingScheme { get; set; }
    public IWriter Writer { get; set; }
    public bool LastWriteEndedWithNewline { get; protected set; }

    protected uint IndentLevel = 0;
    protected Dictionary<uint, string> IndentationByLevel;
    protected Dictionary<OsisFootnoteType, string> FootnoteNameByType;

    public OsisFormatter(INamingScheme namingScheme, IWriter writer)
    {
        NamingScheme = namingScheme;
        Writer = writer;
        LastWriteEndedWithNewline = false;   

        InitIndentationDict();
        InitFootnoteNameDict();
    }    

    #region Formatting functions

    public OsisFormatter StartBook(Book book)
    {
        WriteLine(IndentLevel++, $"<div type=\"book\" osisID=\"{NamingScheme[book]}\">");
        return this;
    }

    public OsisFormatter EndBook()
    {
        WriteLine(--IndentLevel, "</div>");        
        return this;
    }

    public OsisFormatter StartMajorSection()
    {
        WriteLine(IndentLevel++, "<div type=\"majorSection\">");
        return this;
    }

    public OsisFormatter EndMajorSection()
    {
        WriteLine(--IndentLevel, "</div>");
        return this;
    }

    public OsisFormatter StartSection()
    {        
        WriteLine(IndentLevel++, "<div type=\"section\">");
        return this;
    }

    public OsisFormatter EndSection()
    {
        WriteLine(--IndentLevel, "</div>");
        return this;
    }

    public OsisFormatter StartSubSection()
    {
        WriteLine(IndentLevel++, "<div type=\"subSection\">");
        return this;
    }

    public OsisFormatter EndSubSection()
    {        
        WriteLine(--IndentLevel, "</div>");
        return this;
    }

    public OsisFormatter StartParagraph()
    {        
        WriteLine(IndentLevel++, "<p>");
        return this;
    }

    public OsisFormatter EndParagraph()
    {        
        WriteLine(--IndentLevel, "</p>");
        return this;
    }

    public OsisFormatter StartChapter(Book book, uint chapter)
    {
        WriteLine(IndentLevel, $"<chapter sID=\"{NamingScheme[book]}.{chapter}\" osisID=\"{NamingScheme[book]}.{chapter}\"/>");
        return this;
    }

    public OsisFormatter EndChapter(Book book, uint chapter)
    {
        WriteLine(IndentLevel, $"<chapter eID=\"{NamingScheme[book]}.{chapter}\"/>");
        return this;
    }

    public OsisFormatter StartVerse(Book book, uint chapter, uint verse)
    {
        WriteLine(IndentLevel, $"<verse sID=\"{NamingScheme[book]}.{chapter}.{verse}\" osisID=\"{NamingScheme[book]}.{chapter}.{verse}\"/>");

        return this;
    }

    public OsisFormatter EndVerse(Book book, uint chapter, uint verse)
    {       
        WriteLine(IndentLevel, $"<verse eID=\"{NamingScheme[book]}.{chapter}.{verse}\"/>");
        return this;
    }

    public OsisFormatter StartIntroduction()
    {
        WriteLine(IndentLevel++, $"<div type=\"introduction\">");
        return this;
    }

    public OsisFormatter EndIntroduction()
    {
        WriteLine(--IndentLevel, "</div>");
        return this;
    }

    public OsisFormatter LineBreak()
    {        
        WriteLine(IndentLevel, "<lb/>");
        return this;
    }

    public OsisFormatter Title(string title)
    {        
        WriteLine(IndentLevel, $"<title>{title}</title>");
        return this;
    }

    public OsisFormatter StartTitle()
    {
        WriteLine(IndentLevel++, "<title>");
        return this;
    }

    public OsisFormatter EndTitle()
    {
        WriteLine(--IndentLevel, "</title>");
        return this;
    }

    public OsisFormatter Text(string text)
    {       
        if (LastWriteEndedWithNewline) 
        {
            IndentedText(text);
        } 
        else
        {
            UnindentedText(text);
        }
        return this;
    }

    public OsisFormatter UnindentedText(string text)
    {
        Write(text);
        return this;
    }

    public OsisFormatter IndentedText(string text)
    {
        Write(IndentLevel, text);
        return this;
    }

    public OsisFormatter Text(OsisTextStyle style, string text)
    {
        string toWrite = "";
        if (style.HasFlag(OsisTextStyle.Bold))      toWrite += "<hi type=\"bold\">";
        if (style.HasFlag(OsisTextStyle.Italic))    toWrite += "<hi type=\"italic\">";
        if (style.HasFlag(OsisTextStyle.Underline)) toWrite += "<hi type=\"underline\">";
        toWrite += text;        
        if (style.HasFlag(OsisTextStyle.Italic))    toWrite += "</hi>";
        if (style.HasFlag(OsisTextStyle.Bold))      toWrite += "</hi>";
        if (style.HasFlag(OsisTextStyle.Underline)) toWrite += "</hi>";

        Text(toWrite);

        return this;

    }    

    public OsisFormatter StartFootnote(OsisFootnoteType type)
    {        
        WriteLine($"<note type=\"{FootnoteNameByType[type]}\" n=\"*\">");        
        ++IndentLevel;
        return this;
    }

    public OsisFormatter EndFootnote()
    {
        Write(--IndentLevel, "</note>");
        return this;
    }

    public OsisFormatter CatchWord(string catchWord)
    {
        Write($"<catchWord>{catchWord}</catchWord>");
        return this;
    }

    public OsisFormatter Added(string text)
    {
        Write($"<transChange type=\"added\">{text}</transChange>");
        return this;
    }

    public OsisFormatter DivineName(string text)
    {
        Write($"<divineName>{text}</divineName>");
        return this;
    }

    public OsisFormatter ReferenceLine(VerseReference verseRef)
    {
        Indent();
        Reference(verseRef);
        WriteLine();
        return this;
    }

    public OsisFormatter StartLyricsGroup()
    {
        WriteLine(IndentLevel++, "<lg>");
        return this;
    }

    public OsisFormatter EndLyricsGroup()
    {
        WriteLine(--IndentLevel, "</lg>");
        return this;
    }

    public OsisFormatter StartLyric()
    {
        WriteLine(IndentLevel++, "<l>");
        return this;
    }

    public OsisFormatter EndLyric()
    {
        WriteLine(--IndentLevel, "</l>");
        return this;
    }

    public OsisFormatter Indent()
    {
        Write(IndentLevel, string.Empty);
        return this;
    }

    public OsisFormatter Line(string text)
    {
        WriteLine(IndentLevel, text);
        return this;
    }

    public OsisFormatter Line(OsisTextStyle style, string text)
    {
        return Indent()
            .Text(style, text)
            .EndLine();
    }

    public OsisFormatter EndLine()
    {
        WriteLine();
        return this;
    }

    public OsisFormatter HyperLink(string text, Uri uri)
    {
        Write($"<a href=\"{uri}\">{text}</a>");
        return this;
    }    

    public OsisFormatter Reference(VerseReference verseRef)
    {
        string verseRefText = GetSingleReferenceText(verseRef.Verse);

        if (verseRef is SingleVerseReference singleVerseRef)
        {
            // Nothing additional necessary
        }
        else if (verseRef is RangeVerseReference rangeVerseRef)
        {
            verseRefText += "-" + GetSingleReferenceText(rangeVerseRef.EndVerse);
        }
        else if (verseRef is MultipleVerseReference multipleVerseRef)
        {
            verseRefText = string.Join(" ", multipleVerseRef.VerseList.Select(GetSingleReferenceText));
        }
        else
        {
            throw new NotImplementedException($"Conversion for verse reference of type {verseRef.GetType().Name} not implemented!");
        }

        Write($"<reference osisRef=\"{verseRefText}\">{verseRef.Text}</reference>");
        return this;
    }

    private string GetSingleReferenceText(Verse verse)
    {        
        if (verse.VerseNumber != 0)
        {
            return $"{NamingScheme[verse.Book]}.{verse.ChapterNumber}.{verse.VerseNumber}";
        }
        else if (verse.ChapterNumber != 0)
        {
            return $"{NamingScheme[verse.Book]}.{verse.ChapterNumber}";
        }
        else
        {
            return $"{NamingScheme[verse.Book]}";
        }
    }

    public OsisFormatter WriteOsisBeginning(string workName, string workTitle)
    {
        WriteLine(GetIndentation(IndentLevel) + "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
        WriteLine(GetIndentation(IndentLevel) + "<osis xsi:schemaLocation=\"http://www.bibletechnologies.net/2003/OSIS/namespace");
        WriteLine(GetIndentation(IndentLevel) +     "\thttps://www.crosswire.org/~dmsmith/osis/osisCore.2.1.1-cw-latest.xsd\"");
        WriteLine(GetIndentation(IndentLevel) +     "\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
        WriteLine(GetIndentation(IndentLevel) +     "\txmlns=\"http://www.bibletechnologies.net/2003/OSIS/namespace\">");
        WriteLine(GetIndentation(IndentLevel) +     "\t");
        WriteLine(GetIndentation(IndentLevel) +     $"\t<osisText osisIDWork=\"{workName}\" osisRefWork=\"defaultReferenceScheme\" xml:lang=\"de\">");
        WriteLine(GetIndentation(IndentLevel) +         "\t\t");
        WriteLine(GetIndentation(IndentLevel) +         "\t\t<header>");
        WriteLine(GetIndentation(IndentLevel) +             $"\t\t\t<work osisWork=\"{workName}\">");
        WriteLine(GetIndentation(IndentLevel) +                 $"\t\t\t\t<title>{workTitle}</title>");
        WriteLine(GetIndentation(IndentLevel) +                 "\t\t\t\t<type type=\"OSIS\">Bible</type>");
        WriteLine(GetIndentation(IndentLevel) +                 $"\t\t\t\t<identifier type=\"OSIS\">{workName}.HtmlConverted</identifier>");
        WriteLine(GetIndentation(IndentLevel) +                 "\t\t\t\t<refSystem>Bible.German</refSystem>");
        WriteLine(GetIndentation(IndentLevel) +             "\t\t\t</work>");
        WriteLine(GetIndentation(IndentLevel) +             "\t\t\t<work osisWork=\"defaultReferenceScheme\">");
        WriteLine(GetIndentation(IndentLevel) +                 "\t\t\t\t<refSystem>Bible.German</refSystem>");
        WriteLine(GetIndentation(IndentLevel) +             "\t\t\t</work>");
        WriteLine(GetIndentation(IndentLevel) +         "\t\t</header>");
        WriteLine(GetIndentation(IndentLevel) +     "\t");
        LastWriteEndedWithNewline = true;

        IndentLevel += 2;
        //PrependIndentationOnNextWrite = true;

        return this;
    }

    public OsisFormatter WriteOsisEnding()
    {
        IndentLevel -= 2;

        Writer.WriteLine(GetIndentation(IndentLevel) +      "\t</osisText>");
        Writer.WriteLine(GetIndentation(IndentLevel) + "</osis>");
        LastWriteEndedWithNewline = true;

        return this;
    }

    #endregion

    #region Other public methods

    public void Restart()
    {
        IndentLevel = 0;
        LastWriteEndedWithNewline = false;
    }

    #endregion

    #region Indentation

    [MemberNotNull(nameof(IndentationByLevel))]
    protected void InitIndentationDict()
    {
        IndentationByLevel = new Dictionary<uint, string>()
        {
            {0, ""},
            {1, "\t"},
            {2, "\t\t"},
            {3, "\t\t\t"},
            {4, "\t\t\t\t"},
            {5, "\t\t\t\t\t"},
            {6, "\t\t\t\t\t\t"},
            {7, "\t\t\t\t\t\t\t"},
            {8, "\t\t\t\t\t\t\t\t"},
            {9, "\t\t\t\t\t\t\t\t\t"},
        };
    }    

    protected string GetIndentation(uint level)
    {
        if (!IndentationByLevel.ContainsKey(level))
        {
            var indentBuilder = new StringBuilder();
            for (uint i=1; i<=level; i++)
            {
                indentBuilder.Append('\t');
            }
            IndentationByLevel[level] = indentBuilder.ToString();
        }

        return IndentationByLevel[level];
    }

    #endregion

    #region Footnotes

    [MemberNotNull(nameof(FootnoteNameByType))]
    protected void InitFootnoteNameDict()
    {
        FootnoteNameByType = new Dictionary<OsisFootnoteType, string>()
        {
            {OsisFootnoteType.Alternative, "alternative"},
            {OsisFootnoteType.Background, "background"},
            {OsisFootnoteType.CrossReference, "crossReference"},
            {OsisFootnoteType.Explanation, "explanation"},
            {OsisFootnoteType.Study, "study"},
            {OsisFootnoteType.Translation, "translation"},
            {OsisFootnoteType.Variant, "variant"},
        };
    }

    #endregion

    #region Basic Write/WriteLine methods

    protected void Write(string text)
    {
        Writer.Write(text);
        LastWriteEndedWithNewline = text.EndsWith(Environment.NewLine);
    }

    protected void Write(uint indentLevel, string text)
    {
        Writer.Write(GetIndentation(indentLevel) + text);
        LastWriteEndedWithNewline = false;
    }

    protected void WriteLine(string text)
    {
        Writer.WriteLine(text);
        LastWriteEndedWithNewline = true;
    }

    protected void WriteLine(uint indentLevel, string text)
    {
        Writer.WriteLine(GetIndentation(indentLevel) + text);
        LastWriteEndedWithNewline = true;
    }

    protected void WriteLine()
    {
        Writer.WriteLine();
        LastWriteEndedWithNewline = true;
    }

    #endregion
}
