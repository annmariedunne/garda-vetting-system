using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    /// <summary>
    /// Handles validation of an access code entered by an organisation.
    /// <para>This is a public page — no authentication is required.</para>
    /// <para>An organisation enters a code to view the associated applicant's vetting data,
    /// provided the code is active and has not expired.</para>
    /// </summary>
    public class ValidateModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        /// <summary>
        /// Initialises a new instance of <see cref="ValidateModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ValidateModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The access code being validated, bound from the form post.
        /// </summary>
        [BindProperty]
        public string EnteredCode { get; set; } = string.Empty;

        /// <summary>
        /// The applicant profile returned when a valid code is found.
        /// <para>Null if no valid code was found.</para>
        /// </summary>
        public Applicant? FoundApplicant { get; set; }

        /// <summary>
        /// An error message to display when the code is invalid, expired or revoked.
        /// <para>Null if no error occurred.</para>
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Handles GET requests. Renders the code entry form.
        /// </summary>
        /// <returns>The Validate page.</returns>
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// Handles POST requests.
        /// <para>Looks up the entered code and returns the associated
        /// applicant's data if the code is valid, active and not expired.</para>
        /// </summary>
        /// <returns>The Validate page with results or an error message.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(EnteredCode))
            {
                ErrorMessage = "Please enter an access code.";
                return Page();
            }

            // Normalise to uppercase to handle case-insensitive entry
            var normalisedCode = EnteredCode.Trim().ToUpperInvariant();

            var accessCode = await _context.AccessCodes
                .Include(a => a.Applicant)
                    .ThenInclude(a => a!.ApplicantAddresses)
                .FirstOrDefaultAsync(a => a.Code == normalisedCode);

            if (accessCode == null)
            {
                ErrorMessage = "The access code entered was not found.";
                return Page();
            }

            if (!accessCode.IsActive)
            {
                ErrorMessage = "This access code has been revoked.";
                return Page();
            }

            if (accessCode.ExpiryDate.HasValue && accessCode.ExpiryDate.Value < DateTimeOffset.UtcNow)
            {
                ErrorMessage = "This access code has expired.";
                return Page();
            }

            // Code is valid — return the applicant's data
            FoundApplicant = accessCode.Applicant;
            return Page();
        }
    }
}
