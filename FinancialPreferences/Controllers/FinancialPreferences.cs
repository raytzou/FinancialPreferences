using BussinessLogic.Services.Interfaces;
using Common.Models;
using FinancialPreferences.Models;
using FinancialPreferences.Models.Requests;
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
        public IActionResult GetById([FromBody] GetByIdRequest model)
        {
            var preferences = _userPreferenceRepository.GetUserPreferences();
            var users = _userRepository.GetUsers();
            var products = _productRepository.GetProducts();

            var pref = preferences.FirstOrDefault(p => p.PreferenceId == model.PreferenceId);
            if (pref == null) return NotFound();

            var user = users.FirstOrDefault(u => u.UserId == pref.UserId);
            var product = products.FirstOrDefault(p => p.ProductId == pref.ProductId);

            return Json(new
            {
                preferenceId = pref.PreferenceId,
                productId = pref.ProductId,
                productName = product?.ProductName,
                productPrice = product?.Price,
                feeRate = product?.FeeRate,
                userId = user?.UserId,
                userName = user?.UserName,
                accountNumber = pref.AccountNumber,
                orderQuantity = pref.OrderQuantity,
                email = user?.Email
            });
        }

        [HttpPost]
        public IActionResult Edit(FinancialPreferenceViewModel model)
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

            if (model.EditingPreference.PreferenceId == Guid.Empty)
            {
                _userPreferenceRepository.AddUserPreference(userPreference);
            }
            else
            {
                _userPreferenceRepository.UpdateUserPreference(userPreference);
            }

            return RedirectToIndexWithModel(products, users, table);

            UserPreference MappingViewModelToDBModel(PreferenceTableRowViewModel view) => new UserPreference
            {
                PreferenceId = model.EditingPreference.PreferenceId == Guid.Empty ? Guid.NewGuid() : model.EditingPreference.PreferenceId,
                UserId = view.UserId,
                ProductId = view.ProductId,
                OrderQuantity = view.OrderQuantity,
                AccountNumber = view.AccountNumber ?? _userRepository.GetUsers().FirstOrDefault(user => user.UserId == view.UserId)?.AccountNumber ?? throw new InvalidOperationException($"Cannot find the account number while insert data userid: {view.UserId}"),
                TotalAmount = view.EstimatedTotalAmount,
                TotalFee = view.TotalFee
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromBody] DeleteRequest model)
        {
            if (model.PreferenceId == Guid.Empty)
                return BadRequest("PreferenceId empty");

            try
            {
                _userPreferenceRepository.DeleteUserPreference(model.PreferenceId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            IEnumerable<Product> products;
            IEnumerable<User> users;
            List<PreferenceTableRowViewModel> table;
            Search(out products, out users, out table);

            return RedirectToIndexWithModel(products, users, table);
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
                         PreferenceId = preference.PreferenceId,
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

        private IActionResult RedirectToIndexWithModel(IEnumerable<Product> products, IEnumerable<User> users, List<PreferenceTableRowViewModel> table)
        {
            return RedirectToAction("Index", new FinancialPreferenceViewModel
            {
                Products = products.ToList(),
                Users = users.ToList(),
                Preferences = table,
                EditingPreference = new PreferenceTableRowViewModel()
            });
        }
    }
}
