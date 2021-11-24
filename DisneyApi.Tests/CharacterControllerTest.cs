using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisneyApi.AppCode.Characters;
using DisneyApi.Characters;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DisneyApi.Tests
{
    public class CharacterControllerTest
    {
        [Fact]
        public async Task AddingValidCharacterReturnsOkAndNotZeroId()
        {
            CharacterService characterService = new CharacterService();
            var controller = new CharacterController(characterService, characterService);
            CharacterDTO newCharacter = new CharacterDTO()
            {
                Name = "Dumbo",
            };

            var createdResponse = await controller.Create(newCharacter);
            
            Assert.IsType<OkObjectResult>(createdResponse);
            var result = createdResponse as OkObjectResult;
            var createdId = result.Value;
            Assert.NotEqual(0, createdId);
        }

        [Fact]
        public async Task AddingInvalidCharacterReturnsBadRequest()
        {
            CharacterService characterService = new CharacterService();
            var controller = new CharacterController(characterService, characterService);
            controller.ModelState.AddModelError("Name","Required");
            CharacterDTO newCharacter = new CharacterDTO()
            {
            };

            var createdResponse = await controller.Create(newCharacter);
            
            Assert.IsType<BadRequestResult>(createdResponse);
        }

        [Fact]
        public void GettingCharacterWithValidIdReturnsCharacter()
        {
            //Given
            CharacterService characterService = new CharacterService();
            var controller = new CharacterController(characterService, characterService);

            //When
            var response = controller.Details(1);
        
            //Then
            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            var character = result.Value as CharacterFullFeatures;
            Assert.Equal("Mickey", character.Name);            
            Assert.Equal(30, character.Age);            
        }

    }
}
