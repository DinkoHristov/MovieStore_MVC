using MovieStore.Data.Models.DTO;

namespace MovieStore.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Status> LoginAsync(LoginModel model);

        /// <summary>
        /// Logout the current user
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}
