namespace Repository.Interface
{
    public interface IUser
    {
        IEnumerable<Common.Models.User> GetUsers();
    }
}
