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

public record Verse
{
    public Book Book { get; set; }

    public uint ChapterNumber {get; set; }

    public uint VerseNumber { get; set; }

    public Verse(Book book, uint chapter, uint verse)
    {
        Book = book;
        ChapterNumber = chapter;
        VerseNumber = verse;
    }
}
