using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisneyApi.AppCode.Genres
{
    [ApiController]
    [Authorize]
    [Route("genre")]
    public class GenreController : ControllerBase
    {
        
        private readonly IGenreCommandService _cmdService;
        private readonly IGenreQueryService _queryService;
        public GenreController(IGenreCommandService cmdService, IGenreQueryService queryService)
        {
            _cmdService = cmdService;
            _queryService = queryService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(GenreDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var nameAlreadyTaken = _cmdService.IsExistentName(model);
            if(nameAlreadyTaken)
            {
                return BadRequest("Name already exists");
            }        
            return Ok(await _cmdService.CreateGenre(model));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_queryService.GetGenres());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var result = _queryService.GetGenreById(id);
            if(result == null)
                return NotFound("Genre not found");
            
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _cmdService.DeleteGenre(id);
            if(success)
                return Ok();

            return NotFound("Genre not found");
        }

    }
}