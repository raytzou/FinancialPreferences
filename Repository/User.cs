using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System.Data;

namespace Repository
{
    public class User : IUserRepository
    {
        private readonly string _connectionString;

        public User(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");
        }

        public IEnumerable<Common.Models.User> GetUsers()
        {
            var users = new List<Common.Models.User>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetAllUsers", connection))
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
                        var user = new Common.Models.User
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
