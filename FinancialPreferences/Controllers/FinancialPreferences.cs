using FinancialPreferences.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace FinancialPreferences.Controllers
{
    public class FinancialPreferences : Controller
    {
        private readonly IProduct _productRepository;
        private readonly IUser _userRepository;

        public FinancialPreferences(
            IProduct productRepository,
            IUser userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View(new FinancialPreferenceViewModel
            {
                Products = _productRepository.GetProducts().ToList(),
                Users = _userRepository.GetUsers().ToList(),
            });
        }
    }
}
