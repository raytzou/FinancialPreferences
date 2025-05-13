using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System.Data;

namespace Repository
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        private readonly string _connectionString;

        public UserPreferenceRepository(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("conneciton string 404 not found");
        }

        public void AddUserPreference(Common.Models.UserPreference userPreference)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_AddUserPreference", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PreferenceId", userPreference.PreferenceId);
                command.Parameters.AddWithValue("@UserId", userPreference.UserId);
                command.Parameters.AddWithValue("@ProductId", userPreference.ProductId);
                command.Parameters.AddWithValue("@OrderQuantity", userPreference.OrderQuantity);
                command.Parameters.AddWithValue("@AccountNumber", userPreference.AccountNumber);
                command.Parameters.AddWithValue("@TotalAmount", userPreference.TotalAmount);
                command.Parameters.AddWithValue("@TotalFee", userPreference.TotalFee);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Common.Models.UserPreference> GetUserPreferences()
        {
            var preferences = new List<Common.Models.UserPreference>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetAllUserPreferences", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    // get index for each column, no need to call GetOrdinal multiple times
                    var idIdx = reader.GetOrdinal("PreferenceId");
                    var userIdx = reader.GetOrdinal("UserId");
                    var productIdx = reader.GetOrdinal("ProductId");
                    var quantityIdx = reader.GetOrdinal("OrderQuantity");
                    var accountIdx = reader.GetOrdinal("AccountNumber");
                    var amountIdx = reader.GetOrdinal("TotalAmount");
                    var feeIdx = reader.GetOrdinal("TotalFee");

                    while (reader.Read())
                    {
                        var preference = new Common.Models.UserPreference
                        {
                            PreferenceId = reader.GetGuid(idIdx),
                            UserId = reader.GetGuid(userIdx),
                            ProductId = reader.GetGuid(productIdx),
                            OrderQuantity = reader.GetInt32(quantityIdx),
                            AccountNumber = reader.GetString(accountIdx),
                            TotalAmount = reader.GetDecimal(amountIdx),
                            TotalFee = reader.GetDecimal(feeIdx)
                        };
                        preferences.Add(preference);
                    }
                };
            };

            return preferences;
        }
    }
}
