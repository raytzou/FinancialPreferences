using Microsoft.AspNetCore.Mvc;

namespace FinancialPreferences.Controllers
{
    public class HousePublisherController : Controller
    {
        private readonly ILogger<HousePublisherController> _logger;

        public HousePublisherController(ILogger<HousePublisherController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}