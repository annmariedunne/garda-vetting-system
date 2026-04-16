using GardaVettingSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace GardaVettingSystem.Tests.Models
{
    public class AccessCodeTests
    {
        private AccessCode _accessCode = default!;

        [SetUp]
        public void SetUp()
        {
            _accessCode = new AccessCode
            {
                AccessCodeId = 1,
                ApplicantNumber = 1,
                Code = "A3B4C5D6E7F8",
                OrganisationName = "Blackrock GAA",
                CreatedDate = DateTimeOffset.UtcNow,
                ExpiryDate = DateTimeOffset.UtcNow.AddDays(30),
                IsActive = true
            };
        }

        // --- Default Values ---

        [Test]
        public void AccessCode_DefaultValues_Code_IsEmpty()
        {
            var accessCode = new AccessCode();
            Assert.That(accessCode.Code, Is.EqualTo(string.Empty));
        }

        [Test]
        public void AccessCode_DefaultValues_OrganisationName_IsEmpty()
        {
            var accessCode = new AccessCode();
            Assert.That(accessCode.OrganisationName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void AccessCode_DefaultValues_IsActive_IsTrue()
        {
            var accessCode = new AccessCode();
            Assert.That(accessCode.IsActive, Is.True);
        }

        [Test]
        public void AccessCode_DefaultValues_ExpiryDate_IsNull()
        {
            var accessCode = new AccessCode();
            Assert.That(accessCode.ExpiryDate, Is.Null);
        }

        [Test]
        public void AccessCode_DefaultValues_CreatedDate_IsSet()
        {
            var accessCode = new AccessCode();
            Assert.That(accessCode.CreatedDate, Is.Not.EqualTo(default(DateTimeOffset)));
        }

        // --- Valid Model ---

        [Test]
        public void AccessCode_ValidModel_PassesValidation()
        {
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- Code ---

        [Test]
        public void AccessCode_Code_CanBeSet()
        {
            Assert.That(_accessCode.Code, Is.EqualTo("A3B4C5D6E7F8"));
        }

        [Test]
        public void AccessCode_Code_Required_FailsWhenEmpty()
        {
            _accessCode.Code = string.Empty;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("Code")));
        }

        [Test]
        public void AccessCode_Code_ExceedsMaxLength_FailsValidation()
        {
            _accessCode.Code = new string('A', 21);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("Code")));
        }

        [Test]
        public void AccessCode_Code_AtMaxLength_PassesValidation()
        {
            _accessCode.Code = new string('A', 20);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void AccessCode_Code_Is12Characters()
        {
            Assert.That(_accessCode.Code.Length, Is.EqualTo(12));
        }

        // --- OrganisationName ---

        [Test]
        public void AccessCode_OrganisationName_CanBeSet()
        {
            Assert.That(_accessCode.OrganisationName, Is.EqualTo("Blackrock GAA"));
        }

        [Test]
        public void AccessCode_OrganisationName_Required_FailsWhenEmpty()
        {
            _accessCode.OrganisationName = string.Empty;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("OrganisationName")));
        }

        [Test]
        public void AccessCode_OrganisationName_ExceedsMaxLength_FailsValidation()
        {
            _accessCode.OrganisationName = new string('A', 101);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("OrganisationName")));
        }

        [Test]
        public void AccessCode_OrganisationName_AtMaxLength_PassesValidation()
        {
            _accessCode.OrganisationName = new string('A', 100);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_accessCode, new ValidationContext(_accessCode), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- IsActive ---

        [Test]
        public void AccessCode_IsActive_CanBeRevoked()
        {
            _accessCode.IsActive = false;
            Assert.That(_accessCode.IsActive, Is.False);
        }

        // --- CreatedDate ---

        [Test]
        public void AccessCode_CreatedDate_CanBeSet()
        {
            var date = new DateTimeOffset(2026, 4, 13, 0, 0, 0, TimeSpan.Zero);
            _accessCode.CreatedDate = date;
            Assert.That(_accessCode.CreatedDate, Is.EqualTo(date));
        }

        // --- ExpiryDate ---

        [Test]
        public void AccessCode_ExpiryDate_CanBeSet()
        {
            var expiry = DateTimeOffset.UtcNow.AddDays(30);
            _accessCode.ExpiryDate = expiry;
            Assert.That(_accessCode.ExpiryDate, Is.EqualTo(expiry));
        }

        [Test]
        public void AccessCode_ExpiryDate_WhenPast_IndicatesExpired()
        {
            _accessCode.ExpiryDate = DateTimeOffset.UtcNow.AddDays(-1);
            Assert.That(_accessCode.ExpiryDate < DateTimeOffset.UtcNow, Is.True);
        }

        [Test]
        public void AccessCode_ExpiryDate_WhenFuture_IndicatesActive()
        {
            _accessCode.ExpiryDate = DateTimeOffset.UtcNow.AddDays(30);
            Assert.That(_accessCode.ExpiryDate > DateTimeOffset.UtcNow, Is.True);
        }

        // --- ApplicantNumber ---

        [Test]
        public void AccessCode_ApplicantNumber_CanBeSet()
        {
            Assert.That(_accessCode.ApplicantNumber, Is.EqualTo(1));
        }
    }
}
