namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<Common.Models.User> GetUsers();
    }
}
