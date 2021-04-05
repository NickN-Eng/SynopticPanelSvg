using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Pdf;
using SynopticPanelSvg.Pdf;
using System.Reflection;

namespace SynopticPanelSvg.PdfConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Gets the TestPdfsAndOutputs folder path, which is located at: SynopticPanelSvg\SynopticPanelSvg.PdfConverterApp\TestPdfsAndOutput
            var executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pdfConverterRootPath = Directory.GetParent(executingPath).Parent.FullName;
            var workingFolder = Path.Combine(pdfConverterRootPath, "TestPdfsAndOutput");

            //Gets the test pdf document
            var pdfPath = Path.Combine(workingFolder, "DocumentWithAnnots.pdf");
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdfPath));

            //For the first page of the pdf, extract annotations and populate an synoptic panel svg
            var page = pdfDoc.GetFirstPage();
            var pdfAnnotReader = new PdfAnnotationReader();
            var doc = pdfAnnotReader.ExtractFromPage(page);
            doc.Generate();

            //Writes this svg to disk
            var svgPath = Path.Combine(workingFolder, "ResultV1.svg");
            doc.WriteTo(svgPath);
            Console.ReadKey();
        }
    }
}
