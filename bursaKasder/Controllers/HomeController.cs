using System.Diagnostics;
using bursaKasder.HelperClasses;
using bursaKasder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bursaKasder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string? key = Environment.GetEnvironmentVariable("AES_KEY"); // AES ŞİFRELEME ANAHTARI
        private readonly DbContextManager _context;                                                           // 
        public HomeController(ILogger<HomeController> logger, DbContextManager context)
        {
            _logger = logger;
            _context= context;
        }

        public IActionResult Index()
        {

            ViewData["UserName"] = HttpContext.Session.GetString("Admin_name");
            ViewData["UserSurname"] = HttpContext.Session.GetString("Admin_surname");

            var IndexData = _context.BKD_OrganizationInformation.FirstOrDefault();

            if (IndexData == null)
            {
                return NotFound("Hakkımızda bilgisi bulunamadı.");
            }
            return View(IndexData);
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
