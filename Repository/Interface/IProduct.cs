namespace Repository.Interface
{
    public interface IProduct
    {
        IEnumerable<Common.Models.Product> GetProducts();
    }
}
