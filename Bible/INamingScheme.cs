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

public interface INamingScheme
{
    /// <summar>
    /// Returns the name for the book in the naming schema. 
    /// E.g. Psalms: "Ps" with OSIS naming schema, "PSA" with USFM 3 naming schema.
    /// </summary>
    string this[Book book] { get;}
}
