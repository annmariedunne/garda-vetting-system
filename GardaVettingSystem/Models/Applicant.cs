using System.ComponentModel.DataAnnotations;

namespace GardaVettingSystem.Models
{
    public class Applicant
    {
        // Primary key - auto-generated applicant number
        [Key]
        public int ApplicantNumber { get; set; }

        public string UserId { get; set; } = string.Empty;

        // Member's first and last name stored separately
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        [StringLength(50, ErrorMessage = "Birth surname name cannot exceed 50 characters")]
        [Display(Name = "Surname at birth")]
        public string BirthLastName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Mother's last name cannot exceed 50 characters")]
        [Display(Name = "Mother's last name")]
        public string MothersLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date of birth")]
        public DateTimeOffset? DateOfBirth { get; set; }

        [StringLength(50, ErrorMessage = "Birth place cannot exceed 50 characters")]
        [Display(Name = "Place of birth")]
        public string BirthPlace { get; set; } = string.Empty;

        // Navigation property - an applicant can have multiple addresses
        public ICollection<ApplicantAddress> ApplicantAddresses { get; set; } = new List<ApplicantAddress>();
        public ICollection<AccessCode> AccessCodes { get; set; } = new List<AccessCode>();
    }

}
