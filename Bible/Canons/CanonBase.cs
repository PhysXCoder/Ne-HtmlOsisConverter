/*
HtmlOsisConverter - Converts NeÜ Bible HTML files to OSIS XML.
Copyright (C) 2023 PhysXCoder

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

public abstract class CanonBase : ICanon
{
    public abstract IReadOnlyList<BookInfo> Books { get; }

    public Verse GetSuccessorVerse(Verse verse)
    {
        var successorVerse = new Verse(verse.Book, verse.ChapterNumber, verse.VerseNumber + 1);
        if (!CheckIfValid(successorVerse))
        {
            successorVerse = new Verse(verse.Book, verse.ChapterNumber + 1, 1);
        }
        if (!CheckIfValid(successorVerse))
        {
            successorVerse = new Verse(verse.Book + 1, 1, 1);
        }

        return successorVerse;
    }

    public bool CheckIfValid(Verse verse)
    {
        var bookInfo = Books.FirstOrDefault(book => book.Book == verse.Book);
        
        return (bookInfo != null &&
            verse.ChapterNumber <= bookInfo.Chapters &&
            verse.VerseNumber <= bookInfo.VersesByChapter[(int)verse.ChapterNumber]);
    }
}