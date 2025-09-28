using BusinessLogic.Services.Interfaces;
using Common.Models;
using Repository.Interfaces;

namespace BusinessLogic.Services
{
    public class HousePublisherService : IHousePublisherService
    {
        private readonly IHouseRepository _repository;

        public HousePublisherService(IHouseRepository repository)
        {
            _repository = repository;
        }

        public List<House> GetAllHouses() => _repository.GetAll().ToList();

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public List<string> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
