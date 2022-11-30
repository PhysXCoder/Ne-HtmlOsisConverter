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

namespace NeueHtmlOsisConverter.Extensions;

public static class StringExtensions
{
    public static int IndexOfFirstDigit(this string text)
    {
        if (text == null) return -1;

        var length = text.Length;
        if (length < 1) return -1;

        for (int i=0; i<length; i++)
        {
            var c = text[i];
            if (c >= '0' && c <= '9') return i;
        }

        return -1;
    }

    public static int IndexOfLastDigit(this string text)
    {
        if (text == null) return -1;

        var length = text.Length;
        if (length < 1) return -1;

        for (int i=length-1; i>=0; i--)
        {
            var c = text[i];
            if (c >= '0' && c <= '9') return i;
        }

        return -1;
    }

    public static int IndexOfFirstLetter(this string text)
    {
        if (text == null) return -1;

        var length = text.Length;
        if (length < 1) return -1;

        for (int i=0; i<length; i++)
        {            
            if (char.IsLetter(text, i)) return i;
        }

        return -1;
    }    

    public static int IndexOfLastLetter(this string text)
    {
        if (text == null) return -1;

        var length = text.Length;
        if (length < 1) return -1;

        for (int i=length-1; i>=0; i--)
        {            
            if (char.IsLetter(text, i)) return i;
        }

        return -1;
    }

    public static int IndexOfFirstInvalid(this string text, char[] validCharacters)
    {
        if (text == null) return -1;

        var length = text.Length;
        if (length < 1) return -1;

        for (int i=0; i<length; i++)
        {            
            if (!validCharacters.Contains(text[i]))
            {
                return i;
            }
        }

        return -1;
    }
}
