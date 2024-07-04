using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel p)
        {
            // UserRegisterViewModel üzerinden gelen bilgilerle yeni bir AppUser oluşturulur
            AppUser appUser = new AppUser()
            {
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Mail,
                UserName = p.Username
            };
            // Girilen şifre ile şifre tekrarını kontrol eder
            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);

                if (result.Succeeded)
                {
                    // Başarılı ise kullanıcıyı giriş sayfasına yönlendirir
                    return RedirectToAction("SignIn");
                }
                else
                {
                    // Başarısız ise hata mesajlarını model durumuna ekler ve tekrar kayıt sayfasını gösterir
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı adı ve şifre ile giriş yapma işlemi
                var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
                if (result.Succeeded)
                {
                    // Giriş başarılı ise üye alanına yönlendirir
                    return RedirectToAction("Index", "Profile", new { area = "Member" });
                }
                else
                {
                    // Giriş başarısız ise tekrar giriş sayfasını gösterir
                    return RedirectToAction("SignIn", "Login");
                }
            }
            return View();  // Model doğrulama hatası durumunda giriş sayfasını tekrar gösterir
        }
    }
}
