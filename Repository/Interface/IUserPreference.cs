namespace Repository.Interface
{
    public interface IUserPreference
    {
        public IEnumerable<Common.Models.UserPreference> GetUserPreferences();
        public void AddUserPreference(Common.Models.UserPreference userPreference);
    }
}
