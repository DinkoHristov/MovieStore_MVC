using Microsoft.AspNetCore.Mvc;
using MovieStore.Repositories.AbstractServices;

namespace MovieStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService movieService;

        public HomeController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> Index(string term = "", int currentPage = 1)
        {
            var movies = await movieService.MoviesListAsync(term, true, currentPage);

            return View(movies);
        }

        public async Task<IActionResult> MovieDetails(int id)
        {
            var movie = await movieService.GetMovieModelByIdAsync(id);

            return View(movie);
        }
    }
}