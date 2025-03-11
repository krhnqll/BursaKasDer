using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using bursaKasder.HelperClasses;
using bursaKasder.Models;
using bursaKasder.Models.EntityModels;
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
            _context = context;
        }

        public IActionResult Index()
        {
            // Organizasyon bilgisini çek
            var OIData = _context.BKD_OrganizationInformation.FirstOrDefault();

            if (OIData != null)
            {
                // Ziyaretçi sayacını artır
                OIData.Counter = (OIData.Counter ?? 0) + 1;
                _context.SaveChanges();
            }

            var NEWSData = _context.BKD_NewsFromUs.Where(s => s.newsU_Status == 0).OrderByDescending(n => n.newsU_ID).ToList();
            var OSData = _context.BKD_OrganizationalStructure.Where(s => s.OS_Status == 0).OrderBy(n => n.OS_ID).Take(3).ToList();
            var AnnouncementsData = _context.BKD_Announcements.Where(s => s.ann_Status == 0).OrderByDescending(n => n.ann_ID).Take(3).ToList();
            var EventData = _context.BKD_Events.Where(s => s.ev_Status == 0).OrderByDescending(n => n.ev_ID).ToList();

            var model = new IndexViewModel
            {
                DataOI = OIData,
                ListOS = OSData,
                ListNews = NEWSData,
                ListAnnouncements = AnnouncementsData,
                ListEvents = EventData,
                 // Sayaç bilgisini View'e gönder
            };

            return View(model);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
