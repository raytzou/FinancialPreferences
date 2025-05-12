namespace Repository.Interface
{
    public interface IUser
    {
        IEnumerable<Common.User> GetUsers();
    }
}
