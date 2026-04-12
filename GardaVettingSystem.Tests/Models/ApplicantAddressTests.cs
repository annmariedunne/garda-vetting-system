using GardaVettingSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace GardaVettingSystem.Tests.Models
{
    public class ApplicantAddressTests
    {
        private ApplicantAddress _address = default!;

        [SetUp]
        public void SetUp()
        {
            _address = new ApplicantAddress
            {
                AddressLine = "123 Main Street",
                Postcode = "D01 AB12",
                Country = "Ireland",
                ResidentFrom = 2020,
                ResidentTo = null,
                ApplicantNumber = 1
            };
        }

        // --- Default Values ---

        [Test]
        public void ApplicantAddress_DefaultValues_AddressLine_IsEmpty()
        {
            var address = new ApplicantAddress();
            Assert.That(address.AddressLine, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ApplicantAddress_DefaultValues_Postcode_IsEmpty()
        {
            var address = new ApplicantAddress();
            Assert.That(address.Postcode, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ApplicantAddress_DefaultValues_Country_IsEmpty()
        {
            var address = new ApplicantAddress();
            Assert.That(address.Country, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ApplicantAddress_DefaultValues_ResidentTo_IsNull()
        {
            var address = new ApplicantAddress();
            Assert.That(address.ResidentTo, Is.Null);
        }

        // --- Valid Model ---

        [Test]
        public void ApplicantAddress_ValidModel_PassesValidation()
        {
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- AddressLine ---

        [Test]
        public void ApplicantAddress_AddressLine_Required_FailsWhenEmpty()
        {
            _address.AddressLine = string.Empty;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("AddressLine")));
        }

        [Test]
        public void ApplicantAddress_AddressLine_ExceedsMaxLength_FailsValidation()
        {
            _address.AddressLine = new string('A', 251);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("AddressLine")));
        }

        [Test]
        public void ApplicantAddress_AddressLine_AtMaxLength_PassesValidation()
        {
            _address.AddressLine = new string('A', 250);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- Postcode ---

        [Test]
        public void ApplicantAddress_Postcode_ExceedsMaxLength_FailsValidation()
        {
            _address.Postcode = new string('A', 11);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("Postcode")));
        }

        [Test]
        public void ApplicantAddress_Postcode_IsOptional_PassesValidationWhenEmpty()
        {
            _address.Postcode = string.Empty;
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- Country ---

        [Test]
        public void ApplicantAddress_Country_ExceedsMaxLength_FailsValidation()
        {
            _address.Country = new string('A', 51);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
                r.MemberNames.Contains("Country")));
        }

        [Test]
        public void ApplicantAddress_Country_IsOptional_PassesValidationWhenEmpty()
        {
            _address.Country = string.Empty;
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(_address, new ValidationContext(_address), results, true);
            Assert.That(isValid, Is.True);
        }

        // --- ResidentFrom ---

        [Test]
        public void ApplicantAddress_ResidentFrom_IsSet_ReturnsCorrectValue()
        {
            Assert.That(_address.ResidentFrom, Is.EqualTo(2020));
        }

        // --- ResidentTo ---

        [Test]
        public void ApplicantAddress_ResidentTo_WhenNull_IndicatesCurrentAddress()
        {
            _address.ResidentTo = null;
            Assert.That(_address.ResidentTo, Is.Null);
        }

        [Test]
        public void ApplicantAddress_ResidentTo_WhenSet_StoresCorrectYear()
        {
            _address.ResidentTo = 2022;
            Assert.That(_address.ResidentTo, Is.EqualTo(2022));
        }

        // --- Foreign Key ---

        [Test]
        public void ApplicantAddress_ApplicantNumber_IsSet_ReturnsCorrectValue()
        {
            Assert.That(_address.ApplicantNumber, Is.EqualTo(1));
        }
    }
}
