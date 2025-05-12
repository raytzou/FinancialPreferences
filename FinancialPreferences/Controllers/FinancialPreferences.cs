using FinancialPreferences.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace FinancialPreferences.Controllers
{
    public class FinancialPreferences : Controller
    {
        private readonly IProduct _productRepository;

        public FinancialPreferences(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(new FinancialPreferenceViewModel
            {
                Products = _productRepository.GetProducts().ToList(),
            });
        }
    }
}
