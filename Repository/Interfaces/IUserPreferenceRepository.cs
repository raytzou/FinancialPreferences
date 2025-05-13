namespace Repository.Interfaces
{
    public interface IUserPreferenceRepository
    {
        public IEnumerable<Common.Models.UserPreference> GetUserPreferences();
        public void AddUserPreference(Common.Models.UserPreference userPreference);
        public void UpdateUserPreference(Common.Models.UserPreference userPreference);
        public void DeleteUserPreference(Guid preferenceId);
    }
}
