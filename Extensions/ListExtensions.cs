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

public static class ListExtensions
{
    public static T RemoveLast<T>(this IList<T> list)
    {
        T element = list[list.Count - 1];
        list.RemoveAt(list.Count-1);
        return element;
    }

    public static T RemoveFirst<T>(this IList<T> list)
    {
        T element = list[0];
        list.RemoveAt(0);
        return element;
    }

    public static bool AddUnique<T>(this IList<T> list, T element)
    {
        if (list.Contains(element)) return false;

        list.Add(element);
        return true;
    }

    public static bool PrependUnique<T>(this IList<T> list, T element)
    {
        var iElem = list.IndexOf(element);
        if (iElem >= 0) 
        {
            if (iElem != 0)
            {
                var e = list[iElem];
                list.RemoveAt(iElem);
                list.Insert(0, e);
            }
            return false;
        }

        list.Insert(0, element);
        return true;
    }

    public static bool AddRangeUnique<T>(this IList<T> list, IEnumerable<T> elements)
    {       
        bool added = false; 
        foreach(var elem in elements)
        {
            added |= list.AddUnique(elem);
        }

        return added;
    }

    public static bool PrependRangeUnique<T>(this IList<T> list, IEnumerable<T> elements)
    {       
        var listToAdd = elements.Reverse().ToList();   // Reverse to keep order (due to prepend instead of append)

        bool added = false; 
        foreach(var elem in listToAdd)
        {
            added |= list.PrependUnique(elem);
        }

        return added;
    }
}
