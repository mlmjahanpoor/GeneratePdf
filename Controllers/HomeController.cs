using CreateFactor.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Index()
        {
            var root = _environment.WebRootPath;
            var PdfBytes = _generatePdf.GetPDF("<h1>This is my title!</h1>");
            Utilities utilities = new Utilities(_environment);
            utilities.ByteArrayToFile("factor1.pdf",PdfBytes);

            return Ok(new { IsSuccess = true });
        }
    }
}
