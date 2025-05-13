namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Common.Models.Product> GetProducts();
    }
}
