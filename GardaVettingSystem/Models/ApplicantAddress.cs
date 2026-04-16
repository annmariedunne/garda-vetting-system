using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardaVettingSystem.Models
{
    /// <summary>
    /// Represents a single address entry in an applicant's address history.
    /// An applicant can have multiple addresses covering different periods of residence.
    /// Address history is a standard requirement for Garda vetting applications.
    /// </summary>
    public class ApplicantAddress
    {
        /// <summary>
        /// Primary key — auto-generated unique identifier for the address record.
        /// </summary>
        [Key]
        public int AddressId { get; set; }

        /// <summary>
        /// Foreign key referencing the <see cref="Applicant"/> this address belongs to.
        /// Always set server-side — never accepted from form input.
        /// </summary>
        [ForeignKey("Applicant")]
        public int ApplicantNumber { get; set; }

        /// <summary>
        /// Gets or sets the full address line for this residence.
        /// </summary>
        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        [Display(Name = "Address")]
        public string AddressLine { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postcode for this address.
        /// Stored as Postcode rather than Eircode to accommodate applicants who lived abroad,
        /// as Eircode is Ireland-specific. Optional field.
        /// </summary>
        [StringLength(10, ErrorMessage = "Postcode/Eircode cannot exceed 10 characters")]
        [Display(Name = "Postcode/Eircode")]
        public string Postcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country for this address.
        /// Optional field.
        /// </summary>
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the year the applicant began residing at this address.
        /// Stored as an integer year rather than a full date — a deliberate design decision
        /// as Garda vetting applications require year-level precision only.
        /// </summary>
        [Required(ErrorMessage = "Resident from year is required")]
        [Display(Name = "Resident From")]
        public int ResidentFrom { get; set; }

        /// <summary>
        /// Gets or sets the year the applicant stopped residing at this address.
        /// A null value indicates this is the applicant's current address.
        /// </summary>
        [Display(Name = "Resident To")]
        public int? ResidentTo { get; set; }

        /// <summary>
        /// Navigation property — the applicant this address belongs to.
        /// </summary>
        public Applicant? Applicant { get; set; }

    }

}
