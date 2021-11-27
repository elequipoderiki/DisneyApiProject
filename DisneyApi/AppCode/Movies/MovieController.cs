using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisneyApi.AppCode.Movies
{
    [ApiController]
    [Authorize]
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieCommandService _cmdService;
        private readonly IMovieQueryService _queryService;

        public MovieController(IMovieQueryService queryService, IMovieCommandService cmdService)
        {
            _queryService = queryService;
            _cmdService = cmdService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(MovieDTO model)
        {            
            if(ModelState.IsValid)
            {
                return Ok(await _cmdService.CreateMovieAsync(model));
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetMovies(string name = null, int genre = -1, int order = -1)
        {
            return Ok(_queryService.GetMovies(name, genre, order));
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var result = _queryService.GetMovieById(id);
            if(result == null)
                return NotFound("Item not found");

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MovieDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var success = await _cmdService.UpdateMovieAsync(model);
            if(success)
                return Ok();
                
            return NotFound("Item not found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _cmdService.DeleteMovie(id);
            if(success)
                return Ok();

            return NotFound("Item not found");
        }

    }
}