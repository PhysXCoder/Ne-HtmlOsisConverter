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

namespace NeueHtmlOsisConverter.Bible.NamingSchemas;

public class Usfm3NamingScheme : INamingScheme
{    
    public Usfm3NamingScheme()
    { }

    public string this[Book book] => Usfm3NamesByBook[book];

    protected IDictionary<Book, string> Usfm3NamesByBook = new Dictionary<Book, string>() 
    {
        {Book.Genesis, "GEN"},
        {Book.Exodus, "EXO"},
        {Book.Leviticus, "LEV"},
        {Book.Numbers, "NUM"},
        {Book.Deuteronomy, "DEU"},
        {Book.Joshua, "JOS"},
        {Book.Judges, "JDG"},
        {Book.Ruth, "RUT"},
        {Book.FirstSamuel, "1SA"},
        {Book.SecondSamuel, "2SA"},
        {Book.FirstKings, "1KI"},
        {Book.SecondKings, "2KI"},
        {Book.FirstChronicles, "1CH"},
        {Book.SecondChronicles, "2CH"},
        {Book.Ezra, "EZR"},
        {Book.Nehemiah, "NEH"},
        {Book.Esther, "EST"},
        {Book.Job, "JOB"},
        {Book.Psalms, "PSA"},
        {Book.Proverbs, "PRO"},
        {Book.Ecclesiastes, "ECC"},
        {Book.SongOfSongs, "SNG"},
        {Book.Isaiah, "ISA"},
        {Book.Jeremiah, "JER"},
        {Book.Lamentations, "LAM"},
        {Book.Ezekiel, "EZK"},
        {Book.Daniel, "DAN"},
        {Book.Hosea, "HOS"},
        {Book.Joel, "JOL"},
        {Book.Amos, "AMO"},
        {Book.Obadiah, "OBA"},
        {Book.Jonah, "JON"},
        {Book.Micah, "MIC"},
        {Book.Nahum, "NAM"},
        {Book.Habakkuk, "HAB"},
        {Book.Zephaniah, "ZEP"},
        {Book.Haggai, "HAG"},
        {Book.Zechariah, "ZEC"},
        {Book.Malachi, "MAL"},
        {Book.Matthew, "MAT"},
        {Book.Mark, "MRK"},
        {Book.Luke, "LUK"},
        {Book.John, "JHN"},
        {Book.Acts, "ACT"},
        {Book.Romans, "ROM"},
        {Book.FirstCorinthians, "1CO"},
        {Book.SecondCorinthians, "2CO"},
        {Book.Galatians, "GAL"},
        {Book.Ephesians, "EPH"},
        {Book.Philippians, "PHP"},
        {Book.Colossians, "COL"},
        {Book.FirstThessalonians, "1TH"},
        {Book.SecondThessalonians, "2TH"},
        {Book.FirstTimothy, "1TI"},
        {Book.SecondTimothy, "2TI"},
        {Book.Titus, "TIT"},
        {Book.Philemon, "PHM"},
        {Book.Hebrews, "HEB"},
        {Book.James, "JAS"},
        {Book.FirstPeter, "1PE"},
        {Book.SecondPeter, "2PE"},
        {Book.FirstJohn, "1JN"},
        {Book.SecondJohn, "2JN"},
        {Book.ThirdJohn, "3JN"},
        {Book.Jude, "JUD"},
        {Book.Revelation, "REV"},
        {Book.Tobit, "TOB"},
        {Book.Judith, "JDT"},
        {Book.EstherGreek, "ESG"},
        {Book.WisdomOfSolomon, "WIS"},
        {Book.Sirach, "SIR"},
        {Book.Baruch, "BAR"},
        {Book.LetterOfJeremiah, "LJE"},
        {Book.SongOfTheThreeYoungMen, "S3Y"},
        {Book.Susanna, "SUS"},
        {Book.BelAndTheDragon, "BEL"},
        {Book.FirstMaccabees, "1MA"},
        {Book.SecondMaccabees, "2MA"},
        {Book.ThirdMaccabees, "3MA"},
        {Book.FourthMaccabees, "4MA"},
        {Book.FirstEsdras, "1ES"},
        {Book.SecondEsdras, "2ES"},
        {Book.PrayerOfManasseh, "MAN"},
        {Book.Psalm151, "PS2"},
        {Book.Odae, "ODA"},
        {Book.PsalmsOfSolomon, "PSS"},
        {Book.ExtraMaterialA, "XXA"},
        {Book.ExtraMaterialB, "XXB"},
        {Book.ExtraMaterialC, "XXC"},
        {Book.ExtraMaterialD, "XXD"},
        {Book.ExtraMaterialE, "XXE"},
        {Book.ExtraMaterialF, "XXF"},
        {Book.ExtraMaterialG, "XXG"},
        {Book.EzraApocalypse, "EZA"},
        {Book.FifthEzra, "5EZ"},
        {Book.SixthEzra, "6EZ"},
        {Book.DanielGreek, "DAG"},
        {Book.Psalms152155, "PS3"},
        {Book.SecondBaruchApocalypse, "2BA"},
        {Book.LetterOfBaruch, "LBA"},
        {Book.Jubilees, "JUB"},
        {Book.Enoch, "ENO"},
        {Book.FirstMeqabyan, "1MQ"},
        {Book.SecondMeqabyan, "2MQ"},
        {Book.ThirdMeqabyan, "3MQ"},
        {Book.Reproof, "REP"},
        {Book.FourthBaruch, "4BA"},
        {Book.LetterToTheLaodiceans, "LAO"},
        {Book.FrontMatter, "FRT"},
        {Book.BackMatter, "BAK"},
        {Book.OtherMatter, "OTH"},
        {Book.IntroductionMatter, "INT"},
        {Book.Concordance, "CNC"},
        {Book.Glossary, "GLO"},
        {Book.TopicalIndex, "TDX"},
        {Book.NamesIndex, "NDX"},        
    };
}
