using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TraversalCoreProje.Areas.Member.Models;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name); // Giriş yapmış kullanıcının bilgilerini kullanarak UserManager üzerinden bulur
            UserEditViewModel userEditViewModel = new UserEditViewModel(); // UserEditViewModel sınıfından yeni bir örnek oluşturur
            userEditViewModel.name = values.Name;  // Kullanıcının adını UserEditViewModel içindeki alanlara atar
            userEditViewModel.surname = values.Surname; // Kullanıcının soyadını UserEditViewModel içindeki alanlara atar
            userEditViewModel.phonenumber = values.PhoneNumber; // Kullanıcının telefon numarasını UserEditViewModel içindeki alanlara atar
            userEditViewModel.mail = values.Email; // Kullanıcının e-posta adresini UserEditViewModel içindeki alanlara atar
            return View(userEditViewModel); // Index view'ına UserEditViewModel örneğini gönderir
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Image.FileName);
                var imagename = Guid.NewGuid() + extension; // Yeni bir dosya adı oluşturur
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create); // Yeni bir dosya akışı oluşturur
                await p.Image.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
            user.Name = p.name;
            user.Surname = p.surname;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.password); // Kullanıcının şifresini günceller
            var result = await _userManager.UpdateAsync(user); // Kullanıcı bilgilerini UserManager üzerinden günceller

            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login"); // Kullanıcıyı giriş yapma sayfasına yönlendirir
            }
            return View();
        }
    }
}
