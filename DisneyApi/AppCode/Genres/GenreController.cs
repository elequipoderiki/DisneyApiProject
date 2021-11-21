using System;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;
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
            try
            {
                var newGenreId = await _cmdService.CreateGenre(model);
                return Ok(newGenreId);
            }
            catch (ItemAlreadyExistsException ex)
            {
                return BadRequest(new { message = ex.Message, errorCode = ex.StatusCode});
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_queryService.GetGenres());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_queryService.GetGenreById(id));
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }


        [HttpDelete]
        public IActionResult Delete(int genreId)
        {
            try{
                _cmdService.DeleteGenre(genreId);
                return Ok();
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }

    }
}