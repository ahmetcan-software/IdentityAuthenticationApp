using AspNetCoreMvc_IdentityAuthenticationApp.Identity.Models;
using AspNetCoreMvc_IdentityAuthenticationApp.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspNetCoreMvc_IdentityAuthenticationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string? ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };
            TempData["message"] = null;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı!");
                return View(model);
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);  //lockoutonfailure özelliğini tru yaptığımızda kilitlenme durumunu kontrole başlar.

            //Aşağıdaki 3 seçenekten sadece biri gerçekleşir.
            if (signInResult.Succeeded)
            {
                TempData["message"] = "Login işlemi başarılı.";
                //return RedirectToAction("Login");
                return Redirect(model.ReturnUrl ?? "~/");
            }
            if (signInResult.IsLockedOut)
            {
                TempData["message"] = "Login işlemi bir süreliğine kilitlenmiştir.";
                //return RedirectToAction("Login");
            }
            //if (signInResult.IsNotAllowed)
            //{
            //    //Email veya telefon onayı istenirse
            //}
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            AppUser user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };
            var identityResult = await _userManager.CreateAsync(user, model.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                TempData["message"] = "Kullanıcı kayıt işlemi gerçekleştirildi.";
                return RedirectToAction("Register");
            }
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
