using CreateFactor.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Text;
using Wkhtmltopdf.NetCore;

namespace CreateFactor.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        private readonly IGeneratePdf _generatePdf;
        public HomeController(IGeneratePdf generatePdf, IWebHostEnvironment environment)
        {
            _generatePdf = generatePdf;
            _environment = environment;
        }
        [HttpPost]
        public IActionResult Index(string company, string email, string side, string number)
        {

            var y = new
            {
                Company = company,
                Email = email,
                Side = side,
                Number = number
            };

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(y.ToString(), QRCodeGenerator.ECCLevel.Q);
            AsciiQRCode qrCode = new AsciiQRCode(_qrCodeData);

            var pngBytes = new PngByteQRCode(_qrCodeData);

            var x = pngBytes.GetGraphic(20);

            System.IO.File.WriteAllBytes(Path.Combine(_environment.WebRootPath, "qr1.png"), x);



            Wkhtmltopdf.NetCore.ConvertOptions options = new ConvertOptions();

            options.EnableForms = true;
            options.PageSize = Wkhtmltopdf.NetCore.Options.Size.A4;
            options.PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait;
            options.PageMargins = new Wkhtmltopdf.NetCore.Options.Margins(0, 0, 0, 0);


            _generatePdf.SetConvertOptions(options);


            var PdfBytes = _generatePdf.GetPDF($"<img  src='{Path.Combine(_environment.WebRootPath, "test.png")}' style='margin:0;padding:0;display:flex;width:100%;left:0;right:0;position:fixed;'/><img src='{Path.Combine(_environment.WebRootPath, "qr1.png")}' style='width:200px;margin-top:650px'/>");


            System.IO.File.WriteAllBytes(Path.Combine(_environment.WebRootPath, "mypdf.pdf"), PdfBytes);


            return Ok(new { IsSuccess = true, bytes = PdfBytes });
        }


        [HttpPost]
        public IActionResult Create(string txtQRCode)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            AsciiQRCode qrCode = new AsciiQRCode(_qrCodeData);

            var pngBytes = new PngByteQRCode(_qrCodeData);

            var x = pngBytes.GetGraphic(20);

            System.IO.File.WriteAllBytes(Path.Combine(_environment.WebRootPath, "qr1.png"), x);

            return Ok(x);
        }

    }
}
