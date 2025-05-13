namespace Repository.Interfaces
{
    public interface IUser
    {
        IEnumerable<Common.Models.User> GetUsers();
    }
}
