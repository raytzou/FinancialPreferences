namespace BussinessLogic.Services.Interfaces
{
    public interface IFinancialPreferencesService
    {
        /// <summary>
        /// Validates the user preference.
        /// </summary>
        /// <param name="userPreference"></param>
        /// <returns>Error messages.</returns>
        List<string> Validate(Common.Models.UserPreference userPreference);
    }
}
