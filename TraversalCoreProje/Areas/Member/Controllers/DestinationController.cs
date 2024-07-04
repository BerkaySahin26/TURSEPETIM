using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class DestinationController : Controller
    {
        DestinationManager destinationManager = new DestinationManager(new EfDestinationDal());
        public IActionResult Index()
        {
            var values = destinationManager.TGetList(); // Tüm destinasyonları listeler
            return View(values);
        }
        public IActionResult GetCitiesSearchByName(string searchString)
        {
            ViewData["CurrentFilter"] = searchString; // View'a geçici veri gönderir (arama terimini tutmak için)
            var values = from x in destinationManager.TGetList() select x; // Tüm destinasyonları sorgudan seçer
            if (!string.IsNullOrEmpty(searchString)) // Eğer arama terimi boş değilse
            {
                values = values.Where(y => y.City.Contains(searchString)); // Şehir adında arama terimini içeren destinasyonları filtreler
            }
            return View(values.ToList());  // Filtrelenmiş destinasyonları içeren view'ı gösterir
        }
    }
}
