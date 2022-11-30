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
/// German Protestant canon, numbers according to "NeÜ - Neue evangelistische Übersetzung".  
/// </summary>
public class NeÜCanon : ICanon
{
    private readonly IReadOnlyList<BookInfo> _books = new[]
    {
        new BookInfo(Book.Genesis, new List<ushort>()
        {
            0,
            31, 25, 24, 26, 32, 22, 24, 22, 29, 32,
            32, 20, 18, 24, 21, 16, 27, 33, 38, 18,
            34, 24, 20, 67, 34, 35, 46, 22, 35, 43,
            54, 33, 20, 31, 29, 43, 36, 30, 23, 23,
            57, 38, 34, 34, 28, 34, 31, 22, 33, 26
        }, new List<string>() { "Gen", "Genesis", "1Mo", "1 Mo", "1.Mo", "1. Mo", "1.Mose", "1. Mose" }), 

        new BookInfo(Book.Exodus, new List<ushort>()
        {
            0,
            22, 25, 22, 31, 23, 30, 29, 28, 35, 29,
            10, 51, 22, 31, 27, 36, 16, 27, 25, 26,
            37, 30, 33, 18, 40, 37, 21, 43, 46, 38,
            18, 35, 23, 35, 35, 38, 29, 31, 43, 38
        }, new List<string>() { "Exo", "Exodus", "2Mo", "2 Mo", "2.Mo", "2. Mo", "2.Mose", "2. Mose" }),

        new BookInfo(Book.Leviticus, new List<ushort>()
        {
            0,
            17, 16, 17, 35, 26, 23, 38, 36, 24, 20,
            47, 08, 59, 57, 33, 34, 16, 30, 37, 27,
            24, 33, 44, 23, 55, 46, 34
        }, new List<string>() { "Lev", "Levitikus", "Leviticus", "3Mo", "3 Mo", "3.Mo", "3. Mo", "3.Mose", "3. Mose" }),

        new BookInfo(Book.Numbers, new List<ushort>()
        {
            0,
            54, 34, 51, 49, 31, 27, 89, 26, 23, 36,
            35, 16, 33, 45, 41, 35, 28, 32, 22, 29,
            35, 41, 30, 25, 19, 65, 23, 31, 39, 17,     // Achtung 25,19 steht noch in Kap 26
            54, 42, 56, 29, 34, 13
        }, new List<string>() { "Num", "Numeri", "4Mo", "4 Mo", "4.Mo", "4. Mo", "4.Mose", "4. Mose" }),

        new BookInfo(Book.Deuteronomy, new List<ushort>()
        {
            0,
            46, 37, 29, 49, 33, 25, 26, 20, 29, 22,
            32, 31, 19, 29, 23, 22, 20, 22, 21, 20,
            23, 29, 26, 22, 19, 19, 26, 69, 28, 20,
            30, 52, 29, 12
        }, new List<string>() { "Deu", "Deuteronomium", "5Mo", "5 Mo", "5.Mo", "5. Mo", "5.Mose", "5. Mose" }),

        new BookInfo(Book.Joshua, new List<ushort>()
        {
            0,
            18, 24, 17, 24, 15, 27, 26, 35, 27, 43,
            23, 24, 33, 15, 63, 10, 18, 28, 51, 09,
            45, 34, 16, 33
        }, new List<string>() { "Jos", "Josua", "Joshua" }),

        new BookInfo(Book.Judges, new List<ushort>()
        {
            0,
            36, 23, 31, 24, 31, 40, 25, 35, 57, 18,
            40, 15, 25, 20, 20, 31, 13, 31, 30, 48,
            25
        }, new List<string>() { "Ri", "Richter" }),

        new BookInfo(Book.Ruth, new List<ushort>()
        {
            0,
            22, 23, 18, 22
        }, new List<string>() { "Rut", "Ruth" }),

        new BookInfo(Book.FirstSamuel, new List<ushort>()
        {
            0,
            28, 36, 21, 22, 12, 21, 17, 22, 27, 27,
            15, 25, 23, 52, 35, 23, 58, 30, 24, 42,
            16, 23, 28, 23, 44, 25, 12, 25, 11, 31,
            13
        }, new List<string>() { 
            "1Sam", "1 Sam", "1.Sam", "1. Sam", 
            "1Samuel", "1 Samuel", "1.Samuel", "1. Samuel",
        }),

        new BookInfo(Book.SecondSamuel, new List<ushort>()
        {
            0,
            27, 32, 39, 12, 25, 23, 29, 18, 13, 19,
            27, 31, 39, 33, 37, 23, 29, 32, 44, 26,
            22, 51, 39, 25
        }, new List<string>() 
        { 
            "2Sam", "2 Sam", "2.Sam", "2. Sam", 
            "2Samuel", "2 Samuel", "2.Samuel", "2. Samuel",            
        }),

        new BookInfo(Book.FirstKings, new List<ushort>()
        {
            0,
            53, 46, 28, 20, 32, 38, 51, 66, 28, 29,
            43, 33, 34, 31, 34, 34, 24, 46, 21, 43,
            29, 54
        }, new List<string>() 
        { 
            "1Kö", "1 Kö", "1.Kö", "1. Kö", 
            "1Koe", "1 Koe", "1.Koe", "1. Koe",
            "1Kng", "1 Kng", "1.Kng", "1. Kng", 
            "1Könige", "1 Könige", "1.Könige", "1. Könige" 
        }),

        new BookInfo(Book.SecondKings, new List<ushort>()
        {
            0,
            18, 25, 27, 44, 27, 33, 20, 29, 37, 36,
            20, 22, 25, 29, 38, 20, 41, 37, 37, 21,
            26, 20, 37, 20, 30
        }, new List<string>() 
        {
            "2Kö", "2 Kö", "2.Kö", "2. Kö", 
            "2Koe", "2 Koe", "2.Koe", "2. Koe",
            "2Kng", "2 Kng", "2.Kng", "2. Kng", 
            "2Könige", "2 Könige", "2.Könige", "2. Könige"
        }),

        new BookInfo(Book.FirstChronicles, new List<ushort>()
        {
            0,
            54, 55, 24, 43, 41, 66, 40, 40, 44, 14,
            47, 41, 14, 17, 29, 43, 27, 17, 19, 08,
            30, 19, 32, 31, 31, 32, 34, 21, 30      // 22,1 kein fängt schon in Kap 21 an
        }, new List<string>() 
        { 
            "1Ch", "1 Ch", "1.Ch", "1. Ch", 
            "1Chr", "1 Chr", "1.Chr", "1. Chr",
            "1Chronik", "1 Chronik", "1.Chronik", "1. Chronik", 
            "1Chronika", "1 Chronika", "1.Chronika", "1. Chronika", 
            "1Chroniken", "1 Chroniken", "1.Chroniken", "1. Chroniken"
        }),

        new BookInfo(Book.SecondChronicles, new List<ushort>()
        {
            0,
            18, 17, 17, 22, 14, 42, 22, 18, 31, 19,
            23, 16, 23, 14, 19, 14, 19, 34, 11, 37,
            20, 12, 21, 27, 28, 23, 09, 27, 36, 27,
            21, 33, 25, 33, 27, 23
        }, new List<string>() 
        {
            "2Ch", "2 Ch", "2.Ch", "2. Ch", 
            "2Chr", "2 Chr", "2.Chr", "2. Chr",
            "2Chronik", "2 Chronik", "2.Chronik", "2. Chronik", 
            "2Chronika", "2 Chronika", "2.Chronika", "2. Chronika", 
            "2Chroniken", "2 Chroniken", "2.Chroniken", "2. Chroniken"
        }),

        new BookInfo(Book.Ezra, new List<ushort>()
        {
            0,
            11, 70, 13, 24, 17, 22, 28, 36, 15, 44
        }, new List<string>() { "Esra", "Esrah", "Ezra", "Ezrah" }),

        new BookInfo(Book.Nehemiah, new List<ushort>()
        {
            0,
            11, 20, 38, 17, 19, 19, 72, 18, 37, 40,
            36, 47, 31
        }, new List<string>() { "Neh", "Nehemia" }),

        new BookInfo(Book.Esther, new List<ushort>()
        {
            0,
            22, 23, 15, 17, 14, 14, 10, 17, 32, 03
        }, new List<string>() { "Est", "Ester", "Esther" }),

        new BookInfo(Book.Job, new List<ushort>()
        {
            0,
            22, 13, 26, 21, 27, 30, 21, 22, 35, 22,
            20, 25, 28, 22, 35, 22, 16, 21, 29, 29,
            34, 30, 17, 25, 06, 14, 23, 28, 25, 31,
            40, 22, 33, 37, 16, 33, 24, 41, 30, 32,
            26, 17
        }, new List<string>() { "Hb", "Hio", "Hiob" }),

        new BookInfo(Book.Psalms, new List<ushort>()
        {
            0,
            06, 12, 09, 09, 13, 11, 18, 10, 21, 18,
            07, 09, 06, 07, 05, 11, 15, 51, 15, 10,
            14, 32, 06, 10, 22, 12, 14, 09, 11, 13,
            25, 11, 22, 23, 28, 13, 40, 23, 14, 18,
            14, 12, 05, 27, 18, 12, 10, 15, 21, 23,
            21, 11, 07, 09, 24, 14, 12, 12, 18, 14,
            09, 13, 12, 11, 14, 20, 08, 36, 37, 06,
            24, 20, 28, 23, 11, 13, 21, 72, 13, 20,
            17, 08, 19, 13, 14, 17, 07, 19, 53, 17,
            16, 16, 05, 23, 11, 13, 12, 09, 09, 05,
            08, 29, 22, 35, 45, 48, 43, 14, 31, 07,
            10, 10, 09, 08, 18, 19, 02, 29, 176, 07,
            08, 09, 04, 08, 05, 06, 05, 06, 08, 08,
            03, 18, 03, 03, 21, 26, 09, 08, 24, 14,
            10, 08, 12, 15, 21, 10, 20, 14, 09, 06
        }, new List<string>() { "Ps", "Psa", "Psalm", "Psalmen" }),

        new BookInfo(Book.Proverbs, new List<ushort>()
        {
            0,
            33, 22, 35, 27, 23, 35, 27, 36, 18, 32,
            31, 28, 25, 35, 33, 33, 28, 24, 29, 30,
            31, 29, 35, 34, 28, 28, 27, 28, 27, 33,
            31
        }, new List<string>() { "Spr", "Sprüche" }),

        new BookInfo(Book.Ecclesiastes, new List<ushort>()
        {
            0,
            18, 26, 22, 17, 19, 12, 29, 17, 18, 20,
            10, 14
        }, new List<string>() { "Pred", "Prediger", "Koh", "Kohelet", "Ekk", "Ekkl" }),

        new BookInfo(Book.SongOfSongs, new List<ushort>()
        {
            0,
            17, 17, 11, 16, 16, 12, 14, 14
        }, new List<string>() { "Hld", "Hohelied", "Hohelied der Liebe", "Lied der Lieder" }),

        new BookInfo(Book.Isaiah, new List<ushort>()
        {
            0,
            31, 22, 26, 06, 30, 13, 25, 23, 20, 34,
            16, 06, 22, 32, 09, 14, 14, 07, 25, 06,
            17, 25, 18, 23, 12, 21, 13, 29, 24, 33,
            09, 20, 24, 17, 10, 22, 38, 22, 08, 31,
            29, 25, 28, 28, 25, 13, 15, 22, 26, 11,
            23, 15, 12, 17, 13, 12, 21, 14, 21, 22,
            11, 12, 19, 11, 25, 24
        }, new List<string>() { "Jes", "Jesaja"}),

        new BookInfo(Book.Jeremiah, new List<ushort>()
        {
            0,
            19, 37, 25, 31, 31, 30, 34, 23, 25, 25,
            23, 17, 27, 22, 21, 21, 27, 23, 15, 18,
            14, 30, 40, 10, 38, 24, 22, 17, 32, 24,
            40, 44, 26, 22, 19, 32, 21, 28, 18, 16,
            18, 22, 13, 30, 05, 28, 07, 47, 39, 46,
            64, 34
        }, new List<string>() { "Jer", "Jeremia" }),

        new BookInfo(Book.Lamentations, new List<ushort>()
        {
            0,
            22, 22, 66, 22, 22
        }, new List<string>() { "Klg", "Klagelieder" }),

        new BookInfo(Book.Ezekiel, new List<ushort>()
        {
            0,
            28, 10, 27, 17, 17, 14, 27, 18, 11, 22,
            25, 28, 23, 23, 08, 63, 24, 32, 14, 44,
            37, 31, 49, 27, 17, 21, 36, 26, 21, 26,
            18, 32, 33, 31, 15, 38, 28, 23, 29, 49,
            26, 20, 27, 31, 25, 24, 23, 35
        }, new List<string>() { "Hes", "Hesekiel" }),

        new BookInfo(Book.Daniel, new List<ushort>()
        {
            0,
            21, 49, 33, 34, 30, 29, 28, 27, 27, 21, // Buch (nicht HTML!): 6,1 fängt noch in Kap 5 an; 11,1 in Kap 10 
            45, 13                                  
        }, new List<string>() { "Dan", "Daniel" }),

        new BookInfo(Book.Hosea, new List<ushort>()
        {
            0,
            09, 25, 05, 19, 15, 11, 16, 14, 17, 15,
            11, 15, 15, 10
        }, new List<string>() { "Hos", "Hosea" }),

        new BookInfo(Book.Joel, new List<ushort>()
        {
            0,
            20, 27, 05, 21
        }, new List<string>() { "Joe", "Joel" }),

        new BookInfo(Book.Amos, new List<ushort>()
        {
            0,
            15, 16, 15, 13, 27, 14, 17, 14, 15
        }, new List<string>() { "Am", "Amos" }),

        new BookInfo(Book.Obadiah, new List<ushort>()
        {
            0,
            21
        }, new List<string>() { "Obd", "Obadja" }),

        new BookInfo(Book.Jonah, new List<ushort>()
        {
            0,
            16, 11, 10, 11
        }, new List<string>() { "Jona", "Jonah" }),

        new BookInfo(Book.Micah, new List<ushort>()
        {
            0,
            16, 13, 12, 14, 14, 16, 20
        }, new List<string>() { "Mi", "Micha" }),

        new BookInfo(Book.Nahum, new List<ushort>()
        {
            0,
            14, 14, 19
        }, new List<string>() { "Nah", "Nahum" }),

        new BookInfo(Book.Habakkuk, new List<ushort>()
        {
            0,
            17, 20, 19
        }, new List<string>() { "Hab", "Habakuk", "Habakkuk" }),

        new BookInfo(Book.Zephaniah, new List<ushort>()
        {
            0,
            18, 15, 20
        }, new List<string>() { "Zef", "Zefania", "Zefanja" }),

        new BookInfo(Book.Haggai, new List<ushort>()
        {
            0,
            15, 23
        }, new List<string>() { "Hag", "Haggai" }),

        new BookInfo(Book.Zechariah, new List<ushort>()
        {
            0,
            17, 17, 10, 14, 11, 15, 14, 23, 17, 12,
            17, 14, 09, 21
        }, new List<string>() { "Sac", "Sach", "Sacharja", "Sacharia" }),

        new BookInfo(Book.Malachi, new List<ushort>()
        {
            0,
            14, 17, 24
        }, new List<string>() { "Mal", "Maleachi" }),

        new BookInfo(Book.Matthew, new List<ushort>()
        {
            0,
            25, 23, 17, 25, 48, 34, 29, 34, 38, 42,
            30, 50, 58, 36, 39, 28, 27, 35, 30, 34,
            46, 46, 39, 51, 46, 75, 66, 20
        }, new List<string>() { "Mt", "Mtt", "Matthäus" }),

        new BookInfo(Book.Mark, new List<ushort>()
        {
            0,
            45, 28, 35, 41, 43, 56, 37, 38, 50, 52,
            33, 44, 37, 72, 47, 20
        }, new List<string>() { "Mk", "Mrk", "Markus" }),

        new BookInfo(Book.Luke, new List<ushort>()
        {
            0,
            80, 52, 38, 44, 39, 49, 50, 56, 62, 42,
            54, 59, 35, 35, 32, 31, 37, 43, 48, 47,
            38, 71, 56, 53
        }, new List<string>() { "Lk", "Lks", "Lukas" }),

        new BookInfo(Book.John, new List<ushort>()
        {
            0,
            51, 25, 36, 54, 47, 71, 53, 59, 41, 42,
            57, 50, 38, 31, 27, 33, 26, 40, 42, 31,
            25
        }, new List<string>() { "Jo", "Joh", "Johannes" }),

        new BookInfo(Book.Acts, new List<ushort>()
        {
            0,
            26, 47, 26, 37, 42, 15, 60, 40, 43, 48,
            30, 25, 52, 28, 41, 40, 34, 28, 40, 38,
            40, 30, 35, 27, 27, 32, 44, 31          // Kap 27: In alter NeÜ 45
        }, new List<string>() { "Apg", "Apostelgeschichte" }),

        new BookInfo(Book.Romans, new List<ushort>()
        {
            0,
            32, 29, 31, 25, 21, 23, 25, 39, 33, 21,
            36, 21, 14, 23, 33, 27
        }, new List<string>() { "Ro", "Roe", "Roemer", "Roemerbrief", "Rö", "Röm", "Römer", "Römerbrief" }),

        new BookInfo(Book.FirstCorinthians, new List<ushort>()
        {
            0,
            31, 16, 23, 21, 13, 20, 40, 13, 27, 33, // 11,1 schon in Kap 10
            34, 31, 13, 40, 58, 24
        }, new List<string>() 
        {
            "1Kor", "1 Kor", "1.Kor", "1. Kor", 
            "1Korinther", "1 Korinther", "1.Korinther", "1. Korinther",
        }),
        
        new BookInfo(Book.SecondCorinthians, new List<ushort>()
        {
            0,
            24, 17, 18, 18, 21, 18, 16, 24, 15, 18,
            33, 21, 13
        }, new List<string>() 
        { 
            "2Kor", "2 Kor", "2.Kor", "2. Kor", 
            "2Korinther", "2 Korinther", "2.Korinther", "2. Korinther",
        }),

        new BookInfo(Book.Galatians, new List<ushort>()
        {
            0,
            24, 21, 29, 31, 26, 18
        }, new List<string>() { "Gal", "Galater" }),

        new BookInfo(Book.Ephesians, new List<ushort>()
        {
            0,
            23, 22, 21, 32, 33, 24
        }, new List<string>() { "Eph", "Epheser" }),

        new BookInfo(Book.Philippians, new List<ushort>()
        {
            0,
            30, 30, 21, 23
        }, new List<string>() { "Phil", "Philipper" }),

        new BookInfo(Book.Colossians, new List<ushort>()
        {
            0,
            29, 23, 25, 18
        }, new List<string>() { "Kol", "Kolosser" }),

        new BookInfo(Book.FirstThessalonians, new List<ushort>()
        {
            0,
            10, 20, 13, 18, 28
        }, new List<string>() 
        { 
            "1Th", "1 Th", "1.Th", "1. Th", 
            "1Thes", "1 Thes", "1.Thes", "1. Thes", 
            "1Thessalonicher", "1 Thessalonicher", "1.Thessalonicher", "1. Thessalonicher",
            "1Thessaloniker", "1 Thessaloniker", "1.Thessaloniker", "1. Thessaloniker",
        }),

        new BookInfo(Book.SecondThessalonians, new List<ushort>()
        {
            0,
            12, 17, 18
        }, new List<string>() 
        { 
            "2Th", "2 Th", "2.Th", "2. Th", 
            "2Thes", "2 Thes", "2.Thes", "2. Thes", 
            "2Thessalonicher", "2 Thessalonicher", "2.Thessalonicher", "2. Thessalonicher",
            "2Thessaloniker", "2 Thessaloniker", "2.Thessaloniker", "2. Thessaloniker",
        }),

        new BookInfo(Book.FirstTimothy, new List<ushort>()
        {
            0,
            20, 15, 16, 16, 25, 21
        }, new List<string>() 
        { 
            "1Ti", "1 Ti", "1.Ti", "1. Ti", 
            "1Tim", "1 Tim", "1.Tim", "1. Tim",
            "1Timo", "1 Timo", "1.Timo", "1. Timo",
            "1Timotheus", "1 Timotheus", "1.Timotheus", "1. Timotheus",
        }),

        new BookInfo(Book.SecondTimothy, new List<ushort>()
        {
            0,
            18, 26, 17, 22
        }, new List<string>() 
        { 
            "2Ti", "2 Ti", "2.Ti", "2. Ti", 
            "2Tim", "2 Tim", "2.Tim", "2. Tim",
            "2Timo", "2 Timo", "2.Timo", "2. Timo",
            "2Timotheus", "2 Timotheus", "2.Timotheus", "2. Timotheus",
        }),

        new BookInfo(Book.Titus, new List<ushort>()
        {
            0,
            16, 15, 15
        }, new List<string>() { "Tit", "Titus" }),

        new BookInfo(Book.Philemon, new List<ushort>()
        {
            0,
            25
        }, new List<string>() { "Phlm", "Philemon" }),

        new BookInfo(Book.Hebrews, new List<ushort>()
        {
            0,
            14, 18, 19, 16, 14, 20, 28, 13, 28, 39,
            40, 29, 25
        }, new List<string>() { "Heb", "Hebräer", "Hebraeer" }),

        new BookInfo(Book.James, new List<ushort>()
        {
            0,
            27, 26, 18, 17, 20
        }, new List<string>() { "Jak", "Jakobus" }),

        new BookInfo(Book.FirstPeter, new List<ushort>()
        {
            0,
            25, 25, 22, 19, 14
        }, new List<string>() 
        { 
            "1Pet", "1 Pet", "1.Pet", "1. Pet", 
            "1Petrus", "1 Petrus", "1.Petrus", "1. Petrus",
        }),

        new BookInfo(Book.SecondPeter, new List<ushort>()
        {
            0,
            21, 22, 18
        }, new List<string>() 
        { 
            "2Pet", "2 Pet", "2.Pet", "2. Pet", 
            "2Petrus", "2 Petrus", "2.Petrus", "2. Petrus",
        }),

        new BookInfo(Book.FirstJohn, new List<ushort>()
        {
            0,
            10, 29, 24, 21, 21
        }, new List<string>() 
        { 
            "1Joh", "1 Joh", "1.Joh", "1. Joh", 
            "1Johannes", "1 Johannes", "1.Johannes", "1. Johannes",
        }),

        new BookInfo(Book.SecondJohn, new List<ushort>()
        {
            0,
            13
        }, new List<string>()
        { 
            "2Joh", "2 Joh", "2.Joh", "2. Joh", 
            "2Johannes", "2 Johannes", "2.Johannes", "2. Johannes",
        }),

        new BookInfo(Book.ThirdJohn, new List<ushort>()
        {
            0,
            15
        }, new List<string>() 
        { 
            "3Joh", "3 Joh", "3.Joh", "3. Joh", 
            "3Johannes", "3 Johannes", "3.Johannes", "3. Johannes",
        }),

        new BookInfo(Book.Jude, new List<ushort>()
        {
            0,
            25
        }, new List<string>() { "Jud", "Judas" }),
        
        new BookInfo(Book.Revelation, new List<ushort>()
        {
            0,
            20, 29, 22, 11, 14, 17, 17, 13, 21, 11,
            19, 18, 18, 20, 08, 21, 18, 24, 21, 15,
            27, 21
        }, new List<string>() { "Offb", "Off", "Offenbarung" }),

    };

    public IReadOnlyList<BookInfo> Books => _books;
}