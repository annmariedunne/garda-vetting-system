using GardaVettingSystem.Models;

namespace GardaVettingSystem.Tests.Models
{
    public class ApplicantAddressTests
    {
        [Test]
        public void ApplicantAddress_DefaultValues_AreCorrect()
        {
            // Arrange
            var address = new ApplicantAddress();

            // Assert
            Assert.That(address.AddressLine, Is.EqualTo(string.Empty));
        }
    }
}
