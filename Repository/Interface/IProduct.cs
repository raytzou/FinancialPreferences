namespace Repository.Interface
{
    public interface IProduct
    {
        IEnumerable<Common.Product> GetProducts();
    }
}
