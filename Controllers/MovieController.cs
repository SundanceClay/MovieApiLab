using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPILab.Data;
using MovieAPILab.Models;
using Microsoft.EntityFrameworkCore;


namespace MovieAPILab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            return await _context.Movies.ToListAsync();
        }
        // GET: api/Products/5
        [HttpGet("{category}")]
        public async Task<ActionResult<List<Movie>>> GetProduct(string category)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movieList = await _context.Movies.Where(x => x.Category == category).ToListAsync();

            if (movieList == null)
            {
                return NotFound();
            }

            return movieList;
        }

        [HttpGet("GetRandomMovie")]
        public async Task<ActionResult<Movie>> GetRandomMovie()
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movieList = await _context.Movies.ToListAsync();
            var random = new Random();
            int randomMovieId = movieList.OrderBy(x => random.Next()).Select(x => x.Id).FirstOrDefault();

            Movie movie = movieList.Where(x => x.Id == randomMovieId).FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("GetRandomMovieCategory/{category}")]
        public async Task<ActionResult<Movie>> GetRandomMovieCategory(string category)
        {

            if (_context.Movies == null)
            {
                return NotFound();
            }
            List<Movie> movieList = await _context.Movies.Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();
            var random = new Random();
            int randomMovieId = movieList.OrderBy(x => random.Next()).Select(x => x.Id).FirstOrDefault();

            Movie movie = movieList.Where(x => x.Id == randomMovieId).FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("GetRandomUserPicks/{HowManyMovies}")]
        public async Task<ActionResult<List<Movie>>> GetRandomUserPicks(int HowManyMovies)
        {
            var random = new Random();
            var movieListRaw = await _context.Movies.ToListAsync();
            List<Movie> movieList = movieListRaw.OrderBy(x => random.Next()).ToList();
            List<int> movieIntList = (movieList.Select(x => x.Id).Take(HowManyMovies).ToList());
            // now use the integers to get the associated movies by id...
            List<Movie> randomMovies = new List<Movie>();
            foreach(int i in movieIntList)
            {
                randomMovies.Add(await _context.Movies.Where(x => x.Id == i).FirstOrDefaultAsync());
            }

            if (randomMovies == null)
            {
                return NotFound();
            }
            return randomMovies;
        }
        [HttpGet("GetListMovieCategories")]
        public async Task<ActionResult<List<string>>> GetListMovieCategories()
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }

            List<string> categoryList = new List<string>();
            foreach(Movie movie in _context.Movies) 
            { 
                if (!categoryList.Contains(movie.Category))
                    categoryList.Add((string)movie.Category);
            }
            
            return categoryList;
        }

        [HttpGet("GetMovieInfoByTitle/{title}")]
        public async Task<ActionResult<List<Movie>>> GetMovieInfoByTitle(string title)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movieList = await _context.Movies.Where(x => x.Title == title).ToListAsync();

            if (movieList == null)
            {
                return NotFound();
            }

            return movieList;
        }

        [HttpGet("GetMovieIfKeywordInTitle/{keyword}")]
        public async Task<ActionResult<List<Movie>>> GetMovieIfKeywordInTitle(string keyword)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movieList = await _context.Movies.Where(x => x.Title.Contains(keyword)).ToListAsync();

            if (movieList == null)
            {
                return NotFound();
            }

            return movieList;
        }
    }
}
