using bursaKasder.HelperClasses;
using bursaKasder.Models.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace bursaKasder.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbContextManager _context;

        public LoginController(DbContextManager context)
        {
            _context = context;
        }

        private static Dictionary<string, int> loginAttempts = new Dictionary<string, int>();
        private static Dictionary<string, DateTime> lockedUsers = new Dictionary<string, DateTime>();
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (lockedUsers.ContainsKey(model.adm_Username) && lockedUsers[model.adm_Username] > DateTime.Now)
            {
                TimeSpan remainingTime = lockedUsers[model.adm_Username] - DateTime.Now;
                ModelState.AddModelError("", $"Çok fazla hatalı giriş yaptınız. Lütfen {remainingTime.Minutes} dakika {remainingTime.Seconds} saniye sonra tekrar deneyin.");
                return View(model);
            }

            var Users = _context.BKD_Admins.FirstOrDefault(a => a.adm_Username == model.adm_Username);

            if (Users == null || Users.adm_Password != model.adm_Password)
            {
                IncrementLoginAttempts(model.adm_Username);
                ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre. Lütfen tekrar deneyiniz.");
                return View(model);
            }

            // Kullanıcı doğrulandı, giriş başarılı
            ResetLoginAttempts(model.adm_Username);

            //HttpContext.Session.SetString("Admin_Id", Users.adm_ID);
            HttpContext.Session.SetString("Admin_name", Users.adm_Name);
            HttpContext.Session.SetString("Admin_surname", Users.adm_Surname);

            return RedirectToAction("Index", "Admin");

        }

        private void IncrementLoginAttempts(string userId)
        {
            if (!loginAttempts.ContainsKey(userId))
                loginAttempts[userId] = 0;

            loginAttempts[userId]++;

            if (loginAttempts[userId] >= 3)
            {
                lockedUsers[userId] = DateTime.Now.AddMinutes(1); // 1 dakika boyunca giriş yapamaz
                Task.Delay(TimeSpan.FromMinutes(1)).ContinueWith(t => ResetLoginAttempts(userId));
            }
        }

        private void ResetLoginAttempts(string userId)
        {
            if (loginAttempts.ContainsKey(userId))
                loginAttempts.Remove(userId);

            if (lockedUsers.ContainsKey(userId))
                lockedUsers.Remove(userId);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
