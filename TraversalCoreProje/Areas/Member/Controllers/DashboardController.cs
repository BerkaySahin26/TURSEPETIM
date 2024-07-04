using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name); // Giriş yapmış kullanıcının bilgilerini kullanarak UserManager üzerinden bulur
            ViewBag.userName = values.Name + " " + values.Surname; // ViewBag üzerinde kullanıcının adını ve soyadını birleştirip saklar
            ViewBag.userImage = values.ImageUrl;// ViewBag üzerinde kullanıcının profil resim URL'sini saklar
            return View();
        }
        public async Task<IActionResult> MemberDashboard()
        {
            return View(); // Üye panelini göstermek için MemberDashboard view'ını gösterir
        }
    }
}
