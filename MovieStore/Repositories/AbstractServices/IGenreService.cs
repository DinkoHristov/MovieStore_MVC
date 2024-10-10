using MovieStore.Data.Models.Domain;

namespace MovieStore.Repositories.AbstractServices
{
    public interface IGenreService
    {
        /// <summary>
        /// Add new Genre
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool Success, string ErrorMessage)> AddAsync(Genre model);

        /// <summary>
        /// Update the selected Genre
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool Success, string ErrorMessage)> UpdateAsync(Genre model);

        /// <summary>
        /// Get Genre by provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Genre?> GetByIdAsync(int id);

        /// <summary>
        /// Delete the selected Genre with the provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);

        /// <summary>
        /// Get all Genres
        /// </summary>
        /// <returns></returns>
        Task<List<Genre>?> ListAsync();
    }
}