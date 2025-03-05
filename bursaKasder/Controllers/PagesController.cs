using bursaKasder.HelperClasses;
using bursaKasder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bursaKasder.Controllers
{
    public class PagesController : Controller
    {

        private readonly Get_AdminService _getAdminService;
        private readonly Post_AdminService _postAdminService;
        private readonly DbContextManager _context;

        public PagesController(Post_AdminService postAdminService, Get_AdminService getAdminService, DbContextManager context)
        {
            _context = context;
            _postAdminService = postAdminService;
            _getAdminService = getAdminService;


        }
        public IActionResult Index()
        {
            var MainData = _context.BKD_OrganizationInformation.FirstOrDefault();

            if (MainData == null)
            {
                return NotFound("Veri bulunamadı.");
            }
            return View();
        }
        public IActionResult KasderStructure()
        {
            return View();
        }

        public IActionResult contact_us()
        {
            var contactData = _context.BKD_Contact.FirstOrDefault();

            if (contactData == null)
            {
                return NotFound("Hakkımızda bilgisi bulunamadı.");
            }
            return View(contactData);
        }

        public IActionResult about_us()
        {
           var aboutData = _context.BKD_About.FirstOrDefault();

            if (aboutData == null)
            {
                return NotFound("Hakkımızda bilgisi bulunamadı.");
            }

            var MainData = _context.BKD_OrganizationInformation.FirstOrDefault();

            ViewBag.MainPhoto = MainData.OI_Indexphoto;

            return View(aboutData);
        }
        
        public IActionResult news_FromUs()
        {
            var newsData = _context.BKD_NewsFromUs.ToList();

            if (newsData == null)
            {
                return NotFound("Veri bulunamadı");
            }
            return View(newsData);
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
