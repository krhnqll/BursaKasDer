﻿using Microsoft.AspNetCore.Mvc;
using bursaKasder.HelperClasses;
using Microsoft.Identity.Client;
using bursaKasder.Services;
using bursaKasder.Models;
using bursaKasder.Models.EntityModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Protocol;
using bursaKasder.Migrations;
using Microsoft.AspNetCore.Hosting;


namespace bursaKasder.Controllers
{

    public class AdminController : Controller
    {
        private readonly SessionClass _sessionClass;
        private readonly DbContextManager _context;
        private readonly Post_AdminService _postAdminService;
        private readonly Get_AdminService _getAdminService;

        public AdminController(SessionClass sessionClass, DbContextManager context, Post_AdminService postAdminService, Get_AdminService getAdminService)
        {
            _context = context;
            _sessionClass = sessionClass;
            _postAdminService = postAdminService;
            _getAdminService = getAdminService;
        }

        public IActionResult Index()
        {

            ViewData["UserName"] = HttpContext.Session.GetString("Admin_name");
            ViewData["UserSurname"] = HttpContext.Session.GetString("Admin_surname");

            var OIData = _context.BKD_OrganizationInformation.FirstOrDefault();

            if (OIData == null)
            {
                return NotFound("Hakkımızda bilgisi bulunamadı.");
            }


            return View(OIData);
        }

        //--------------------------------- User Operations -------------------------------------------
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            try
            {
                var result = await _getAdminService.GetAllUsers();

                return View(result);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;

                return View(new List<BKD_Admins>());
            }
        }

        [HttpGet]
        public IActionResult admin_add_NewUser()
        {
            return View(new BKD_Admins { });
        }

        [HttpGet]
        public async Task<IActionResult> admin_Edit_Profile(int id)
        {
            var adminUser = await _getAdminService.Get_UserById(id);

            if (adminUser == null)
            {
                return NotFound();
            }

            return View(adminUser);
        }

        [HttpGet]
        public IActionResult admin_Delete_User()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> admin_add_NewUser(BKD_Admins newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _postAdminService.Post_admin_add_NewUser(newUser);
                    if (result)
                    {
                        return RedirectToAction("UserList", "Admin");
                    }
                }
                catch (Exception)
                {
                    return View(newUser);
                }
            }

            return View(newUser);
        }


        [HttpPost]
        public async Task<IActionResult> admin_Edit_Profile(BKD_Admins updatedUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _postAdminService.Post_admin_Edit_Profile(updatedUser);
                    if (result)
                    {
                        return RedirectToAction("UserList", "Admin");
                    }
                }
                catch (Exception)
                {
                    return View(updatedUser);
                }
            }
            return View(updatedUser);
        }

        //-------------------------------- Edit Operations -----------------------------------------------

        // Edit işlemleri aynı sayfada olacak şekilde düzenlemek mantıklı olabilir. Tek bir model kullanılabilir.
        public IActionResult EditAbout()
        {
            return View();
        }

        public IActionResult EditOrganizationInfo()
        {
            return View();
        }

        public IActionResult EditOrganizational_Struct()
        {
            return View();
        }

        public IActionResult editEvents()
        {
            return View();
        }

        public IActionResult editContact()
        {
            return View();
        }

        public IActionResult editAnnouncements()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> admin_Edit_About(BKD_About updatedAbout)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _postAdminService.Post_admin_Edit_About(updatedAbout);
                    if (result)
                    {
                        TempData["Success"] = "Hakkımızda bilgileri güncellendi.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(updatedAbout);
        }

        //------------------------------------ Delete Functions ------------------------------------------

        [HttpPost]
        public IActionResult deleteUser(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }

            try
            {
                var data = _context.BKD_Admins.FirstOrDefault(d => d.adm_ID == id);

                if (data != null)
                {
                    data.adm_Status = 2;

                    _context.SaveChanges();

                    return RedirectToAction("UserList", "Admin");
                }
            }

            catch (Exception)
            {
                return RedirectToAction("UserList");
            }


            return RedirectToAction("UserList");
        }

        [HttpPost]
        public IActionResult deleteEvents(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }

            try
            {
                var data = _context.BKD_Events.FirstOrDefault(d => d.ev_ID == id);

                if (data != null)
                {
                    data.ev_Status = 2;

                    _context.SaveChanges();

                    return RedirectToAction("EventList", "Admin");
                }
            }

            catch (Exception)
            {
                return RedirectToAction("EventList");
            }


            return RedirectToAction("EventList");
        }



        //------------------------------------ Add Functions ------------------------------------------

        public IActionResult addEvents()
        {
            return View();
        }

        [HttpGet]
        public IActionResult addAbout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addAbout(addAboutImage about)
        {
            BKD_About w = new BKD_About();

            if (about.ab_MisVisPhoto != null && about.ab_HistoryPhoto != null)
            {
                var exMisVis = Path.GetExtension(about.ab_MisVisPhoto.FileName);
                var exHis = Path.GetExtension(about.ab_HistoryPhoto.FileName);
                var newImageMisVis = Guid.NewGuid() + exMisVis;
                var newImageHistory = Guid.NewGuid() + exHis;
                var location1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsAboutPhoto/", newImageMisVis);
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsAboutPhoto/", newImageHistory);

                var stream = new FileStream(location, FileMode.Create);
                var stream1 = new FileStream(location1, FileMode.Create);

                about.ab_HistoryPhoto.CopyTo(stream);
                about.ab_MisVisPhoto.CopyTo(stream1);

                w.ab_MisVisPhoto = newImageMisVis;
                w.ab_HistoryPhoto = newImageHistory;
            }

            w.ab_Mission = about.ab_Mission;
            w.ab_Vision = about.ab_Vision;
            w.ab_Status = 0;
            w.ab_History = about.ab_History;


            _context.BKD_About.Add(w);
            _context.SaveChanges();

            return RedirectToAction("AnnouncementsList", "Admin");

        }

        [HttpGet]
        public IActionResult addContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addContact(BKD_Contact contact)
        {
            contact.con_Status = 0;

            _context.BKD_Contact.Add(contact);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");

        }

        [HttpGet]
        public IActionResult addOI()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addOI(addOIimage OI)
        {
            BKD_OrganizationInformation w = new BKD_OrganizationInformation();

            if (OI.OI_Logo != null && OI.OI_StatuePhoto != null && OI.OI_Indexphoto != null)
            {
                var OILogo = Path.GetExtension(OI.OI_Logo.FileName);
                var OIStatue = Path.GetExtension(OI.OI_StatuePhoto.FileName);
                var OIIndex = Path.GetExtension(OI.OI_Indexphoto.FileName);

                var newImageLogo = Guid.NewGuid() + OILogo;
                var newImageStatue = Guid.NewGuid() + OIStatue;
                var newImageIndex = Guid.NewGuid() + OIIndex;

                var location1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsOIPhoto/", newImageLogo);
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsOIPhoto/", newImageStatue);
                var location2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsOIPhoto/", newImageIndex);

                var stream1 = new FileStream(location1, FileMode.Create);
                var stream = new FileStream(location, FileMode.Create);
                var stream2 = new FileStream(location2, FileMode.Create);

                OI.OI_StatuePhoto.CopyTo(stream);
                OI.OI_Logo.CopyTo(stream1);
                OI.OI_Indexphoto.CopyTo(stream2);

                w.OI_Logo = newImageLogo;
                w.OI_StatuePhoto = newImageStatue;
                w.OI_Indexphoto = newImageIndex;
            }

            w.OI_Name = OI.OI_Name;
            w.OI_Status = 0;



            _context.BKD_OrganizationInformation.Add(w);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }




        // --------- Bizden Haberler Fonksiyonları
        public IActionResult NewsList()
        {
            var newsList = _context.BKD_NewsFromUs.Select(n => new addNewsFromUs
            {
                newsU_ID = n.newsU_ID,
                newsU_Title = n.newsU_Title,
                newsU_Content = n.newsU_Content,
                newsU_PhotoPath = n.newsU_Photo, // Veritabanındaki fotoğraf yolunu atıyoruz
                newsU_Date = n.newsU_Date,
                newsU_Status = n.newsU_Status

            }).ToList();


            return View(newsList);
        }

        [HttpGet]
        public IActionResult addNewsFromUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addNewsFromUs(addNewsFromUs tt)
        {


            BKD_NewsFromUs m = new BKD_NewsFromUs();

            if (tt.newsU_Photo != null)
            {
                var extension = Path.GetExtension(tt.newsU_Photo.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadNewsUsPhoto/", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    tt.newsU_Photo.CopyTo(stream);
                }

                m.newsU_Photo = newFileName;
            }
            else
            {
                m.newsU_Photo = "default.jpg"; // Varsayılan resim eklenebilir
            }

            m.newsU_Title = tt.newsU_Title;
            m.newsU_Status = 0;
            m.newsU_Content = tt.newsU_Content;
            m.newsU_Date = tt.newsU_Date == default ? DateTime.Now : tt.newsU_Date; // Tarih boşsa bugünün tarihi atanır

            _context.BKD_NewsFromUs.Add(m);
            _context.SaveChanges();

            return RedirectToAction("NewsList", "Admin"); // Başarılı ekleme sonrası listeye yönlendir
        }

        [HttpGet]
        public IActionResult editNewsFromUs(int id)
        {
            var news = _context.BKD_NewsFromUs
                .Where(n => n.newsU_ID == id)
                .Select(n => new addNewsFromUs
                {
                    newsU_ID = n.newsU_ID,
                    newsU_Title = n.newsU_Title,
                    newsU_Content = n.newsU_Content,
                    newsU_PhotoPath = n.newsU_Photo,
                    newsU_Date = n.newsU_Date

                }).FirstOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            return PartialView("editNewsFromUs", news);
        }

        [HttpPost]
        public IActionResult editNewsFromUs(addNewsFromUs model)
        {
            var news = _context.BKD_NewsFromUs.Find(model.newsU_ID);

            if (news == null)
            {
                return NotFound();
            }

            news.newsU_Title = model.newsU_Title;
            news.newsU_Content = model.newsU_Content;
            news.newsU_Date = DateTime.Now;
            news.newsU_Status = 1;

            if (model.newsU_Photo != null)
            {
                string filePath = Path.Combine("wwwroot/UploadNewsUsPhoto", model.newsU_Photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.newsU_Photo.CopyTo(stream);
                }

                news.newsU_Photo = "/UploadNewsUsPhoto/" + model.newsU_Photo.FileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index","Admin");
        }


        [HttpPost]
        public IActionResult deleteNewsFromUs(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }

            try
            {
                var data = _context.BKD_NewsFromUs.FirstOrDefault(d => d.newsU_ID == id);

                if (data != null)
                {
                    data.newsU_Status = 2;

                    _context.SaveChanges();

                    return RedirectToAction("NewsUsList", "Admin");
                }
            }

            catch (Exception)
            {
                return RedirectToAction("NewsUsList");
            }


            return RedirectToAction("NewsUsList");
        }


        // --------- Duyuru Fonksiyonları
        public IActionResult AnnouncementsList()
        {
            var announcements = _context.BKD_Announcements.Select(n => new addAnnouncementsImage
            {
                ann_ID = n.ann_ID,
                ann_Title = n.ann_Title,
                ann_Content = n.ann_Content,
                ann_PhotoPath = n.ann_Photo,

            }).ToList();

            if (announcements == null)
            {
                announcements = new List<addAnnouncementsImage>(); // Boş bir liste gönder
            }

            return View(announcements);
        }

        [HttpGet]
        public IActionResult addAnnouncements()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addAnnouncements(addAnnouncementsImage p)
        {
            BKD_Announcements w = new BKD_Announcements();

            if (p.ann_Photo != null)
            {
                var extension = Path.GetExtension(p.ann_Photo.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsAnnouncementsPhoto/", newImageName);

                var stream = new FileStream(location, FileMode.Create);

                p.ann_Photo.CopyTo(stream);
                w.ann_Photo = newImageName;
            }

            w.ann_Title = p.ann_Title;
            w.ann_Status = 0;
            w.ann_Date = p.ann_Date;
            w.ann_Content = p.ann_Content;

            _context.BKD_Announcements.Add(w);
            _context.SaveChanges();

            return RedirectToAction("AnnouncementsList", "Admin");
        }

        [HttpGet]
        public IActionResult editAnnouncement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult editAnnouncement(addAnnouncementsImage model)
        {
            return RedirectToAction();
        }

        [HttpPost]
        public IActionResult deleteAnnouncements(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }

            try
            {
                var data = _context.BKD_Announcements.FirstOrDefault(d => d.ann_ID == id);

                if (data != null)
                {
                    data.ann_Status = 2;

                    _context.SaveChanges();

                    return RedirectToAction("AnnouncementsList", "Admin");
                }
            }

            catch (Exception)
            {
                return RedirectToAction("AnnouncementsList");
            }


            return RedirectToAction("AnnouncementsList");
        }

        // ----------- Teşkilat Yapısı Fonksiyonları
        public IActionResult OSStructureList()
        {
            var structures = _context.BKD_OrganizationalStructure.Select(n => new addOSImage {
                OS_ID = n.OS_ID,
                OS_Name = n.OS_Name,
                OS_Surname = n.OS_Surname,
                OS_PhotoPath = n.OS_Photo,
                OS_Comment = n.OS_Comment,
                OS_Status = n.OS_Status,
                OS_Degree = n.OS_Degree

            }).ToList();

            return View(structures);
        }

        [HttpGet]
        public IActionResult addOS()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addOS(addOSImage OS)
        {
            BKD_OrganizationalStructure m = new BKD_OrganizationalStructure();

            if (OS.OS_Photo != null)
            {
                var OILogo = Path.GetExtension(OS.OS_Photo.FileName);
                var newImageLogo = Guid.NewGuid() + OILogo;
                var location1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadOSPhoto/", newImageLogo);
                var stream1 = new FileStream(location1, FileMode.Create);
                OS.OS_Photo.CopyTo(stream1);
                m.OS_Photo = newImageLogo;

            }

            m.OS_Name = OS.OS_Name;
            m.OS_Status = 0;
            m.OS_Surname = OS.OS_Surname;
            m.OS_Degree = OS.OS_Degree;
            m.OS_Comment = OS.OS_Comment;

            _context.BKD_OrganizationalStructure.Add(m);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult editStructure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult editStructure(int id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult deleteOS(int id)
        {
            return View();
        }
    }
}
