using MovieStore.Data.Models.Domain;
using MovieStore.Data.Models.DTO;

namespace MovieStore.Repositories.AbstractServices
{
    public interface IMovieService
    {
        /// <summary>
        /// Add new movie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool Success, string ErrorMessage)> AddAsync(MovieModel model);

        /// <summary>
        /// Edit movie details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool Success, string ErrorMessage)> UpdateAsync(MovieModel model);

        /// <summary>
        /// Get the movie by provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Movie?> GetByIdAsync(int id);

        /// <summary>
        /// Delete the selected movie by provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <param name="term"></param>
        /// <param name="paging"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        Task<PaginatedListMovieModel?> MoviesListAsync(string term = "", bool paging = false, int currentPage = 0);

        /// <summary>
        /// Loads the movie model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MovieModel?> GetMovieModelByIdAsync(int id);
    }
}