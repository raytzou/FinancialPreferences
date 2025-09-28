using Common.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class HouseRepository : IHouseRepository
    {
        private AppDbContext _dbContext;

        public HouseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<House> GetAll() => _dbContext.Houses.AsNoTracking();

        public House? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Create(House house)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }


        public void Update(House house)
        {
            throw new NotImplementedException();
        }
    }
}
