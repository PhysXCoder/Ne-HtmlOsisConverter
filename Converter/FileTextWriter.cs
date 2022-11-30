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

namespace NeueHtmlOsisConverter.Converter;

public class FileTextWriter : IWriter
{
    protected TextWriter Writer;

    public FileTextWriter(TextWriter writer)
    {
        Writer = writer;
    }

    public void Dispose()
    {
        Writer.Dispose();
    }

    public void Write(string text)
    {
        Writer.Write(text);
    }

    public void WriteLine(string text = "")
    {
        Writer.WriteLine(text);
    }
}
