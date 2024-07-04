using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;   // Kulanıcı servisi 
        private readonly IReservationService _reservationService;  // Rezervasyon servisi

        public UserController(IAppUserService appUserService, IReservationService reservationService)
        {
            _appUserService = appUserService;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            var values = _appUserService.TGetList(); // Tüm kullanıcıları listeler
            return View(values); // Index view'ına bu kullanıcıları gönderir
        }
        public IActionResult DeleteUser(int id)
        {
            var values = _appUserService.TGetByID(id);  // Belirli bir kullanıcıyı ID'ye göre getirir
            _appUserService.TDelete(values);  // Kullanıcıyı siler
            return RedirectToAction("Index"); // Silme işlemi tamamlandıktan sonra Index action'ına yönlendirilir
        }
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var values = _appUserService.TGetByID(id);
            return View(values);  // Kullanıcı düzenleme için EditUser view'ına bu kullanıcıyı gönderir
        }

        [HttpPost]
        public IActionResult EditUser(AppUser appUser)
        {
            _appUserService.TUpdate(appUser);
            return RedirectToAction("Index");
        }

        public IActionResult CommentUser(int id)
        {
            _appUserService.TGetList(); // Kullanıcı yorumlarını listeler (implementasyon eksik olduğu için şu an geçici olarak sadece listeliyor)
            return View();
        }

        public IActionResult ReservationUser(int id)
        {
            var values = _reservationService.GetListWithReservationByAccepted(id); // Belirli bir kullanıcının kabul edilmiş rezervasyonlarını getirir
            return View(values);
        }
    }
}


