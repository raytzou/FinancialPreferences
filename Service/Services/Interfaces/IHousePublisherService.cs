using Common.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHousePublisherService
    {
        List<House> GetAllHouses();
        void Create();
        void Update();
        void Delete();
        List<string> Validate(House content);
    }
}
