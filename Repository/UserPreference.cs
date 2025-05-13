using Microsoft.Data.SqlClient;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class UserPreference : IUserPreference
    {
        public IEnumerable<Common.UserPreference> GetUserPreferences()
        {
            var connectionString = "Server=localhos;Database=Financial;TrustServerCertificate=True;User Id=demo;Password=your_password;";
            var preferences = new List<Common.UserPreference>();

            using (var connection = new SqlConnection(connectionString))
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
                        var preference = new Common.UserPreference
                        {
                            PreferenceId = reader.GetGuid(idIdx),
                            UserId = reader.GetGuid(userIdx),
                            ProductId = reader.GetGuid(productIdx),
                            OrderQuantity = reader.GetInt32(quantityIdx),
                            AccountNumber = reader.GetString(accountIdx),
                            TotalAmount = reader.GetInt32(amountIdx),
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
