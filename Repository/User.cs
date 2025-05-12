using Microsoft.Data.SqlClient;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class User : IUser
    {
        public IEnumerable<Common.User> GetUsers()
        {
            var connectionString = "Server=localhost;Database=Financial;TrustServerCertificate=True;User Id=demo;Password=your_password;";
            var users = new List<Common.User>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_GetAllProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    // get index for each column, no need to call GetOrdinal multiple times
                    var idIdx = reader.GetOrdinal("UserId");
                    var nameIdx = reader.GetOrdinal("UserName");
                    var emailIdx = reader.GetOrdinal("Email");
                    var accountIdx = reader.GetOrdinal("AccountNumber");

                    while (reader.Read())
                    {
                        var user = new Common.User
                        {
                            UserId = reader.GetGuid(idIdx),
                            UserName = reader.GetString(nameIdx),
                            Email = reader.GetString(emailIdx),
                            AccountNumber = reader.GetString(accountIdx)
                        };
                        users.Add(user);
                    }
                };
            };

            return users;
        }
    }
}
