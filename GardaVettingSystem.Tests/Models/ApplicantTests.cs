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
                FirstName = "Joe",
                LastName = "Bloggs"
            };

            // Assert
            Assert.That(applicant.FullName, Is.EqualTo("Joe Bloggs"));
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

        [Test]
        public void Applicant_DateOfBirth_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                DateOfBirth = new DateTimeOffset(1990, 1, 20, 0, 0, 0, TimeSpan.Zero)
            };

            // Assert
            Assert.That(applicant.DateOfBirth, Is.Not.Null);
            Assert.That(applicant.DateOfBirth?.Year, Is.EqualTo(1990));
            Assert.That(applicant.DateOfBirth?.Month, Is.EqualTo(1));
            Assert.That(applicant.DateOfBirth?.Day, Is.EqualTo(20));
        }

        [Test]
        public void Applicant_DateOfBirth_IsNullByDefault()
        {
            // Arrange
            var applicant = new Applicant();

            // Assert
            Assert.That(applicant.DateOfBirth, Is.Null);
        }

        [Test]
        public void Applicant_Gender_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                Gender = "Female"
            };

            // Assert
            Assert.That(applicant.Gender, Is.EqualTo("Female"));
        }

        [Test]
        public void Applicant_BirthLastName_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                BirthLastName = "Bloggs"
            };

            // Assert
            Assert.That(applicant.BirthLastName, Is.EqualTo("Bloggs"));
        }

        [Test]
        public void Applicant_BirthLastName_DefaultValue_IsEmpty()
        {
            // Arrange
            var applicant = new Applicant();

            // Assert
            Assert.That(applicant.BirthLastName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_MothersLastName_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                MothersLastName = "Smith"
            };

            // Assert
            Assert.That(applicant.MothersLastName, Is.EqualTo("Smith"));
        }

        [Test]
        public void Applicant_MothersLastName_DefaultValue_IsEmpty()
        {
            // Arrange
            var applicant = new Applicant();

            // Assert
            Assert.That(applicant.MothersLastName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_BirthPlace_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                BirthPlace = "Dublin, Ireland"
            };

            // Assert
            Assert.That(applicant.BirthPlace, Is.EqualTo("Dublin, Ireland"));
        }

        [Test]
        public void Applicant_BirthPlace_DefaultValue_IsEmpty()
        {
            // Arrange
            var applicant = new Applicant();

            // Assert
            Assert.That(applicant.BirthPlace, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_ApplicantNumber_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                ApplicantNumber = 1
            };

            // Assert
            Assert.That(applicant.ApplicantNumber, Is.EqualTo(1));
        }

        [Test]
        public void Applicant_UserId_CanBeSet()
        {
            // Arrange
            var applicant = new Applicant
            {
                UserId = "abc-123-def-456"
            };

            // Assert
            Assert.That(applicant.UserId, Is.EqualTo("abc-123-def-456"));
        }
    }
}
