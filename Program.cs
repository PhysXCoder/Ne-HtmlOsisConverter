/*
HtmlOsisConverter - Converts NeÜ Bible HTML files to OSIS XML.
Copyright (C) 2022-2024 PhysXCoder

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
using NeueHtmlOsisConverter.Bible.Canons;
using NeueHtmlOsisConverter.Bible.NamingSchemas;

namespace NeueHtmlOsisConverter.Converter;

public static class Program 
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Usage: HtmlOsisConverter pathToHtmlFolder outputFilenamePath");
        string htmlFolderName = args[0];
        string outputFilename = args[1];        

        DirectoryInfo htmlFolder = new DirectoryInfo(htmlFolderName);
        FileInfo outputFile = new FileInfo(outputFilename);

        Console.WriteLine();
        Console.WriteLine($"Input path: {htmlFolder.FullName}");
        Console.WriteLine($"Output file: {outputFile.FullName}");
        Console.WriteLine();
        Console.WriteLine();

        ICanon canon = new NeÜCanon();
        INamingScheme namingScheme = new OsisNamingScheme(); 
        IFilenames filenames = new HtmlFilenames();  
        string workName = "GerNeUe";
        string title = "NeÜ - Neue evangelistische Übersetzung";

        Convert(htmlFolder, outputFile, title, workName, filenames, canon, namingScheme);

        Console.WriteLine();
        Console.WriteLine($"Program finished");        
    }

    public static void Convert(DirectoryInfo htmlFolder, FileInfo outputFile,
        string title, string workName, 
        IFilenames filenames, ICanon canon, INamingScheme namingScheme)    
    {
        Converter converter = new Converter(
            canon, filenames, namingScheme, title, workName);

        converter.Convert(htmlFolder, outputFile);
    }
}