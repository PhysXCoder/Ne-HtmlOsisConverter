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

using NeueHtmlOsisConverter.Bible;

namespace NeueHtmlOsisConverter.Converter;

public class HtmlFilenames : IFilenames
{
    public static readonly IReadOnlyDictionary<Book, string> FilenameByBook = new Dictionary<Book, string>() 
    {
        {Book.Genesis, "1mo.html"},
        {Book.Exodus, "2mo.html"},
        {Book.Leviticus, "3mo.html"},
        {Book.Numbers, "4mo.html"},
        {Book.Deuteronomy, "5mo.html"},
        {Book.Joshua, "jos.html"},
        {Book.Judges, "ri.html"},
        {Book.Ruth, "rut.html"},
        {Book.FirstSamuel, "1sam.html"},
        {Book.SecondSamuel, "2sam.html"},
        {Book.FirstKings, "1koe.html"},
        {Book.SecondKings, "2koe.html"},
        {Book.FirstChronicles, "1chr.html"},
        {Book.SecondChronicles, "2chr.html"},
        {Book.Ezra, "esra.html"},
        {Book.Nehemiah, "neh.html"},
        {Book.Esther, "est.html"},
        {Book.Job, "hiob.html"},
        {Book.Psalms, "ps.html"},
        {Book.Proverbs, "spr.html"},
        {Book.Ecclesiastes, "pred.html"},
        {Book.SongOfSongs, "hl.html"},
        {Book.Isaiah, "jes.html"},
        {Book.Jeremiah, "jer.html"},
        {Book.Lamentations, "kla.html"},
        {Book.Ezekiel, "hes.html"},
        {Book.Daniel, "dan.html"},
        {Book.Hosea, "hos.html"},
        {Book.Joel, "joel.html"},
        {Book.Amos, "amos.html"},
        {Book.Obadiah, "obadja.html"},
        {Book.Jonah, "jona.html"},
        {Book.Micah, "mi.html"},
        {Book.Nahum, "nah.html"},
        {Book.Habakkuk, "hab.html"},
        {Book.Zephaniah, "zef.html"},
        {Book.Haggai, "hag.html"},
        {Book.Zechariah, "sach.html"},
        {Book.Malachi, "mal.html"},
        {Book.Matthew, "mt.html"},
        {Book.Mark, "mk.html"},
        {Book.Luke, "lk.html"},
        {Book.John, "jo.html"},
        {Book.Acts, "apg.html"},
        {Book.Romans, "roe.html"},
        {Book.FirstCorinthians, "1kor.html"},
        {Book.SecondCorinthians, "2kor.html"},
        {Book.Galatians, "gal.html"},
        {Book.Ephesians, "eph.html"},
        {Book.Philippians, "phil.html"},
        {Book.Colossians, "kol.html"},
        {Book.FirstThessalonians, "1thes.html"},
        {Book.SecondThessalonians, "2thes.html"},
        {Book.FirstTimothy, "1tim.html"},
        {Book.SecondTimothy, "2tim.html"},
        {Book.Titus, "tit.html"},
        {Book.Philemon, "phm.html"},
        {Book.Hebrews, "hebr.html"},
        {Book.James, "jak.html"},
        {Book.FirstPeter, "1pt.html"},
        {Book.SecondPeter, "2pt.html"},
        {Book.FirstJohn, "1jo.html"},
        {Book.SecondJohn, "2jo.html"},
        {Book.ThirdJohn, "3jo.html"},
        {Book.Jude, "jud.html"},
        {Book.Revelation, "off.html"},
    };

    public const string ForewordFilename = "index.html";
    

    IReadOnlyDictionary<Book, string> IFilenames.FilenameByBook => FilenameByBook;

    string IFilenames.ForewordFilename => ForewordFilename;
}
