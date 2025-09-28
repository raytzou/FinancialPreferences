using Common.Models;

namespace Repository.Interfaces
{
    public interface IHouseRepository
    {
        IQueryable<House> GetAll();
    }
}
