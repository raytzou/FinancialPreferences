using BusinessLogic.Services.Interfaces;
using Common.Models;
using Repository.Interfaces;

namespace BusinessLogic.Services
{
    public class FinancialPreferencesService : IFinancialPreferencesService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public FinancialPreferencesService(IUserRepository userRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public List<string> Validate(UserPreference userPreference)
        {
            var users = _userRepository.GetUsers();
            var products = _productRepository.GetProducts();
            var errors = new List<string>();

            if (!users.Any(user => user.UserId == userPreference.UserId))
                errors.Add($"User with ID {userPreference.UserId} does not exist.");
            if (!products.Any(product => product.ProductId == userPreference.ProductId))
                errors.Add($"Product with ID {userPreference.ProductId} does not exist.");
            if(userPreference.OrderQuantity <= 0)
                errors.Add("Order quantity must be greater than zero.");

            return errors;
        }
    }
}
