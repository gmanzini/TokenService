using NUnit.Framework;
using TokenGeneratorService.Domain;

namespace TokenGenerator.Tests
{
    
    public class TokenTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidateTokenCreation_LessThan30Min()
        {
            CardDTO cardDTO = new CardDTO();

        }

        [Test]
        public void ValidateTokenCreation_MoreThan30Min()
        {
            Assert.Pass();
        }
    }
}