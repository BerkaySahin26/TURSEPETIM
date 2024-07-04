using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Controllers
{
    public class PdfReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaticPdfReport()  // Statik bir PDF raporu oluşturur ve tarayıcıya indirme olarak sunar
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreports/" + "dosya1.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);  // Document nesnesi oluşturulur, PageSize.A4 standart A4 boyutları kullanılır
            PdfWriter.GetInstance(document, stream);

            document.Open();
            // Belgeye bir başlık ekler
            Paragraph paragraph = new Paragraph("TurSepetim Rezervasyon Pdf Raporu");

            document.Add(paragraph);
            document.Close();
            return File("/pdfreports/dosya1.pdf", "application/pdf", "dosya1.pdf");
        }
        public IActionResult StaticCustomerReport() // Statik bir müşteri raporu oluşturur ve tarayıcıya indirme olarak suna
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreports/" + "dosya2.pdf");
            var stream = new FileStream(path, FileMode.Create);  // Dosyayı oluşturmak için FileStream kullanılır

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);

            document.Open();

            PdfPTable pdfPTable = new PdfPTable(3);
            // Tabloya başlık hücreleri eklenir
            pdfPTable.AddCell("Misafir Adı");
            pdfPTable.AddCell("Misafir Soyadı");
            pdfPTable.AddCell("Misafir TC");

            pdfPTable.AddCell("Berkay");
            pdfPTable.AddCell("Şahin");
            pdfPTable.AddCell("11111111110");

            pdfPTable.AddCell("Emre");
            pdfPTable.AddCell("Sarıyer");
            pdfPTable.AddCell("22222222222");

            pdfPTable.AddCell("Orkun");
            pdfPTable.AddCell("Şahin");
            pdfPTable.AddCell("44444444445");

            document.Add(pdfPTable);

            document.Close();
            return File("/pdfreports/dosya2.pdf", "application/pdf", "dosya2.pdf");
        }
    }
}
