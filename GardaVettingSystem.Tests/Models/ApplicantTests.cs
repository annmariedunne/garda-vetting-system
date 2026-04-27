using GardaVettingSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace GardaVettingSystem.Tests.Models
{
    public class ApplicantTests
    {
        private Applicant _applicant = default!;

        [SetUp]
        public void SetUp()
        {
            _applicant = new Applicant
            {
                ApplicantNumber = 1,
                UserId = "abc-123-def-456",
                FirstName = "Joe",
                LastName = "Bloggs",
                BirthLastName = "Smith",
                MothersLastName = "Smith",
                Gender = "Male",
                DateOfBirth = new DateTimeOffset(1990, 1, 20, 0, 0, 0, TimeSpan.Zero),
                BirthPlace = "Dublin, Ireland"
            };
        }

        // --- Default Values ---

        [Test]
        public void Applicant_DefaultValues_FirstName_IsEmpty()
        {
            var applicant = new Applicant();
            Assert.That(applicant.FirstName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_DefaultValues_LastName_IsEmpty()
        {
            var applicant = new Applicant();
            Assert.That(applicant.LastName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_DefaultValues_UserId_IsEmpty()
        {
            var applicant = new Applicant();
            Assert.That(applicant.UserId, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_DefaultValues_BirthLastName_IsEmpty()
        {
            var applicant = new Applicant();
            Assert.That(applicant.BirthLastName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_DefaultValues_MothersLastName_IsEmpty()
        {
            var applicant = new Applicant();
            Assert.That(applicant.MothersLastName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_DefaultValues_BirthPlace_IsEmpty()
        {
            var applicant = new Applicant();
            Assert.That(applicant.BirthPlace, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Applicant_DefaultValues_DateOfBirth_IsNull()
        {
            var applicant = new Applicant();
            Assert.That(applicant.DateOfBirth, Is.Null);
        }

        // --- Valid Model ---

        [Test]
        public void Applicant_ValidModel_PassesValidation()
        {
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- FullName ---

        [Test]
        public void Applicant_FullName_ReturnsFirstAndLastName()
        {
            Assert.That(_applicant.FullName, Is.EqualTo("Joe Bloggs"));
        }

        // --- FirstName ---

        [Test]
        public void Applicant_FirstName_CanBeSet()
        {
            Assert.That(_applicant.FirstName, Is.EqualTo("Joe"));
        }

        [Test]
        public void Applicant_FirstName_Required_FailsWhenEmpty()
        {
            _applicant.FirstName = string.Empty;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("FirstName")));
        }

        [Test]
        public void Applicant_FirstName_ExceedsMaxLength_FailsValidation()
        {
            _applicant.FirstName = new string('A', 51);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("FirstName")));
        }

        [Test]
        public void Applicant_FirstName_AtMaxLength_PassesValidation()
        {
            _applicant.FirstName = new string('A', 50);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- LastName ---

        [Test]
        public void Applicant_LastName_CanBeSet()
        {
            Assert.That(_applicant.LastName, Is.EqualTo("Bloggs"));
        }

        [Test]
        public void Applicant_LastName_Required_FailsWhenEmpty()
        {
            _applicant.LastName = string.Empty;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("LastName")));
        }

        [Test]
        public void Applicant_LastName_ExceedsMaxLength_FailsValidation()
        {
            _applicant.LastName = new string('A', 51);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("LastName")));
        }

        // --- Gender ---

        [Test]
        public void Applicant_Gender_CanBeSet()
        {
            Assert.That(_applicant.Gender, Is.EqualTo("Male"));
        }

        [Test]
        public void Applicant_Gender_Required_FailsWhenEmpty()
        {
            _applicant.Gender = string.Empty;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("Gender")));
        }

        // --- DateOfBirth ---

        [Test]
        public void Applicant_DateOfBirth_CanBeSet()
        {
            Assert.That(_applicant.DateOfBirth, Is.Not.Null);
            Assert.That(_applicant.DateOfBirth?.Year, Is.EqualTo(1990));
            Assert.That(_applicant.DateOfBirth?.Month, Is.EqualTo(1));
            Assert.That(_applicant.DateOfBirth?.Day, Is.EqualTo(20));
        }

        // --- BirthLastName ---

        [Test]
        public void Applicant_BirthLastName_CanBeSet()
        {
            Assert.That(_applicant.BirthLastName, Is.EqualTo("Smith"));
        }

        [Test]
        public void Applicant_BirthLastName_IsOptional_PassesValidationWhenEmpty()
        {
            _applicant.BirthLastName = string.Empty;
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Applicant_BirthLastName_ExceedsMaxLength_FailsValidation()
        {
            _applicant.BirthLastName = new string('A', 51);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("BirthLastName")));
        }

        // --- MothersLastName ---

        [Test]
        public void Applicant_MothersLastName_CanBeSet()
        {
            Assert.That(_applicant.MothersLastName, Is.EqualTo("Smith"));
        }

        [Test]
        public void Applicant_MothersLastName_IsOptional_PassesValidationWhenEmpty()
        {
            _applicant.MothersLastName = string.Empty;
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Applicant_MothersLastName_ExceedsMaxLength_FailsValidation()
        {
            _applicant.MothersLastName = new string('A', 51);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("MothersLastName")));
        }

        // --- BirthPlace ---

        [Test]
        public void Applicant_BirthPlace_CanBeSet()
        {
            Assert.That(_applicant.BirthPlace, Is.EqualTo("Dublin, Ireland"));
        }

        [Test]
        public void Applicant_BirthPlace_IsOptional_PassesValidationWhenEmpty()
        {
            _applicant.BirthPlace = string.Empty;
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Applicant_BirthPlace_ExceedsMaxLength_FailsValidation()
        {
            _applicant.BirthPlace = new string('A', 51);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_applicant, new ValidationContext(_applicant), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("BirthPlace")));
        }

        // --- ApplicantNumber ---

        [Test]
        public void Applicant_ApplicantNumber_CanBeSet()
        {
            Assert.That(_applicant.ApplicantNumber, Is.EqualTo(1));
        }

        // --- UserId ---

        [Test]
        public void Applicant_UserId_CanBeSet()
        {
            Assert.That(_applicant.UserId, Is.EqualTo("abc-123-def-456"));
        }
    }
}
