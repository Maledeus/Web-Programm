using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WifiSD.Common.Extensions.SD.Common.Extensions;
using WifiSD.Core.Application.Queries;
using WifiSD.Core.Entities.Movies;
using WifiSD.Persistence.Repositories.DBContext;
using WifiSD.WebAPP.Extensions;
using WifiSD.WebAPP.Models;

namespace WifiSD.WebAPP.Controllers
{
    public class MovieController : BaseController
    {
        private readonly MovieDbContext _context;

        public MovieController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index([FromQuery] GetMovieDtosQuery query, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(query, cancellationToken);
            return View(result);    



            /*
            var movieDbContext = _context.Movies.Include(m => m.Genre).Include(m => m.MediumType);
            return View(await movieDbContext.ToListAsync());
            */
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details([FromRoute] GetMovieDtoQuery query, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(query, cancellationToken);
            return View(result);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id");
            ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,Id,Name,Price,ReleaseDate,GenreId,MediumTypeCode")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.Id = Guid.NewGuid();
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", movie.GenreId);
            ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code", movie.MediumTypeCode);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit([FromRoute] GetMovieDtoQuery query, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            var movieModel = new MovieModel { MovieDto = result };

            await this.InitGenresAndMediumTypes(movieModel, result.GenreId, result.MediumTypeCode, result.Rating, cancellationToken);

            return View(movieModel);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Rating,Id,Name,Price,ReleaseDate,GenreId,MediumTypeCode")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", movie.GenreId);
            ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code", movie.MediumTypeCode);
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MediumType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Private Helpers

        private async Task InitGenresAndMediumTypes(MovieModel model, int? genreId = default, string mediumTypeCode = default,Ratings? rating = default, CancellationToken cancellationToken = default)
        {
            var genres = HttpContext.Session.Get<IEnumerable<Genre>>("Genres");
            if (genres == null)
            {
                genres = await base.Mediator.Send(new GetGenresQuery(), cancellationToken);
                HttpContext.Session.Set<IEnumerable<Genre>>("Genres", genres);
            }

            var mediumTypes = HttpContext.Session.Get<IEnumerable<MediumType>>("MediumTypes");
            if (mediumTypes == null)
            {
                mediumTypes = await base.Mediator.Send(new GetMediumTypesQuery(), cancellationToken);
                HttpContext.Session.Set<IEnumerable<MediumType>>("MediumTypes", mediumTypes);
            }

            /* Localisierten Ratings */
            var localizedRatings = HttpContext.Session.Get<List<KeyValuePair<object, string>>>("Ratings");
            if (localizedRatings == null)
            {
                localizedRatings = EnumExtensions.EnumToList<Ratings>();
                HttpContext.Session.Set<List<KeyValuePair<object, string>>>("Ratings", localizedRatings);
            }

            var genreSelectList = new SelectList(genres, nameof(Genre.Id), nameof(Genre.Name), genreId);
            var mediumTypeSelectList = new SelectList(mediumTypes, nameof(MediumType.Code), nameof(MediumType.Name), mediumTypeCode);
            var ratingsSelectList = new SelectList(localizedRatings, "Key", "Value", rating);

            if (model != null)
            {
                model.Genres = genreSelectList;
                model.MediumTypes = mediumTypeSelectList;
                model.Ratings = ratingsSelectList;
            }

            else
            {
                ViewBag.Genres = genreSelectList;
                ViewBag.MediumTypes = mediumTypeSelectList;
                ViewBag.Ratings = ratingsSelectList;
                                
            }


        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
        #endregion
    }
}
