using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Data.Models.Domain;
using MovieStore.Repositories.AbstractServices;

namespace MovieStore.Repositories.ImplementedServices
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext dbContext;

        public GenreService(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(bool Success, string ErrorMessage)> AddAsync(Genre model)
        {
            try
            {
                var isGenreExists = await dbContext.Genres.AnyAsync(g => g.Name == model.Name);
                if (isGenreExists)
                    return (false, "This Genre is already created!");

                await dbContext.Genres.AddAsync(model);
                await dbContext.SaveChangesAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var genre = await GetByIdAsync(id);

                if (genre is null)
                    return (false, "Genre could not be found");

                dbContext.Genres.Remove(genre);
                await dbContext.SaveChangesAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<Genre?> GetByIdAsync(int id)
            => await dbContext.Genres.FindAsync(id);

        public async Task<List<Genre>?> ListAsync()
            => await dbContext.Genres.ToListAsync();

        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(Genre model)
        {
            try
            {
                dbContext.Genres.Update(model);
                await dbContext.SaveChangesAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}