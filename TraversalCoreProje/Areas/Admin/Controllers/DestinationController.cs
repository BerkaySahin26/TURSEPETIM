using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Controller'ın Admin alanı içinde çalışacağını belirttik
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService; // Constructor aracılığıyla IDestinationService bağımlılığı enjekte edilir
        }

        public IActionResult Index()
        {
            var values = _destinationService.TGetList(); // Tüm destinasyonları getiren bir metot çağrıldı
            return View(values);
        }

        [HttpGet]
        public IActionResult AddDestination()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDestination(Destination destination)
        {
            _destinationService.TAdd(destination);  // Yeni bir destinasyon eklemek için
            return RedirectToAction("Index");
        }
        public IActionResult DeleteDestination(int id)
        {
            var values = _destinationService.TGetByID(id); //Belirli bir ID'ye sahip destinasyonu getiren
            _destinationService.TDelete(values);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateDestination(int id)
        {
            var values = _destinationService.TGetByID(id);
            return View(values); // Güncelleme için UpdateDestination view'ına bu destinasyon bilgileri gönderilir
        }
        [HttpPost]
        public IActionResult UpdateDestination(Destination destination)
        {
            _destinationService.TUpdate(destination);
            return RedirectToAction("Index");  // Güncelleme işlemi tamamlandıktan sonra Index actiona yönlendirilir
        }
    }
}
