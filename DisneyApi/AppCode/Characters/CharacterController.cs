using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Characters;
using DisneyApi.AppCode.Common;
using DisneyApi.AppCode.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisneyApi.Characters
{
    [ApiController]
    [Authorize]
    [Route("characters")]

    public class CharacterController : ControllerBase
    {

        private readonly ICharacterQueryService _queryService;
        private readonly ICharacterCommandService _cmdService;


        public CharacterController(ICharacterQueryService queryService, ICharacterCommandService cmdService)
        {
            _queryService = queryService;
            _cmdService = cmdService;
        }


        [HttpGet]
        public IActionResult GetCharacters(string name =null, int age = -1, int movies = -1)
        {            
            return Ok(_queryService.GetCharacters(name, age, movies));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CharacterDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newCharacterId = await _cmdService.CreateCharacter(model);
            return Ok(newCharacterId);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cmdService.DeleteCharacter(id);
                return Ok();
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }


        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_queryService.GetCharacterById(id));
            }
            // catch(Exception ex)
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CharacterDTO model)
        {   
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {   
                await _cmdService.UpdateCharacterAsync(model);
                return Ok();
            }
            catch(ItemNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }
    }
}