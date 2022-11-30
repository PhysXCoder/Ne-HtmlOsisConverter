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

namespace NeueHtmlOsisConverter.Bible.Canons;

/// <summary>
/// Canon according to King James Version.
/// Verse numbers taken from SWORD project's canon.h v 1.9.0
/// https://www.crosswire.org/sword/develop/index.jsp
/// </summary>
public class KjvCanon : ICanon
{
        
    private readonly IReadOnlyList<BookInfo> _books = new[]
    {
        new BookInfo(Book.Genesis, new List<ushort>()
        {
            0,
            31, 25, 24, 26, 32, 22, 24, 22, 29, 32,
            32, 20, 18, 24, 21, 16, 27, 33, 38, 18,
            34, 24, 20, 67, 34, 35, 46, 22, 35, 43,
            55, 32, 20, 31, 29, 43, 36, 30, 23, 23,
            57, 38, 34, 34, 28, 34, 31, 22, 33, 26
        }, new List<string>() { "1Mo", "1 Mo", "Genesis" }),

        new BookInfo(Book.Exodus, new List<ushort>()
        {
            0,
            22, 25, 22, 31, 23, 30, 25, 32, 35, 29,
            10, 51, 22, 31, 27, 36, 16, 27, 25, 26,
            36, 31, 33, 18, 40, 37, 21, 43, 46, 38,
            18, 35, 23, 35, 35, 38, 29, 31, 43, 38
        }, new List<string>() { "2Mo", "2 Mo", "Exodus"}),
        
        new BookInfo(Book.Leviticus, new List<ushort>()
        {
            0,
            17, 16, 17, 35, 19, 30, 38, 36, 24, 20,
            47, 08, 59, 57, 33, 34, 16, 30, 37, 27,
            24, 33, 44, 23, 55, 46, 34
        }, new List<string>() { "3Mo", "3 Mo", "Leviticus" }),
        
        new BookInfo(Book.Numbers, new List<ushort>()
        {
            0,
            54, 34, 51, 49, 31, 27, 89, 26, 23, 36,
            35, 16, 33, 45, 41, 50, 13, 32, 22, 29,
            35, 41, 30, 25, 18, 65, 23, 31, 40, 16,
            54, 42, 56, 29, 34, 13
        }, new List<string>() { "4Mo", "4 Mo", "Numbers" }),
        
        new BookInfo(Book.Deuteronomy, new List<ushort>()
        {
            0,
            46, 37, 29, 49, 33, 25, 26, 20, 29, 22,
            32, 32, 18, 29, 23, 22, 20, 22, 21, 20,
            23, 30, 25, 22, 19, 19, 26, 68, 29, 20,
            30, 52, 29, 12
        }, new List<string>() { "5Mo", "5 Mo", "Deuteronomy" }),
        
        new BookInfo(Book.Joshua, new List<ushort>()
        {
            0,
            18, 24, 17, 24, 15, 27, 26, 35, 27, 43,
            23, 24, 33, 15, 63, 10, 18, 28, 51, 09,
            45, 34, 16, 33
        }, new List<string>() { "Jos", "Joshua" }),
        
        new BookInfo(Book.Judges, new List<ushort>()
        {
            0,
            36, 23, 31, 24, 31, 40, 25, 35, 57, 18,
            40, 15, 25, 20, 20, 31, 13, 31, 30, 48,
            25
        }, new List<string>() { "Judg", "Judges" }),
        
        new BookInfo(Book.Ruth, new List<ushort>()
        {
            0,
            22, 23, 18, 22
        }, new List<string>() { "Ruth" }),
        
        new BookInfo(Book.FirstSamuel, new List<ushort>()
        {
            0,
            28, 36, 21, 22, 12, 21, 17, 22, 27, 27,
            15, 25, 23, 52, 35, 23, 58, 30, 24, 42,
            15, 23, 29, 22, 44, 25, 12, 25, 11, 31,
            13,
        }, new List<string>() { "1Sam", "1 Sam", "1 Samuel" }),
        
        new BookInfo(Book.SecondSamuel, new List<ushort>()
        {
            0,
            27, 32, 39, 12, 25, 23, 29, 18, 13, 19,
            27, 31, 39, 33, 37, 23, 29, 33, 43, 26,
            22, 51, 39, 25
        }, new List<string>() { "2Sam", "2 Sam", "2 Samuel" }),
        
        new BookInfo(Book.FirstKings, new List<ushort>()
        {
            0,
            53, 46, 28, 34, 18, 38, 51, 66, 28, 29,
            43, 33, 34, 31, 34, 34, 24, 46, 21, 43,
            29, 53
        }, new List<string>() { "1Kg", "1Kgs", "1 Kings", "1 Kg", "1 Kgs", "1 Kings" }),
        
        new BookInfo(Book.SecondKings, new List<ushort>()
        {
            0,
            18, 25, 27, 44, 27, 33, 20, 29, 37, 36,
            21, 21, 25, 29, 38, 20, 41, 37, 37, 21,
            26, 20, 37, 20, 30
        }, new List<string>() { "2Kg", "2Kgs", "2 Kings", "2 Kg", "2 Kgs", "2 Kings" }),
        
        new BookInfo(Book.FirstChronicles, new List<ushort>()
        {
            0,
            54, 55, 24, 43, 26, 81, 40, 40, 44, 14,
            47, 40, 14, 17, 29, 43, 27, 17, 19, 08,
            30, 19, 32, 31, 31, 32, 34, 21, 30
        }, new List<string>() { "1Chr", "1 Chr", "1Chronicles", "1 Chronicles" }),
        
        new BookInfo(Book.SecondChronicles, new List<ushort>()
        {
            0,
            17, 18, 17, 22, 14, 42, 22, 18, 31, 19,
            23, 16, 22, 15, 19, 14, 19, 34, 11, 37,
            20, 12, 21, 27, 28, 23, 09, 27, 36, 27,
            21, 33, 25, 33, 27, 23
        }, new List<string>() { "2Chr", "2 Chr", "2Chronicles", "2 Chronicles" }),
        
        new BookInfo(Book.Ezra, new List<ushort>()
        {
            0,
            11, 70, 13, 24, 17, 22, 28, 36, 15, 44
        }, new List<string>() { "Ezra" }),
        
        new BookInfo(Book.Nehemiah, new List<ushort>()
        {
            0,
            11, 20, 32, 23, 19, 19, 73, 18, 38, 39,
            36, 47, 31
        }, new List<string>() { "Neh", "Nehemia" }),
        
        new BookInfo(Book.Esther, new List<ushort>()
        {
            0,
            22, 23, 15, 17, 14, 14, 10, 17, 32, 03
        }, new List<string>() { "Est", "Esther" }),
        
        new BookInfo(Book.Job, new List<ushort>()
        {
            0,
            22, 13, 26, 21, 27, 30, 21, 22, 35, 22,
            20, 25, 28, 22, 35, 22, 16, 21, 29, 29,
            34, 30, 17, 25, 06, 14, 23, 28, 25, 31,
            40, 22, 33, 37, 16, 33, 24, 41, 30, 24,
            34, 17
        }, new List<string>() { "Job" }),
        
        new BookInfo(Book.Psalms, new List<ushort>()
        {
            0,
            06, 12, 08, 08, 12, 10, 17, 09, 20, 18,
            07, 08, 06, 07, 05, 11, 15, 50, 14, 09,
            13, 31, 06, 10, 22, 12, 14, 09, 11, 12,
            24, 11, 22, 22, 28, 12, 40, 22, 13, 17,
            13, 11, 05, 26, 17, 11, 09, 14, 20, 23,
            19, 09, 06, 07, 23, 13, 11, 11, 17, 12,
            08, 12, 11, 10, 13, 20, 07, 35, 36, 05,
            24, 20, 28, 23, 10, 12, 20, 72, 13, 19,
            16, 08, 18, 12, 13, 17, 07, 18, 52, 17,
            16, 15, 05, 23, 11, 13, 12, 09, 09, 05,
            08, 28, 22, 35, 45, 48, 43, 13, 31, 07,
            10, 10, 09, 08, 18, 19, 02, 29, 176, 07,
            08, 09, 04, 08, 05, 06, 05, 06, 08, 08,
            03, 18, 03, 03, 21, 26, 09, 08, 24, 13,
            10, 07, 12, 15, 21, 10, 20, 14, 09, 06
        }, new List<string>() { "Ps", "Psa", "Psalms" }),
        
        new BookInfo(Book.Proverbs, new List<ushort>()
        {
            0,
            33, 22, 35, 27, 23, 35, 27, 36, 18, 32,
            31, 28, 25, 35, 33, 33, 28, 24, 29, 30,
            31, 29, 35, 34, 28, 28, 27, 28, 27, 33,
            31
        }, new List<string>() { "Pro", "Proverbs" }),
        
        new BookInfo(Book.Ecclesiastes, new List<ushort>()
        {
            0,
            18, 26, 22, 16, 20, 12, 29, 17, 18, 20,
            10, 14
        }, new List<string>() { "Eccl" }),
        
        new BookInfo(Book.SongOfSongs, new List<ushort>()
        {
            0,
            17, 17, 11, 16, 16, 13, 13, 14
        }, new List<string>() { "Sgs", "SoS", "Song of Songs" }),
        
        new BookInfo(Book.Isaiah, new List<ushort>()
        {
            0,
            31, 22, 26, 06, 30, 13, 25, 22, 21, 34,
            16, 06, 22, 32, 09, 14, 14, 07, 25, 06,
            17, 25, 18, 23, 12, 21, 13, 29, 24, 33,
            09, 20, 24, 17, 10, 22, 38, 22, 08, 31,
            29, 25, 28, 28, 25, 13, 15, 22, 26, 11,
            23, 15, 12, 17, 13, 12, 21, 14, 21, 22,
            11, 12, 19, 12, 25, 24
        }, new List<string>() { "Isa", "Isaiah" }),
        
        new BookInfo(Book.Jeremiah, new List<ushort>()
        {
            0,
            19, 37, 25, 31, 31, 30, 34, 22, 26, 25,
            23, 17, 27, 22, 21, 21, 27, 23, 15, 18,
            14, 30, 40, 10, 38, 24, 22, 17, 32, 24,
            40, 44, 26, 22, 19, 32, 21, 28, 18, 16,
            18, 22, 13, 30, 05, 28, 07, 47, 39, 46,
            64, 34
        }, new List<string>() { "Jer", "Jeremiah" }),
        
        new BookInfo(Book.Lamentations, new List<ushort>()
        {
            0,
            22, 22, 66, 22, 22
        }, new List<string>() { "Lam", "Lamentations" }),
        
        new BookInfo(Book.Ezekiel, new List<ushort>()
        {
            0,
            28, 10, 27, 17, 17, 14, 27, 18, 11, 22,
            25, 28, 23, 23, 08, 63, 24, 32, 14, 49,
            32, 31, 49, 27, 17, 21, 36, 26, 21, 26,
            18, 32, 33, 31, 15, 38, 28, 23, 29, 49,
            26, 20, 27, 31, 25, 24, 23, 35
        }, new List<string>() { "Eze", "Ezekiel" }),
        
        new BookInfo(Book.Daniel, new List<ushort>()
        {
            0,
            21, 49, 30, 37, 31, 28, 28, 27, 27, 21,
            45, 13                                  
        }, new List<string>() { "Dan", "Daniel" }),
        
        new BookInfo(Book.Hosea, new List<ushort>()
        {
            0,
            11, 23, 05, 19, 15, 11, 16, 14, 17, 15,
            12, 14, 16, 09
        }, new List<string>() { "Hos", "Hosea" }),
        
        new BookInfo(Book.Joel, new List<ushort>()
        {
            0,
            20, 32, 21
        }, new List<string>() { "Joel" }),
        
        new BookInfo(Book.Amos, new List<ushort>()
        {
            0,
            15, 16, 15, 13, 27, 14, 17, 14, 15
        }, new List<string>() { "Am", "Amos" }),
        
        new BookInfo(Book.Obadiah, new List<ushort>()
        {
            0,
            21
        }, new List<string>() { "Obd", "Obadiah" }),
        
        new BookInfo(Book.Jonah, new List<ushort>()
        {
            0,
            17, 10, 10, 11
        }, new List<string>() { "Jonah" }),
        
        new BookInfo(Book.Micah, new List<ushort>()
        {
            0,
            16, 13, 12, 13, 15, 16, 20
        }, new List<string>() { "Mi", "Mic", "Micah" }),
        
        new BookInfo(Book.Nahum, new List<ushort>()
        {
            0,
            15, 13, 19
        }, new List<string>() { "Nah", "Nahum" }),
        
        new BookInfo(Book.Habakkuk, new List<ushort>()
        {
            0,
            17, 20, 19
        }, new List<string>() { "Hab", "Habakuk" }),
        
        new BookInfo(Book.Zephaniah, new List<ushort>()
        {
            0,
            18, 15, 20
        }, new List<string>() { "Zeph", "Zephaniah" }),
        
        new BookInfo(Book.Haggai, new List<ushort>()
        {
            0,
            15, 23
        }, new List<string>() { "Hag", "Haggai" }),
        
        new BookInfo(Book.Zechariah, new List<ushort>()
        {
            0,
            21, 13, 10, 14, 11, 15, 14, 23, 17, 12,
            17, 14, 09, 21
        }, new List<string>() { "Zec", "Zechariah" }),
        
        new BookInfo(Book.Malachi, new List<ushort>()
        {
            0,
            14, 17, 18, 06
        }, new List<string>() { "Mal", "Malachi" }),
        
        new BookInfo(Book.Matthew, new List<ushort>()
        {
            0,
            25, 23, 17, 25, 48, 34, 29, 34, 38, 42,
            30, 50, 58, 36, 39, 28, 27, 35, 30, 34,
            46, 46, 39, 51, 46, 75, 66, 20
        }, new List<string>() { "Mt", "Matthew" }),
        
        new BookInfo(Book.Mark, new List<ushort>()
        {
            0,
            45, 28, 35, 41, 43, 56, 37, 38, 50, 52,
            33, 44, 37, 72, 47, 20
        }, new List<string>() { "Mk", "Mark" }),
        
        new BookInfo(Book.Luke, new List<ushort>()
        {
            0,
            80, 52, 38, 44, 39, 49, 50, 56, 62, 42,
            54, 59, 35, 35, 32, 31, 37, 43, 48, 47,
            38, 71, 56, 53
        }, new List<string>() { "Lk", "Luke" }),
        
        new BookInfo(Book.John, new List<ushort>()
        {
            0,
            51, 25, 36, 54, 47, 71, 53, 59, 41, 42,
            57, 50, 38, 31, 27, 33, 26, 40, 42, 31,
            25
        }, new List<string>() { "Jn", "Jo", "John" }),
        
        new BookInfo(Book.Acts, new List<ushort>()
        {
            0,
            26, 47, 26, 37, 42, 15, 60, 40, 43, 48,
            30, 25, 52, 28, 41, 40, 34, 28, 41, 38,
            40, 30, 35, 27, 27, 32, 44, 31
        }, new List<string>() { "Acts" }),
        
        new BookInfo(Book.Romans, new List<ushort>()
        {
            0,
            32, 29, 31, 25, 21, 23, 25, 39, 33, 21,
            36, 21, 14, 23, 33, 27
        }, new List<string>() { "Rom", "Romans" }),
        
        new BookInfo(Book.FirstCorinthians, new List<ushort>()
        {
            0,
            31, 16, 23, 21, 13, 20, 40, 13, 27, 33,
            34, 31, 13, 40, 58, 24
        }, new List<string>() { "1Cor", "1 Cor", "1Corinthians", "1 Corinthians"}),
        
        new BookInfo(Book.SecondCorinthians, new List<ushort>()
        {
            0,
            24, 17, 18, 18, 21, 18, 16, 24, 15, 18,
            33, 21, 14
        }, new List<string>() { "2Cor", "2 Cor", "2Corinthians", "2 Corinthians" }),
        
        new BookInfo(Book.Galatians, new List<ushort>()
        {
            0,
            24, 21, 29, 31, 26, 18
        }, new List<string>() { "Gal", "Galatians" }),
        
        new BookInfo(Book.Ephesians, new List<ushort>()
        {
            0,
            23, 22, 21, 32, 33, 24
        }, new List<string>() { "Eph", "Ephesians" }),
        
        new BookInfo(Book.Philippians, new List<ushort>()
        {
            0,
            30, 30, 21, 23
        }, new List<string>() { "Phil", "Philippians" }),
        
        new BookInfo(Book.Colossians, new List<ushort>()
        {
            0,
            29, 23, 25, 18
        }, new List<string>() { "Col", "Collossians" }),
        
        new BookInfo(Book.FirstThessalonians, new List<ushort>()
        {
            0,
            10, 20, 13, 18, 28
        }, new List<string>() { "1Th", "1 Th", "1Thes", "1 Thes", "1Thessalonians", "1 Thessalonians" }),
        
        new BookInfo(Book.SecondThessalonians, new List<ushort>()
        {
            0,
            12, 17, 18,
        }, new List<string>() { "2Th", "2 Th", "2Thes", "2 Thes", "2Thessalonians", "2 Thessalonians" }),
        
        new BookInfo(Book.FirstTimothy, new List<ushort>()
        {
            0,
            20, 15, 16, 16, 25, 21
        }, new List<string>() { "1Tim", "1 Tim", "1Timothy", "1 Timothy" }),
        
        new BookInfo(Book.SecondTimothy, new List<ushort>()
        {
            0,
            18, 26, 17, 22
        }, new List<string>() { "2Tim", "2 Tim", "2Timothy", "2 Timothy" }),
        
        new BookInfo(Book.Titus, new List<ushort>()
        {
            0,
            16, 15, 15
        }, new List<string>() { "Tit", "Titus" }),
        
        new BookInfo(Book.Philemon, new List<ushort>()
        {
            0,
            25
        }, new List<string>() { "Ph", "Phlm", "Philemon" }),
        
        new BookInfo(Book.Hebrews, new List<ushort>()
        {
            0,
            14, 18, 19, 16, 14, 20, 28, 13, 28, 39,
            40, 29, 25
        }, new List<string>() { "Heb", "Hebrews" }),
        
        new BookInfo(Book.James, new List<ushort>()
        {
            0,
            27, 26, 18, 17, 20
        }, new List<string>() { "Jam", "James" }),
        
        new BookInfo(Book.FirstPeter, new List<ushort>()
        {
            0,
            25, 25, 22, 19, 14
        }, new List<string>() { "1Pt", "1 Pt", "1Peter", "1 Peter" }),
        
        new BookInfo(Book.SecondPeter, new List<ushort>()
        {
            0,
            21, 22, 18
        }, new List<string>() { "2Pt", "2 Pt", "2Peter", "2 Peter" }),
        
        new BookInfo(Book.FirstJohn, new List<ushort>()
        {
            0,
            10, 29, 24, 21, 21
        }, new List<string>() { "1Jn", "1 Jn", "1Joh", "1 Joh", "1John", "1 John" }),
        
        new BookInfo(Book.SecondJohn, new List<ushort>()
        {
            0,
            13
        }, new List<string>() { "2Jn", "2 Jn", "2Joh", "2 Joh", "2John", "2 John" }),
        
        new BookInfo(Book.ThirdJohn, new List<ushort>()
        {
            0,
            14
        }, new List<string>() { "3Jn", "3 Jn", "3Joh", "3 Joh", "3John", "3 John" }),
        
        new BookInfo(Book.Jude, new List<ushort>()
        {
            0,
            25
        }, new List<string>() { "Jud", "Jude" }),
        
        new BookInfo(Book.Revelation, new List<ushort>()
        {
            0,
            20, 29, 22, 11, 14, 17, 17, 13, 21, 11,
            19, 17, 18, 20, 08, 21, 18, 24, 21, 15,
            27, 21
        }, new List<string>() { "Rev", "Revelation" })
        
    };

    public IReadOnlyList<BookInfo> Books => _books;
}