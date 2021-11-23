using System;
using System.Collections.Generic;
using DisneyApi.AppCode.Characters;
using DisneyApi.Characters;
using Xunit;

namespace DisneyApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            CharacterService queryService = new CharacterService();
            var controller = new CharacterController(queryService, null);
            
        }

    }
}
