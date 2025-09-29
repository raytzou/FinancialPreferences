using Common.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHousePublisherService
    {
        List<House> GetAllHouses();
        void Create(string houseName, string address, decimal totalPrice, decimal floorArea, string description);
        void Update(House content);
        void Delete(string id);
        List<string> Validate(House content);
    }
}
