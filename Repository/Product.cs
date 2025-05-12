using Microsoft.Data.SqlClient;
using Repository.Interface;
using System.Data;

namespace Repository
{
    public class Product : IProduct
    {
        public IEnumerable<Common.Product> GetProducts()
        {
            var connectionString = "Server=localhost;Database=Financial;TrustServerCertificate=True;User Id=demo;Password=your_password;";
            var products = new List<Common.Product>();

            using (var connection = new SqlConnection(connectionString))
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
                        var product = new Common.Product
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
