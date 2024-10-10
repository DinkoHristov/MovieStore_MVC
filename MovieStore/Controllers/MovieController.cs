using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStore.Constants;
using MovieStore.Data;
using MovieStore.Data.Models.DTO;
using MovieStore.Repositories.AbstractServices;

namespace MovieStore.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly DatabaseContext dbContext;

        public MovieController(IMovieService movieService, IGenreService genreService, 
            DatabaseContext dbContext)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Add()
        {
            var model = new MovieModel();

            var genres = await genreService.ListAsync();
            model.GenreList = genres
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(MovieModel model)
        {
            var genres = await genreService.ListAsync();
            model.GenreList = genres
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await movieService.AddAsync(model);

            if (!result.Success)
            {
                TempData["Message"] = result.ErrorMessage;

                return View();
            }

            TempData["Message"] = MovieStoreConstants.SuccessfullyAddedMovie;

            return RedirectToAction(nameof(Add));
        }

        public async Task<IActionResult> Update(int id)
        {
            var movieModel = await movieService.GetMovieModelByIdAsync(id);

            return View(movieModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(MovieModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await movieService.UpdateAsync(model);

            if (!result.Success)
            {
                TempData["Message"] = result.ErrorMessage;

                return View();
            }

            TempData["Message"] = MovieStoreConstants.SuccessfullyUpdatedMovie;

            return RedirectToAction(nameof(MovieList));
        }

        public async Task<IActionResult> MovieList() => View(await movieService.MoviesListAsync());

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await movieService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Message"] = result.ErrorMessage;

                return RedirectToAction(nameof(HomeController.Index));
            }

            TempData["Message"] = MovieStoreConstants.SuccessfullyRemovedMovie;

            return RedirectToAction(nameof(MovieList));
        }
    }
}