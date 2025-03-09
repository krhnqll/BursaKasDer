using bursaKasder.HelperClasses;
using bursaKasder.Models;
using bursaKasder.Models.EntityModels;
using bursaKasder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

        [HttpGet]
        public IActionResult showNewsDetail(int? id)
        {
            var postDetail = _context.BKD_NewsFromUs.FirstOrDefault(n => n.newsU_ID == id);
            if (postDetail == null)
            {
                return NotFound();
            }
            return View(postDetail);
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

        [HttpGet]
        public IActionResult showEventDetail(int id)
        {
            var eventDetail = _context.BKD_Events.Where(s => s.ev_Status == 0 ).Include(e => e.EventPhotos).FirstOrDefault(e => e.ev_ID == id);

            if (eventDetail == null)
            {
                return NotFound();
            }

            var viewModel = new EventViewModel
            {
                ev_ID = eventDetail.ev_ID,
                ev_Title = eventDetail.ev_Title,
                ev_Content = eventDetail.ev_Content,
                ev_Date = eventDetail.ev_Date,
                ev_MainPhoto = eventDetail.ev_MainPhoto,
                EventPhotos = eventDetail.EventPhotos.Select(p => p.evP_Photo).ToList()
            };

            return View(viewModel);
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
