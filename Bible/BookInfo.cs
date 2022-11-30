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

public class BookInfo
{
    /// <summary>
    /// The Book that is described in this BookInfo.
    /// </summary>
    public Book Book { get; set; }

    /// <summary>
    /// Returns the number of biblical chapters. E.g. 50 for Genesis.
    ///
    /// Note that chapters may differ from canon to canon.
    /// E.g. Joel has 3 chapters in English bibles, but 4 chapters in German bibles. Similar Malachi 3/4. 
    /// </summary>
    public ushort Chapters => (ushort)(VersesByChapter.Count - 1);

    /// <summary>
    /// Returns the number of verses in each Chapter.
    /// Chapter 0 is special for introductory text etc. Chapter 1 is the first biblical chapter of the book and so on.
    ///
    /// Note that verse numbers might differ between canons.
    /// E.g. Isaiah 8:23 in French/German bibles is 9:1 in English bibles. 
    /// </summary>
    public List<ushort> VersesByChapter { get; set; }

    /// <summary>
    /// Names of the book (e.g. "Gen", "Genesis", "1Mo", "1 Mo", "1.Mo", "1. Mo", "1. Mose")
    /// </summary>
    public List<string> BookNames { get; set; }

    public BookInfo(Book book, List<ushort> versesByChapter, List<string> bookNames)
    {
        Book = book;
        VersesByChapter = versesByChapter;
        BookNames = bookNames;
    }
}