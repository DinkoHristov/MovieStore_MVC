using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Constants;
using MovieStore.Data.Models.Domain;
using MovieStore.Repositories.AbstractServices;

namespace MovieStore.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await genreService.AddAsync(model);

            if (!result.Success)
            {
                TempData["Message"] = result.ErrorMessage;

                return View();
            }

            TempData["Message"] = MovieStoreConstants.SuccessfullyAddedGenre;

            return RedirectToAction(nameof(Add));
        }

        public async Task<IActionResult> Update(int id)
        {
            var genre = await genreService.GetByIdAsync(id);

            return View(genre);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await genreService.UpdateAsync(model);

            if (!result.Success)
            {
                TempData["Message"] = result.ErrorMessage;

                return View();
            }

            TempData["Message"] = MovieStoreConstants.SuccessfullyUpdatedGenre;

            return RedirectToAction(nameof(GenreList));
        }

        public async Task<IActionResult> GenreList() => View(await genreService.ListAsync());

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await genreService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Message"] = result.ErrorMessage;

                return RedirectToAction(nameof(HomeController.Index), MovieStoreConstants.HomeControllerName);
            }

            TempData["Message"] = MovieStoreConstants.SuccessfullyRemovedGenre;

            return RedirectToAction(nameof(GenreList));
        }
    }
}