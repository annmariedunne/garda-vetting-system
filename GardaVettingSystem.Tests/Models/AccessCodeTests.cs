using GardaVettingSystem.Models;

namespace GardaVettingSystem.Tests.Models
{
    public class AccessCodeTests
    {
        [Test]
        public void AccessCode_DefaultValues_AreCorrect()
        {
            // Arrange
            var accessCode = new AccessCode();

            // Assert
            Assert.That(accessCode.IsActive, Is.True);
        }
    }
}
