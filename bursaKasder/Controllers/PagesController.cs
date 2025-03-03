using Microsoft.AspNetCore.Mvc;

namespace bursaKasder.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult KasderStructure()
        {
            return View();
        }

        public IActionResult contact_us()
        {
            return View();
        }

        public IActionResult about_us()
        {
            return View();
        }
        
        public IActionResult news_FromUs()
        {
            return View();
        }
        public IActionResult news_FromKast()
        {
            return View();
        }
        public IActionResult events()
        {
            return View();
        }
    }
}
