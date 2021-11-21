using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;
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
                var newMovieId = await _cmdService.CreateMovie(model);
                return Ok(newMovieId);
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
            try
            {
                return Ok(_queryService.GetMovieById(id));
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(MovieDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {   
                await _cmdService.UpdateMovieAsync(model);
                return Ok();
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cmdService.DeleteMovie(id);
                return Ok();
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }

    }
}