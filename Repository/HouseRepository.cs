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
    }
}
