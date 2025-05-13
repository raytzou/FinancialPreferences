using BussinessLogic.Services.Interfaces;
using Common.Models;
using FinancialPreferences.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace FinancialPreferences.Controllers
{
    public class FinancialPreferences : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        private readonly IFinancialPreferencesService _financialPreferencesService;

        public FinancialPreferences(
            IProductRepository productRepository,
            IUserRepository userRepository,
            IUserPreferenceRepository userPreferenceRepository,
            IFinancialPreferencesService financialPreferencesService)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _userPreferenceRepository = userPreferenceRepository;
            _financialPreferencesService = financialPreferencesService;
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
            IEnumerable<Product> products;
            IEnumerable<User> users;
            List<PreferenceTableRowViewModel> table;
            Search(out products, out users, out table);

            return View("Index", new FinancialPreferenceViewModel
            {
                Products = products.ToList(),
                Users = users.ToList(),
                Preferences = table
            });
        }

        [HttpPost]
        public IActionResult Add(FinancialPreferenceViewModel model)
        {
            var userPreference = MappingViewModelToDBModel(model.EditingPreference);
            var validationErrors = _financialPreferencesService.Validate(userPreference);

            IEnumerable<Product> products;
            IEnumerable<User> users;
            List<PreferenceTableRowViewModel> table;
            Search(out products, out users, out table);

            if (validationErrors.Count > 0)
            {
                foreach (var error in validationErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View("Index", new FinancialPreferenceViewModel
                {
                    Products = products.ToList(),
                    Users = users.ToList(),
                    Preferences = table,
                    EditingPreference = model.EditingPreference
                });
            }

            _userPreferenceRepository.AddUserPreference(userPreference);
            return RedirectToAction("Index", new FinancialPreferenceViewModel
            {
                Products = products.ToList(),
                Users = users.ToList(),
                Preferences = table,
                EditingPreference = new PreferenceTableRowViewModel()
            });

            UserPreference MappingViewModelToDBModel(PreferenceTableRowViewModel model) => new UserPreference
            {
                PreferenceId = Guid.NewGuid(),
                UserId = model.UserId,
                ProductId = model.ProductId,
                OrderQuantity = model.OrderQuantity,
                AccountNumber = model.AccountNumber ?? _userRepository.GetUsers().FirstOrDefault(user => user.UserId == model.UserId)?.AccountNumber ?? throw new InvalidOperationException($"Cannot find the account number while insert data userid: {model.UserId}"),
                TotalAmount = model.EstimatedTotalAmount,
                TotalFee = model.TotalFee
            };
        }

        private void Search(out IEnumerable<Product> products, out IEnumerable<User> users, out List<PreferenceTableRowViewModel> table)
        {
            products = _productRepository.GetProducts();
            users = _userRepository.GetUsers();
            var preferences = _userPreferenceRepository.GetUserPreferences();

            table = (from preference in preferences
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
        }
    }
}
