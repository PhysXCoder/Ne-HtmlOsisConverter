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

namespace NeueHtmlOsisConverter.Bible;

public abstract class VerseReference
{
    public Verse Verse { get; set; }

    public string Text { get; set; }

    protected VerseReference(Verse verse, string text)
    {
        Verse = verse;
        Text = text;
    }

    public abstract IReadOnlyList<Verse> GetAllSingleVerses(ICanon canon);
}

public class SingleVerseReference : VerseReference
{
    public SingleVerseReference(Verse verse, string text)
        : base(verse, text)
    { }

    public override IReadOnlyList<Verse> GetAllSingleVerses(ICanon canon) => 
        (new List<Verse>() {Verse}).AsReadOnly();
}

public class RangeVerseReference : VerseReference
{
    /// <summary>
    /// Last Verse that is included (!) in the span.
    /// </summary>
    public Verse EndVerse { get; set; }

    public RangeVerseReference(Verse startVerse, Verse endVerse, string text)
        : base(startVerse, text)
    {
        EndVerse = endVerse;
    }

    public override IReadOnlyList<Verse> GetAllSingleVerses(ICanon canon)
    {
        var verses = new List<Verse>();
        var currentVerse = Verse;
        while (currentVerse != EndVerse && currentVerse.Book <= EndVerse.Book) 
        {
            verses.Add(currentVerse);
            currentVerse = canon.GetSuccessorVerse(currentVerse);
        };
        if (!verses.Contains(EndVerse)) 
        {
            verses.Add(EndVerse);
        }        
        return verses.AsReadOnly();
    }
}

public class MultipleVerseReference : VerseReference
{
    public IList<Verse> VerseList { get; set; }

    public MultipleVerseReference(IEnumerable<Verse> references, string text)
        : base(references.First(), text)
    {
        VerseList = references.ToList();
    }

    public override IReadOnlyList<Verse> GetAllSingleVerses(ICanon canon) =>
        ((List<Verse>)VerseList).AsReadOnly();
}
