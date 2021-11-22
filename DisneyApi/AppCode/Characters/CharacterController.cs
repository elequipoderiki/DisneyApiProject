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
            return Ok(await _cmdService.CreateCharacter(model));
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _cmdService.DeleteCharacter(id);
            if(success)
                return Ok();
            
            return NotFound("Item not found");
        }


        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var result = _queryService.GetCharacterById(id);
            if(result == null)
                return NotFound("Item not found");
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CharacterDTO model)
        {   
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var success = await _cmdService.UpdateCharacterAsync(model);
            if(success)
                return Ok();

            return NotFound("Item not found");
        }
    }
}