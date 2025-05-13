namespace Repository.Interface
{
    public interface IUserPreference
    {
        public IEnumerable<Common.UserPreference> GetUserPreferences();
    }
}
