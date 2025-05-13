namespace Repository.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Common.Models.Product> GetProducts();
    }
}
