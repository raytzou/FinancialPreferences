using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class Product : IProduct
    {
        private readonly string _connectionString;

        public Product(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Cannot find the conneciton string");
        }

        public IEnumerable<Common.Models.Product> GetProducts()
        {
            var products = new List<Common.Models.Product>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetAllProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    // get index for each column, no need to call GetOrdinal multiple times
                    var idIdx = reader.GetOrdinal("ProductId");
                    var nameIdx = reader.GetOrdinal("ProductName");
                    var priceIdx = reader.GetOrdinal("Price");
                    var feeIdx = reader.GetOrdinal("FeeRate");

                    while (reader.Read())
                    {
                        var product = new Common.Models.Product
                        {
                            ProductId = reader.GetGuid(idIdx),
                            ProductName = reader.GetString(nameIdx),
                            Price = reader.GetDecimal(priceIdx),
                            FeeRate = reader.GetDecimal(feeIdx)
                        };
                        products.Add(product);
                    }
                };
            };

            return products;
        }
    }
}
