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

        public House? GetById(Guid id) => GetAll().FirstOrDefault(x => x.Id == id);

        public void Create(House house) => _dbContext.Houses.Add(house);

        public void Delete(Guid id)
        {
            var target = _dbContext.Houses.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException($"Cannot find the House entity. ID: {id}");

            _dbContext.Houses.Remove(target);
        }

        public void Update(House house)
        {
            var target = _dbContext.Houses.FirstOrDefault(x => x.Id == house.Id) ?? throw new InvalidOperationException($"Cannot find the House entity. ID: {house.Id}");

            target.HouseName = house.HouseName;
            target.Address = house.Address;
            target.TotalPrice = house.TotalPrice;
            target.FloorArea = house.FloorArea;
            target.Description = house.Description;
            target.UpdatedDate = DateTime.Now;
        }

        public void CommitChanges() => _dbContext.SaveChanges();
    }
}
