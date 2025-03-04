using Microsoft.AspNetCore.Mvc;
using bursaKasder.HelperClasses;
using Microsoft.Identity.Client;
using bursaKasder.Services;
using bursaKasder.Models;
using bursaKasder.Models.EntityModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;


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

            return View();
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
                        return RedirectToAction("UserList","Admin");
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
                catch (Exception )
                {
                    return View(updatedUser);
                }
            }
            return View(updatedUser);
        }

        [HttpPost]
        public async Task<IActionResult> admin_Delete_User(int adminId)
        {
            var user = new BKD_Admins { adm_ID = adminId };

            try
            {
                bool result = await _postAdminService.Post_admin_Delete_User(user);
                if (result)
                {
                    TempData["Success"] = "Kullanıcı başarıyla silindi.";
                    return RedirectToAction("Index","Admin");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View();
        }


        //-------------------------------- Edit Operations -----------------------------------------------

        // Edit işlemleri aynı sayfada olacak şekilde düzenlemek mantıklı olabilir. Tek bir model kullanılabilir.
        public IActionResult admin_Edit_About()
        {
            return View();
        }

        public IActionResult admin_Edit_OrganizationInfo()
        {
            return View();
        }

        public IActionResult admin_Edit_Organizational_Struct()
        {
            return View();
        }
      
        public IActionResult admin_edit_NewsFromUs()
        {
            return View();
        }

        public IActionResult admin_edit_Events()
        {
            return View();
        }

        public IActionResult admin_edit_Contact()
        {
            return View();
        }

        public IActionResult admin_edit_Announcements()
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

        //------------------------------------ Get And Post Operations ------------------------------------------




        [HttpGet]
        public IActionResult addAnnouncements()
        {
            var announcements = _context.BKD_Announcements.ToList();
            return View(announcements);
        }

        [HttpPost] 
        public IActionResult addAnnouncements(addAnnouncementsImage p)
        {
            BKD_Announcements w = new BKD_Announcements();

            if(p.ann_Photo !=null)
            {
                var extension= Path.GetExtension(p.ann_Photo.FileName);
                var newImageName = Guid.NewGuid()+extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadsAnnouncementsPhoto/",newImageName);

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

            return RedirectToAction("addAnnouncements","Admin");
        }

        //EventsGet&Post
        public IActionResult admin_add_Events()
        {
            return View();
        }

        //NewsFromUsGet&Post
        public IActionResult admin_add_NewsFromUs()
        {
            return View();
        }

        //OrganizationalStructureGet&Post
        public IActionResult admin_add_OrgStructure()
        {
            return View();
        }
    }
}
