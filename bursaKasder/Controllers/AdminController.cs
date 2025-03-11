using Microsoft.AspNetCore.Mvc;
using bursaKasder.HelperClasses;
using Microsoft.Identity.Client;
using bursaKasder.Services;
using bursaKasder.Models;
using bursaKasder.Models.EntityModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Protocol;
using bursaKasder.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


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

        private void CheckAdminSession()
        {
            if (string.IsNullOrEmpty(_sessionClass.Admin_name))
            {
                Response.Redirect("/Login/Index");
            }
        }

        public IActionResult Index()
        {
            var adminName = HttpContext.Session.GetString("Admin_name");

            // Kullanıcı oturum açmamışsa giriş sayfasına yönlendir
            if (string.IsNullOrEmpty(adminName))
            {
                return RedirectToAction("Index", "Login"); // "Account" controller'ındaki "Login" action'ına yönlendir
            }

            var counterValue = _context.BKD_OrganizationInformation.Select(s => s.Counter).FirstOrDefault();

            ViewBag.Name = adminName;
            ViewBag.Surname = HttpContext.Session.GetString("Admin_surname");
            ViewBag.Counter = counterValue;

            var OIData = _context.BKD_OrganizationInformation.FirstOrDefault();

            if (OIData == null)
            {
                return NotFound("Hakkımızda bilgisi bulunamadı.");
            }

            return View(OIData);
        }


        //-------------- Kullanıcı Fonksiyonları
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            CheckAdminSession();
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
            CheckAdminSession();
            return View(new BKD_Admins { });
        }

        [HttpGet]
        public async Task<IActionResult> admin_Edit_Profile(int id)
        {
            CheckAdminSession();
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

        [HttpPost]
        public IActionResult deleteUser(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Geçersiz haber ID!";
                return RedirectToAction("UserList", "Admin");
            }

            try
            {
                var data = _context.BKD_Admins.FirstOrDefault(d => d.adm_ID == id);

                if (data != null)
                {
                    data.adm_Status = 1; // Silme yerine pasif hale getirme
                    _context.BKD_Admins.Update(data); // Güncelleme işlemini belirt
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Haber başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Haber bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Haber silinirken bir hata oluştu!";
            }

            return RedirectToAction("UserList", "Admin");
        }


        //------------- Hakkında 

        [HttpGet]
        public IActionResult addAbout()
        {
            CheckAdminSession();
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

            return RedirectToAction("aboutInfo", "Admin");

        }

        [HttpGet]
        public IActionResult addContact()
        {
            CheckAdminSession();
            return View();
        }

        [HttpPost]
        public IActionResult addContact(BKD_Contact contact)
        {
            contact.con_Status = 0;

            _context.BKD_Contact.Add(contact);
            _context.SaveChanges();

            return RedirectToAction("contactInfo", "Admin");

        }

        [HttpGet]
        public IActionResult addOI()
        {
            CheckAdminSession();
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

            return RedirectToAction("bkdInfo", "Admin");
        }




        // --------- Bizden Haberler Fonksiyonları
        public IActionResult NewsList()
        {
            CheckAdminSession();
            var newsList = _context.BKD_NewsFromUs.Where(s => s.newsU_Status == 0).OrderByDescending(e => e.newsU_Date).Select(n => new addNewsFromUs
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
            CheckAdminSession();
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
            

            if (model.newsU_Photo != null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadNewsUsPhoto", model.newsU_Photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.newsU_Photo.CopyTo(stream);
                }

                news.newsU_Photo = model.newsU_Photo.FileName;
            }

            _context.SaveChanges();

            return RedirectToAction("NewsList", "Admin");
        }


        [HttpPost]
        public IActionResult deleteNewsFromUs(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Geçersiz haber ID!";
                return RedirectToAction("NewsList", "Admin");
            }

            try
            {
                var data = _context.BKD_NewsFromUs.FirstOrDefault(d => d.newsU_ID == id);

                if (data != null)
                {
                    data.newsU_Status = 1; // Silme yerine pasif hale getirme
                    _context.BKD_NewsFromUs.Update(data); // Güncelleme işlemini belirt
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Haber başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Haber bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Haber silinirken bir hata oluştu!";
            }

            return RedirectToAction("NewsList", "Admin");
        }





        // --------- Duyuru Fonksiyonları
        public IActionResult AnnouncementsList()
        {
            CheckAdminSession();
            var announcements = _context.BKD_Announcements.Where(s => s.ann_Status == 0).OrderByDescending(e => e.ann_Date).Select(n => new addAnnouncementsImage
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
            CheckAdminSession();
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
        public IActionResult editAnnouncement(int id)
        {
            CheckAdminSession();
            var news = _context.BKD_Announcements
               .Where(n => n.ann_ID == id)
               .Select(n => new addAnnouncementsImage
               {
                   ann_ID = n.ann_ID,
                   ann_Title = n.ann_Title,
                   ann_Content = n.ann_Content,
                   ann_PhotoPath = n.ann_Photo,
                   ann_Date = n.ann_Date

               }).FirstOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            return PartialView("editAnnouncement", news);
        }


        [HttpPost]
        public IActionResult editAnnouncement(addAnnouncementsImage model)
        {
            var news = _context.BKD_Announcements.Find(model.ann_ID);

            if (news == null)
            {
                return NotFound();
            }

            news.ann_Title = model.ann_Title;
            news.ann_Content = model.ann_Content;
            news.ann_Date = DateTime.Now;
            

            if (model.ann_Photo != null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsAnnouncementsPhoto", model.ann_Photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ann_Photo.CopyTo(stream);
                }

                news.ann_Photo = model.ann_Photo.FileName;
            }

            _context.SaveChanges();

            return RedirectToAction("AnnouncementsList", "Admin");
        }

        [HttpPost]
        public IActionResult deleteAnnouncements(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Geçersiz haber ID!";
                return RedirectToAction("AnnouncementList", "Admin");
            }

            try
            {
                var data = _context.BKD_Announcements.FirstOrDefault(d => d.ann_ID == id);

                if (data != null)
                {
                    data.ann_Status = 1; // Silme yerine pasif hale getirme
                    _context.BKD_Announcements.Update(data); // Güncelleme işlemini belirt
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Haber başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Haber bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Haber silinirken bir hata oluştu!";
            }

            return RedirectToAction("AnnouncementsList", "Admin");
        }

        // ----------- Teşkilat Yapısı Fonksiyonları
        public IActionResult OSStructureList()
        {
            CheckAdminSession();
            var structures = _context.BKD_OrganizationalStructure.Where(s => s.OS_Status == 0).Select(n => new addOSImage
            {
                OS_ID = n.OS_ID,
                OS_Name = n.OS_Name,
                OS_Surname = n.OS_Surname,
                OS_PhotoPath = n.OS_Photo,
                OS_Comment = n.OS_Comment,
                OS_Status = n.OS_Status,
                OS_Degree = n.OS_Degree,

            }).ToList();

            return View(structures);
        }

        [HttpGet]
        public IActionResult addOS()
        {
            CheckAdminSession();
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

            return RedirectToAction("OSStructureList", "Admin");
        }

        [HttpGet]
        public IActionResult editStructure(int id)
        {
            var news = _context.BKD_OrganizationalStructure
               .Where(n => n.OS_ID == id)
               .Select(n => new addOSImage
               {
                   OS_ID = n.OS_ID,
                   OS_Name = n.OS_Name,
                   OS_Surname = n.OS_Surname,
                   OS_Degree = n.OS_Degree,
                   OS_PhotoPath = n.OS_Photo,
                   OS_Comment = n.OS_Comment,

               }).FirstOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            return PartialView("editStructure", news);
        }

        [HttpPost]
        public IActionResult editStructure(addOSImage model)
        {
            var news = _context.BKD_OrganizationalStructure.Find(model.OS_ID);

            if (news == null)
            {
                return NotFound();
            }

            news.OS_Name = model.OS_Name;
            news.OS_Surname = model.OS_Surname;
            news.OS_Degree = model.OS_Degree;
            news.OS_Comment = model.OS_Comment;
            

            if (model.OS_Photo != null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadOSPhoto", model.OS_Photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.OS_Photo.CopyTo(stream);
                }

                news.OS_Photo = model.OS_Photo.FileName;
            }

            _context.SaveChanges();

            return RedirectToAction("OSStructureList", "Admin");
        }

        [HttpPost]
        public IActionResult deleteOS(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Geçersiz haber ID!";
                return RedirectToAction("OSStructureList", "Admin");
            }

            try
            {
                var data = _context.BKD_OrganizationalStructure.FirstOrDefault(d => d.OS_ID == id);

                if (data != null)
                {
                    data.OS_Status = 1; // Silme yerine pasif hale getirme
                    _context.BKD_OrganizationalStructure.Update(data); // Güncelleme işlemini belirt
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Haber başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Haber bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Haber silinirken bir hata oluştu!";
            }

            return RedirectToAction("OSStructureList", "Admin");
        }


        // -------------------- İletişim bilgileri 

        public IActionResult contactInfo()
        {
            CheckAdminSession();
            var contactData = _context.BKD_Contact.FirstOrDefault();

            if (contactData == null)
            {
                return NotFound();
            }

            return View(contactData);
        }

        [HttpGet]
        public IActionResult editContact()
        {
            CheckAdminSession();
            var findData = _context.BKD_Contact.FirstOrDefault();

            if (findData != null)
            {
                return PartialView("editContact", findData);
            }


            return View();
        }

        [HttpPost]
        public IActionResult editContact(BKD_Contact contact)
        {
            var existingData = _context.BKD_Contact.FirstOrDefault();

            if (existingData != null)
            {
                existingData.con_Adress = contact.con_Adress;
                existingData.con_Phone = contact.con_Phone;
                existingData.con_PhoneSecond = contact.con_PhoneSecond;
                existingData.con_Email = contact.con_Email;
                existingData.con_EmailSecond = contact.con_EmailSecond;
                existingData.con_URLX = contact.con_URLX;
                existingData.con_URLFacebook = contact.con_URLFacebook;
                existingData.con_URLInstagram = contact.con_URLInstagram;
                existingData.con_URLYoutube = contact.con_URLYoutube;
                existingData.con_Status = 1;

                _context.Entry(existingData).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("contactInfo", "Admin");

            }
            return View(contact);
        }

        // ------------------ Dernek Bilgileri 

        [HttpGet]
        public IActionResult bkdInfo()
        {
            CheckAdminSession();
            var bkdDATA = _context.BKD_OrganizationInformation.Select(n => new addOIimage
            {
                OI_ID = n.OI_ID,
                OI_Name = n.OI_Name,
                OI_LogoPath = n.OI_Logo,
                OI_StatuePhotoPath = n.OI_StatuePhoto,
                OI_IndexphotoPath = n.OI_Indexphoto,
                OI_Status = n.OI_Status,
            }).FirstOrDefault();

            return View(bkdDATA);
        }

        [HttpGet]
        public IActionResult editBkd()
        {
            CheckAdminSession();
            var news = _context.BKD_OrganizationInformation.Select(n => new addOIimage
            {
                OI_ID = n.OI_ID,
                OI_Name = n.OI_Name,
                OI_LogoPath = n.OI_Logo,
                OI_StatuePhotoPath = n.OI_StatuePhoto,
                OI_IndexphotoPath = n.OI_Indexphoto,

            }).FirstOrDefault();

            if (news != null)
            {
                return PartialView("editBkd", news);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult editBkd(addOIimage model)
        {
            var news = _context.BKD_OrganizationInformation.Find(model.OI_ID);

            if (news == null)
            {
                return NotFound();
            }

            news.OI_Name = model.OI_Name;
            news.OI_Status = 1;

            var files = new Dictionary<string, IFormFile>
            {
                { "OI_Logo", model.OI_Logo },
                { "OI_StatuePhoto", model.OI_StatuePhoto },
                { "OI_Indexphoto", model.OI_Indexphoto }
            };

            foreach (var file in files)
            {
                if (file.Value != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Value.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsOIPhoto", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.Value.CopyTo(stream);
                    }

                    // Veritabanındaki ilgili alanı güncelliyoruz
                    switch (file.Key)
                    {
                        case "OI_Logo":
                            news.OI_Logo = uniqueFileName;
                            break;
                        case "OI_StatuePhoto":
                            news.OI_StatuePhoto = uniqueFileName;
                            break;
                        case "OI_Indexphoto":
                            news.OI_Indexphoto = uniqueFileName;
                            break;
                    }
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Veritabanına kaydedilirken hata oluştu: " + ex.Message);
            }

            return RedirectToAction("bkdInfo", "Admin");
        }

        // -------- Hakkında Bilgileri

        [HttpGet]
        public IActionResult aboutInfo()
        {
            CheckAdminSession();
            var aboutData = _context.BKD_About.Select(n => new addAboutImage
            {
                ab_ID = n.ab_ID,
                ab_History = n.ab_History,
                ab_HistoryPhotoPath = n.ab_HistoryPhoto,
                ab_Vision = n.ab_Vision,
                ab_MisVisPhotoPath = n.ab_MisVisPhoto,
                ab_Mission = n.ab_Mission,
                ab_Status = n.ab_Status,

            }).FirstOrDefault();

            return View(aboutData);
        }
        [HttpGet]
        public IActionResult editAbout()
        {
            CheckAdminSession();
            var news = _context.BKD_About
              .Select(n => new addAboutImage
              {
                  ab_ID = n.ab_ID,
                  ab_History = n.ab_History,
                  ab_Vision = n.ab_Vision,
                  ab_Mission = n.ab_Mission,
                  ab_HistoryPhotoPath = n.ab_HistoryPhoto,
                  ab_MisVisPhotoPath = n.ab_MisVisPhoto,

              }).FirstOrDefault();

            if (news != null)
            {
                return PartialView("editAbout", news);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult editAbout(addAboutImage model)
        {
            var news = _context.BKD_About.Find(model.ab_ID);

            if (news == null)
            {
                return NotFound();
            }

            news.ab_History = model.ab_History;
            news.ab_Vision = model.ab_Vision;
            news.ab_Mission = model.ab_Mission;
            news.ab_Status = 1;

            var files = new Dictionary<string, IFormFile>
            {
                { "ab_HistoryPhoto", model.ab_HistoryPhoto },
                { "ab_MisVisPhoto", model.ab_MisVisPhoto },

            };

            foreach (var file in files)
            {
                if (file.Value != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Value.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsAboutPhoto", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.Value.CopyTo(stream);
                    }

                    // Veritabanındaki ilgili alanı güncelliyoruz
                    switch (file.Key)
                    {
                        case "OI_Logo":
                            news.ab_HistoryPhoto = uniqueFileName;
                            break;
                        case "OI_StatuePhoto":
                            news.ab_MisVisPhoto = uniqueFileName;
                            break;

                    }
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Veritabanına kaydedilirken hata oluştu: " + ex.Message);
            }

            return RedirectToAction("aboutInfo", "Admin");
        }

        // -------- Etkinlik Fonksiyonları

        [HttpGet]
        public IActionResult eventList()
        {
            CheckAdminSession();
            var eventData = _context.BKD_Events.Where(s => s.ev_Status == 0).OrderByDescending(e => e.ev_Date).Select(e => new EventViewModel
            {
                ev_ID = e.ev_ID,
                ev_Title = e.ev_Title,
                ev_Content = e.ev_Content,
                ev_Date = e.ev_Date,
                ev_MainPhoto = e.ev_MainPhoto

            }).ToList();

            return View(eventData);
        }

        [HttpGet]
        public IActionResult addEvent()
        {
            CheckAdminSession();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addEvent(EventViewModel model)
        {
            BKD_Events newEvent = new BKD_Events
            {
                ev_Title = model.ev_Title,
                ev_Content = model.ev_Content,
                ev_Date = model.ev_Date == default ? DateTime.Now : model.ev_Date,
                ev_Status = 0
            };

            // **Ana Fotoğraf Yükleme**
            if (model.MainPhotoFile != null)
            {
                var extension = Path.GetExtension(model.MainPhotoFile.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadEventPhoto/", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.MainPhotoFile.CopyToAsync(stream);
                }

                newEvent.ev_MainPhoto = newFileName;
            }
            else
            {
                newEvent.ev_MainPhoto = "default.jpg";
            }

            _context.BKD_Events.Add(newEvent);
            await _context.SaveChangesAsync();

            // **Ekstra Fotoğrafları Kaydetme (Yöntem 1: Model Binding ile)**
            if (model.NewPhotos != null && model.NewPhotos.Count > 0)
            {
                foreach (var file in model.NewPhotos)
                {
                    if (file != null && file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var newFileName = Guid.NewGuid() + extension;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadEventPhoto/", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        BKD_EventPhotos eventPhoto = new BKD_EventPhotos
                        {
                            evP_Photo = newFileName,
                            evP_EventId = newEvent.ev_ID,
                            evP_Status = 0
                        };

                        _context.BKD_EventPhotos.Add(eventPhoto);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("eventList", "Admin");
        }


        [HttpPost]
        public IActionResult delEvent(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Geçersiz haber ID!";
                return RedirectToAction("eventList", "Admin");
            }

            try
            {
                var data = _context.BKD_Events.FirstOrDefault(d => d.ev_ID == id);

                if (data != null)
                {
                    data.ev_Status = 1; // Silme yerine pasif hale getirme
                    _context.BKD_Events.Update(data); // Güncelleme işlemini belirt
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Haber başarıyla silindi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Haber bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Haber silinirken bir hata oluştu!";
            }

            return RedirectToAction("eventList", "Admin");
        }

        [HttpGet]
        public IActionResult editEvent(int id)
        {
            var eventToEdit = _context.BKD_Events
                .Include(e => e.EventPhotos) // İlişkili fotoğrafları da çekiyoruz
                .FirstOrDefault(e => e.ev_ID == id);

            if (eventToEdit == null)
            {
                return NotFound();
            }

            EventViewModel model = new EventViewModel
            {
                ev_ID = eventToEdit.ev_ID,
                ev_Title = eventToEdit.ev_Title,
                ev_Content = eventToEdit.ev_Content,
                ev_Date = eventToEdit.ev_Date,
                ev_MainPhoto = eventToEdit.ev_MainPhoto,
                EventPhotos = eventToEdit.EventPhotos.Select(p => p.evP_Photo).ToList()
            };

            return PartialView("editEvent", model);
        }

        [HttpPost]
        public async Task<IActionResult> editEvent(EventViewModel model)
        {
            var eventToUpdate = _context.BKD_Events
                .Include(e => e.EventPhotos)
                .FirstOrDefault(e => e.ev_ID == model.ev_ID);

            if (eventToUpdate == null)
            {
                return NotFound();
            }

            // Güncellenen Bilgiler
            eventToUpdate.ev_Title = model.ev_Title;
            eventToUpdate.ev_Content = model.ev_Content;
            eventToUpdate.ev_Date = model.ev_Date;

            // **Ana Fotoğraf Güncelleme**
            if (model.MainPhotoFile != null)
            {
                // Eski fotoğrafı sil
                if (!string.IsNullOrEmpty(eventToUpdate.ev_MainPhoto) && eventToUpdate.ev_MainPhoto != "default.jpg")
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadEventPhoto/", eventToUpdate.ev_MainPhoto);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Yeni fotoğrafı kaydet
                var extension = Path.GetExtension(model.MainPhotoFile.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadEventPhoto/", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.MainPhotoFile.CopyToAsync(stream);
                }

                eventToUpdate.ev_MainPhoto = newFileName;
            }

            // **Ekstra Fotoğrafları Güncelleme**
            if (model.NewPhotos != null && model.NewPhotos.Count > 0)
            {
                foreach (var file in model.NewPhotos)
                {
                    if (file != null && file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var newFileName = Guid.NewGuid() + extension;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadEventPhoto/", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        BKD_EventPhotos eventPhoto = new BKD_EventPhotos
                        {
                            evP_Photo = newFileName,
                            evP_EventId = eventToUpdate.ev_ID,
                            
                        };

                        _context.BKD_EventPhotos.Add(eventPhoto);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("eventList", "Admin");
        }

        [HttpPost]
        public IActionResult DeletePhoto(string photoName, int eventId)
        {
            var photo = _context.BKD_EventPhotos.FirstOrDefault(p => p.evP_Photo == photoName && p.evP_EventId == eventId);
            if (photo != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadEventPhoto/", photo.evP_Photo);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.BKD_EventPhotos.Remove(photo);
                _context.SaveChanges();
            }

            return RedirectToAction("editEvent", new { id = eventId });
        }

        //------ İletişime Geçenler

        [HttpGet]
        public IActionResult conUList()
        {
            CheckAdminSession();
            var messages = _context.BKD_ContactUs
                            .OrderByDescending(m => m.conU_DateMessage) // En yeni mesaj en üstte
                            .ToList();
            return View(messages);
        }

    }
}
