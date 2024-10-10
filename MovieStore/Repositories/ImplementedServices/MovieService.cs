using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Data.Models.Domain;
using MovieStore.Data.Models.DTO;
using MovieStore.Repositories.AbstractServices;

namespace MovieStore.Repositories.ImplementedServices
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext dbContext;

        public MovieService(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(bool Success, string ErrorMessage)> AddAsync(MovieModel model)
        {
            try
            {
                var isMovieExists = await dbContext.Movies.AnyAsync(g => g.Title == model.Title);
                if (isMovieExists)
                    return (false, "This Movie is already created!");

                var movie = new Movie()
                {
                    Title = model.Title,
                    ReleasedYear = model.ReleasedYear,
                    Cast = model.Cast,
                    Director = model.Director,
                    MovieImage = model.MovieImage,
                };

                await dbContext.Movies.AddAsync(movie);

                foreach (var genreId in model.GenresIds)
                {
                    var movieGenre = new MovieGenre()
                    {
                        MovieId = movie.Id,
                        Movie = await dbContext.Movies.FindAsync(movie.Id),
                        GenreId = genreId,
                        Genre = await dbContext.Genres.FindAsync(genreId)
                    };

                    movie.Genres.Add(movieGenre);
                }

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
                var movie = await GetByIdAsync(id);

                if (movie is null)
                    return (false, "Movie could not be found");

                var movieGenres = await dbContext.MovieGenres.Where(m => m.MovieId == movie.Id).ToListAsync();
                foreach (var movieGenre in movieGenres)
                {
                    dbContext.MovieGenres.Remove(movieGenre);
                }

                dbContext.Movies.Remove(movie);
                await dbContext.SaveChangesAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<Movie?> GetByIdAsync(int id)
            => await dbContext.Movies.FindAsync(id);

        public async Task<MovieModel?> GetMovieModelByIdAsync(int id)
        {
            return await dbContext.Movies
                .Select(m => new MovieModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleasedYear = m.ReleasedYear,
                    MovieImage = m.MovieImage,
                    Director = m.Director,
                    Cast = m.Cast,
                    GenresIds = m.Genres.Select(g => g.Genre.Id).ToList(), // Load the selected genres
                    GenreList = dbContext.Genres.Select(g => new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.Id.ToString(),
                        Selected = m.Genres.Any(mg => mg.Genre.Id == g.Id) // Mark selected genres
                    }).ToList()
                })
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PaginatedListMovieModel?> MoviesListAsync(string term = "", bool paging = false, int currentPage = 0)
        {
            var moviesModel = new PaginatedListMovieModel();
            var movies = new List<MovieModel>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                movies = await dbContext.Movies
                    .Where(m => m.Title.ToLower().StartsWith(term.ToLower()))
                    .AsNoTracking()
                    .Select(m => new MovieModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        ReleasedYear = m.ReleasedYear,
                        MovieImage = m.MovieImage,
                        Director = m.Director,
                        Cast = m.Cast,
                        GenreList = m.Genres.Select(g => new SelectListItem
                        {
                            Text = g.Genre.Name,
                            Value = g.Genre.Id.ToString()
                        })
                        .ToList()
                    })
                    .ToListAsync();
            }
            else
            {
                movies = await dbContext.Movies
                    .AsNoTracking()
                    .Select(m => new MovieModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        ReleasedYear = m.ReleasedYear,
                        MovieImage = m.MovieImage,
                        Director = m.Director,
                        Cast = m.Cast,
                        GenreList = m.Genres.Select(g => new SelectListItem
                        {
                            Text = g.Genre.Name,
                            Value = g.Genre.Id.ToString()
                        })
                        .ToList()
                    })
                    .ToListAsync();
            }

            if (paging)
            {
                int pageSize = 10;
                int count = movies.Count;
                int totalPages = (int)Math.Ceiling(count / (double)pageSize);

                movies = movies.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                moviesModel.PageSize = pageSize;
                moviesModel.CurrentPage = currentPage;
                moviesModel.TotalPages = totalPages;
            }

            moviesModel.MovieList = movies;

            return moviesModel;
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(MovieModel model)
        {
            try
            {
                var dbMovie = await GetByIdAsync((int)model.Id);

                if (dbMovie is null)
                    return (false, "Movie is not found!");

                dbMovie.Title = model.Title;
                dbMovie.ReleasedYear = model.ReleasedYear;
                dbMovie.Cast = model.Cast;
                dbMovie.Director = model.Director;
                dbMovie.MovieImage = model.MovieImage;

                var dbMovieGenres = await dbContext.MovieGenres.Where(m => m.MovieId == dbMovie.Id).ToListAsync();
                dbContext.RemoveRange(dbMovieGenres);

                await dbContext.SaveChangesAsync();

                foreach (var genreId in model.GenresIds)
                {
                    var movieGenre = new MovieGenre()
                    {
                        MovieId = dbMovie.Id,
                        Movie = await dbContext.Movies.FindAsync(dbMovie.Id),
                        GenreId = genreId,
                        Genre = await dbContext.Genres.FindAsync(genreId)
                    };

                    dbMovie.Genres.Add(movieGenre);
                }

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