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

public interface ICanon
{
    /// <summary>
    /// Every book that is contained in the canon is listed here.
    /// </summary>
    IReadOnlyList<BookInfo> Books { get; }

    /// <summary>
    /// Gets the next verse (increases verse number by 1. Possible jumps to next chapter.)
    /// </summary>
    Verse GetSuccessorVerse(Verse verse);
}