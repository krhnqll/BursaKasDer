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


            var OIData = _context.BKD_OrganizationInformation.FirstOrDefault();
            var NEWSData = _context.BKD_NewsFromUs.OrderByDescending(n => n.newsU_ID).ToList();
            var OSData = _context.BKD_OrganizationalStructure.OrderBy(n => n.OS_ID).Take(3).ToList();
            var AnnouncementsData = _context.BKD_Announcements.OrderByDescending(n => n.ann_ID).Take(3).ToList();

            var model = new IndexViewModel
            {
                DataOI = OIData,
                ListOS = OSData,
                ListNews = NEWSData,
                ListAnnouncements = AnnouncementsData
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
