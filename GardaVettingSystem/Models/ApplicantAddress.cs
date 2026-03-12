using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardaVettingSystem.Models
{
    public class ApplicantAddress
    {

        [Key]
        public int AddressId { get; set; }

        // Foreign key back to Applicant
        [ForeignKey("Applicant")]
        public int ApplicantNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        [Display(Name = "Address")]
        public string AddressLine { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "Postcode/Eircode cannot exceed 10 characters")]
        [Display(Name = "Postcode/Eircode")]
        public string Postcode { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Resident from year is required")]
        [Display(Name = "Resident From")]
        public int ResidentFrom { get; set; }

        // Null means this is the current address
        [Display(Name = "Resident To")]
        public int? ResidentTo { get; set; }

        // Navigation property back to Applicant
        public Applicant? Applicant { get; set; }

    }

}
