using bursaKasder.HelperClasses;
using bursaKasder.Models;
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
            var structureData = _context.BKD_OrganizationalStructure.Where(s => s.OS_Status == 0).ToList();

            if (structureData == null)
            {
                return NotFound("Teşkilat Yapısı Hakkında Bilgi bulunamadı.");
            }
            return View(structureData);
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
            var newsData = _context.BKD_NewsFromUs.Where(s => s.newsU_Status == 0).ToList();

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
            var eventData = _context.BKD_Events.Where(s => s.ev_Status == 0).ToList();

            if (eventData == null) { return NotFound(); }

            return View(eventData);
        }

        [HttpPost]
        public IActionResult ContactUsAdd(BKD_ContactUs cUS)
        {
            cUS.conU_DateMessage = DateTime.Now;
            cUS.conU_Status = 0;

            _context.BKD_ContactUs.Add(cUS);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}
