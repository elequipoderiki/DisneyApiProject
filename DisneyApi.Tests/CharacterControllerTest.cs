using System.Collections.Generic;
using System.Linq;
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
        public async Task CreateWithValidCharacterShouldReturnOkAndNotZeroId()
        {
            FakeCharacterService stubService = new FakeCharacterService();
            var controller = new CharacterController(stubService, stubService);
            CharacterDTO newCharacter = new CharacterDTO()
            {
                Name = "Mickey",
            };

            var createdResponse = await controller.Create(newCharacter);
            
            Assert.IsType<OkObjectResult>(createdResponse);
            var result = createdResponse as OkObjectResult;
            Assert.NotNull(result);
            var createdId = result.Value;
            Assert.NotEqual(0, createdId);
        }

        [Fact]
        public async Task CreateWithInvalidCharacterShouldReturnBadRequest()
        {
            FakeCharacterService stubService = new FakeCharacterService();
            var controller = new CharacterController(stubService, stubService);
            controller.ModelState.AddModelError("Name","Required");
            CharacterDTO noNameCharacter = new CharacterDTO()
            {
            };

            var createdResponse = await controller.Create(noNameCharacter);
            
            Assert.IsType<BadRequestResult>(createdResponse);
        }

        [Fact]
        public void GetCharacterWithValidIdShouldReturnOkAndReferredCharacter()
        {
            FakeCharacterService mockService = new FakeCharacterService();
            mockService.SeedTwoCharactersInOrderWithNames("Mickey", "Donald");
            var controller = new CharacterController(mockService, mockService);

            var response = controller.Details(1);
        
            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            Assert.NotNull(result);
            var character = result.Value as CharacterFullFeatures;
            Assert.Equal("Mickey", character.Name);            
        }

        [Fact]
        public void GetCharacterWithInvalidIdShouldReturnNotFound()
        {
            FakeCharacterService mockService = new FakeCharacterService();
            mockService.SeedTwoCharactersInOrderWithNames("Mickey", "Donald");
            var controller = new CharacterController(mockService, mockService);

            var response = controller.Details(3);

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public void GetCharactersShouldReturnOkAndAllCharacters()
        {
            FakeCharacterService mockService = new FakeCharacterService();
            mockService.SeedTwoCharactersInOrderWithNames("Mickey", "Donald");
            var controller = new CharacterController(mockService, mockService);

            var response = controller.GetCharacters();

            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            Assert.NotNull(result);
            var responseCharacters = (IEnumerable<CharacterPrincipalFeatures>)result.Value;
            var responseNames = GetNamesFromResponse(responseCharacters);
            Assert.Contains<string>("Mickey",responseNames);
            Assert.Contains<string>("Donald",responseNames);
            Assert.Equal(2, responseCharacters.Count());
        }

        [Fact]
        public void FilterCharactersByExistentNameShouldReturnOkAndReferredCharacter()
        {
            FakeCharacterService mockService = new FakeCharacterService();
            mockService.SeedTwoCharactersInOrderWithNames("Mickey", "Donald");
            var controller = new CharacterController(mockService, mockService);
        
            var response = controller.GetCharacters("Mickey");

            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            Assert.NotNull(result);
            var responseCharacters = (IEnumerable<CharacterPrincipalFeatures>) result.Value;
            var responseNames = GetNamesFromResponse(responseCharacters);
            Assert.Contains<string>("Mickey",responseNames);
            Assert.DoesNotContain<string>("Donald",responseNames);
            Assert.Equal(1, responseCharacters.Count());
        }

        private IEnumerable<string> GetNamesFromResponse(IEnumerable<CharacterPrincipalFeatures> responseCharacters)
        {
            return responseCharacters.Select(c => c.Name);
        }
    }
}
