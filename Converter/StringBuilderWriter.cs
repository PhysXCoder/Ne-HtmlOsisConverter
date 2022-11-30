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

using System.Text;

namespace NeueHtmlOsisConverter.Converter;

public class StringBuilderWriter : IWriter
{
    protected StringBuilder Builder;

    public StringBuilderWriter()
    {
        Builder = new StringBuilder();
    }

    public void Dispose()
    {
        Clear();
    }

    public void Clear()
    {
        Builder.Clear();
    }

    public void Write(string text)
    {
        Builder.Append(text);
    }

    public void WriteLine(string text = "")
    {
        Builder.AppendLine(text);
    }

    public override string ToString()
    {
        return Builder.ToString();
    }
}
