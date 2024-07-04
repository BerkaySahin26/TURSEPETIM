using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class LastDestinationsController : Controller
    {
        private readonly IDestinationService _destinationService; // IDestinationService türünden dependency injection

        public LastDestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }
        public IActionResult Index()
        {
            var values = _destinationService.TGetLast4Destinations(); // IDestinationService aracılığıyla son 4 destinasyonu alır
            return View(values); // Index view'ına son destinasyonları gönderir
        }
    }
}
