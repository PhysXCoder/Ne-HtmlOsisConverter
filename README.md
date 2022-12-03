# NeÜHtmlOsisConverter

FOSS program to convert *Neue evangelistische Übersetzung (NeÜ)* of the Bible to OSIS format. Input is HTML files of the Translator Mr. Vanheiden. Output is a single OSIS XML file used by Crosswire Bible Society's SWORD Project to provide the NeÜ Bible.

Licensed under GPLv3, built with dotnet 6.0 on Ubuntu 22.04.1.

## Usage
Start the program with 2 command line parameters:
1. Path to HTML folder containing the NeÜ HTML files
2. Output file path

Example: <code>NeueHtmlOsisConverter /path/to/NeÜ gerneue.osis.xml</code>

## Links
- Online version of the NeÜ: [https://neue.derbibelvertrauen.de/](https://neue.derbibelvertrauen.de/)
- SWORD project: [https://www.crosswire.org/sword/](https://www.crosswire.org/sword/)
- OSIS format: [https://crosswire.org/osis/](https://crosswire.org/osis/)
- Program to read the NeÜ on Android devices: AndBible [https://andbible.github.io/](https://andbible.github.io/)
- Programs to read the NeÜ on Linux/Windows: 
  - Bibletime: [https://bibletime.info/](https://bibletime.info/), 
  - Xiphos: [https://xiphos.org/](https://xiphos.org/), 
  - Ezra: [https://ezrabibleapp.net/](https://ezrabibleapp.net/) 
  - and many more...

## Result

Input HTML file to the left, generated output on the right: ![Input and Output files](InputOutput.png)
gerneue module viewed in Bibletime: ![Bibletime with NeÜ](Result.png)