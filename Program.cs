using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

if(args.Length == 0)
{

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Please provide input directory");
    Console.ResetColor(); 
    return;
}

Console.WriteLine("Merge in progress...");

var inputDirectory = args[0];
string[] pdfFiles = GetPdfFiles(inputDirectory);

using var outputDocument = new PdfDocument();

foreach (string pdfFile in pdfFiles)
{
    PdfDocument inputDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);

    for (int pageIndex = 0; pageIndex < inputDocument.PageCount; pageIndex++)
    {
        PdfPage page = inputDocument.Pages[pageIndex];
        outputDocument.AddPage(page);
    }
}

var outputDirectory = $"{inputDirectory}/output";
Directory.CreateDirectory(outputDirectory);
outputDocument.Save($"{outputDirectory}/merged.pdf");
Console.WriteLine($"Done. Output file saved in {outputDirectory}");

string[] GetPdfFiles(string inputDirectory)
{
    return Directory
        .GetFiles(inputDirectory, "*.pdf")
        .OrderBy(x => x)
        .ToArray();
}