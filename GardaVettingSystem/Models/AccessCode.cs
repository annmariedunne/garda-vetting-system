using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardaVettingSystem.Models
{
    public class AccessCode
    {
        [Key]
        public int AccessCodeId { get; set; }

        // Foreign key back to Applicant
        [ForeignKey("Applicant")]
        public int ApplicantNumber { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Code cannot exceed 20 characters")]
        [Display(Name = "Access Code")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Organisation name cannot exceed 100 characters")]
        [Display(Name = "Organisation Name")]
        public string OrganisationName { get; set; } = string.Empty;

        [Display(Name = "Created Date")]
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        [Display(Name = "Expiry Date")]
        public DateTimeOffset? ExpiryDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // Navigation property back to Applicant
        public Applicant? Applicant { get; set; }
    }
}