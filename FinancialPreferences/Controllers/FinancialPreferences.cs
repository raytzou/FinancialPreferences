using Microsoft.AspNetCore.Mvc;

namespace FinancialPreferences.Controllers
{
    public class FinancialPreferences : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
