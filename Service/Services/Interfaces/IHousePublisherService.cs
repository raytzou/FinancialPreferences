namespace BusinessLogic.Services.Interfaces
{
    public interface IHousePublisherService
    {
        void Create();
        void Update();
        void Delete();
        List<string> Validate();
    }
}
