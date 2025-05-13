using FinancialPreferences.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace FinancialPreferences.Controllers
{
    public class FinancialPreferences : Controller
    {
        private readonly IProduct _productRepository;
        private readonly IUser _userRepository;
        private readonly IUserPreference _userPreferenceRepository;

        public FinancialPreferences(
            IProduct productRepository,
            IUser userRepository,
            IUserPreference userPreferenceRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _userPreferenceRepository = userPreferenceRepository;
        }

        public IActionResult Index()
        {
            return View(new FinancialPreferenceViewModel
            {
                Products = _productRepository.GetProducts().ToList(),
                Users = _userRepository.GetUsers().ToList(),
            });
        }

        [HttpPost]
        public IActionResult Search()
        {
            var products = _productRepository.GetProducts();
            var users = _userRepository.GetUsers();
            var preferences = _userPreferenceRepository.GetUserPreferences();

            var table = (from preference in preferences
                         join product in products on preference.ProductId equals product.ProductId
                         join user in users on preference.UserId equals user.UserId
                         select new PreferenceTableRowViewModel
                         {
                             ProductName = product.ProductName,
                             ProductPrice = product.Price,
                             FeeRate = product.FeeRate,
                             OrderQuantity = preference.OrderQuantity,
                             EstimatedTotalAmount = preference.OrderQuantity * product.Price,
                             TotalFee = preference.OrderQuantity * product.Price * product.FeeRate / 100,
                             AccountNumber = preference.AccountNumber,
                             Email = user.Email
                         }).ToList();

            return View("Index", new FinancialPreferenceViewModel
            {
                Products = products.ToList(),
                Users = users.ToList(),
                Preferences = table
            });
        }
    }
}
