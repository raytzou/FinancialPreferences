using Common.Models;

namespace Repository.Interfaces
{
    public interface IHouseRepository
    {
        IQueryable<House> GetAll();
        House? GetById(Guid id);
        void Create(House house);
        void Update(House house);
        void Delete(Guid id);
    }
}
