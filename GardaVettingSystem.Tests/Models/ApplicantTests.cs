using GardaVettingSystem.Models;

namespace GardaVettingSystem.Tests.Models
{
    public class ApplicantTests
    {
        [Test]
        public void Applicant_FullName_ReturnsFirstAndLastName()
        {
            // Arrange
            var applicant = new Applicant
            {
                FirstName = "Ann Marie",
                LastName = "Dunne"
            };

            // Assert
            Assert.That(applicant.FullName, Is.EqualTo("Ann Marie Dunne"));
        }

        [Test]
        public void Applicant_DefaultValues_AreNotNull()
        {
            // Arrange
            var applicant = new Applicant();

            // Assert
            Assert.That(applicant.FirstName, Is.EqualTo(string.Empty));
            Assert.That(applicant.LastName, Is.EqualTo(string.Empty));
            Assert.That(applicant.UserId, Is.EqualTo(string.Empty));
        }
    }
}
