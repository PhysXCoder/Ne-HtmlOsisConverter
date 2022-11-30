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

/// <summary>
/// Names taken from OSIS 2.1.1 User's Manual, which refers to SBL Handbook of Style (Society of Biblical Literature)
/// </summary>
public class OsisNamingScheme : INamingScheme
{
    public OsisNamingScheme()
    { }

    public string this[Book book] => OsisNamesByBook[book];

    protected IDictionary<Book, string> OsisNamesByBook = new Dictionary<Book, string>() 
    {
        {Book.Genesis, "Gen"},
        {Book.Exodus, "Exod"},
        {Book.Leviticus, "Lev"},
        {Book.Numbers, "Num"},
        {Book.Deuteronomy, "Deut"},
        {Book.Joshua, "Josh"},
        {Book.Judges, "Judg"},
        {Book.Ruth, "Ruth"},
        {Book.FirstSamuel, "1Sam"},
        {Book.SecondSamuel, "2Sam"},
        {Book.FirstKings, "1Kgs"},
        {Book.SecondKings, "2Kgs"},
        {Book.FirstChronicles, "1Chr"},
        {Book.SecondChronicles, "2Chr"},
        {Book.Ezra, "Ezra"},
        {Book.Nehemiah, "Neh"},
        {Book.Esther, "Esth"},
        {Book.Job, "Job"},
        {Book.Psalms, "Ps"},
        {Book.Proverbs, "Prov"},
        {Book.Ecclesiastes, "Eccl"},
        {Book.SongOfSongs, "Song"},
        {Book.Isaiah, "Isa"},
        {Book.Jeremiah, "Jer"},
        {Book.Lamentations, "Lam"},
        {Book.Ezekiel, "Ezek"},
        {Book.Daniel, "Dan"},
        {Book.Hosea, "Hos"},
        {Book.Joel, "Joel"},
        {Book.Amos, "Amos"},
        {Book.Obadiah, "Obad"},
        {Book.Jonah, "Jonah"},
        {Book.Micah, "Mic"},
        {Book.Nahum, "Nah"},
        {Book.Habakkuk, "Hab"},
        {Book.Zephaniah, "Zeph"},
        {Book.Haggai, "Hag"},
        {Book.Zechariah, "Zech"},
        {Book.Malachi, "Mal"},
        {Book.Matthew, "Matt"},
        {Book.Mark, "Mark"},
        {Book.Luke, "Luke"},
        {Book.John, "John"},
        {Book.Acts, "Acts"},
        {Book.Romans, "Rom"},
        {Book.FirstCorinthians, "1Cor"},
        {Book.SecondCorinthians, "2Cor"},
        {Book.Galatians, "Gal"},
        {Book.Ephesians, "Eph"},
        {Book.Philippians, "Phil"},
        {Book.Colossians, "Col"},
        {Book.FirstThessalonians, "1Thess"},
        {Book.SecondThessalonians, "2Thess"},
        {Book.FirstTimothy, "1Tim"},
        {Book.SecondTimothy, "2Tim"},
        {Book.Titus, "Titus"},
        {Book.Philemon, "Phlm"},
        {Book.Hebrews, "Heb"},
        {Book.James, "Jas"},
        {Book.FirstPeter, "1Pet"},
        {Book.SecondPeter, "2Pet"},
        {Book.FirstJohn, "1John"},
        {Book.SecondJohn, "2John"},
        {Book.ThirdJohn, "3John"},
        {Book.Jude, "Jude"},
        {Book.Revelation, "Rev"},
        {Book.Baruch, "Bar"},
        {Book.DanielGreek, "AddDan"},
        {Book.BelAndTheDragon, "Bel"},
        {Book.SongOfTheThreeYoungMen, "SgThree"},
        {Book.Susanna, "Sus"},
        {Book.FirstEsdras, "1Esd"},
        {Book.SecondEsdras, "2Esd"},
        {Book.EstherGreek, "AddEsth"},
        {Book.LetterOfJeremiah, "EpJer"},
        {Book.Judith, "Jdt"},
        {Book.FirstMaccabees, "1Macc"},
        {Book.SecondMaccabees, "2Macc"},
        {Book.ThirdMaccabees, "3Macc"},
        {Book.FourthMaccabees, "4Macc"},
        {Book.PrayerOfManasseh, "PrMan"},
        {Book.Sirach, "Sir"},
        {Book.Tobit, "Tob"},
        {Book.WisdomOfSolomon, "Wis"},
    };
}
