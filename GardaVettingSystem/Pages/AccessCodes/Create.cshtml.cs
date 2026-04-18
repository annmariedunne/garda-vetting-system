using System.Security.Cryptography;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GardaVettingSystem.Pages.AccessCodes
{
    /// <summary>
    /// Handles the generation of a new access code for the logged-in applicant.
    /// The applicant provides an organisation name; all other fields are set server-side.
    /// </summary>
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Route constant for the Applicants Details page.
        /// </summary>
        private const string ApplicantsDetailsPage = "/Applicants/Details";

        /// <summary>
        /// Route constant for the Applicants Create page.
        /// </summary>
        private const string ApplicantsCreatePage = "/Applicants/Create";

        /// <summary>
        /// Initialises a new instance of <see cref="CreateModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userManager">The ASP.NET Identity user manager.</param>
        public CreateModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The access code being created, bound from the form post.
        /// </summary>
        [BindProperty]
        public AccessCode AccessCode { get; set; } = new();

        /// <summary>
        /// The applicant number of the logged-in user, used to populate the Cancel back link.
        /// </summary>
        public int ApplicantNumber { get; set; }

        /// <summary>
        /// Handles GET requests. Verifies the logged-in user has an applicant profile
        /// before displaying the Generate Access Code form.
        /// </summary>
        /// <returns>
        /// The Generate Access Code page, or a redirect to Create Profile if no profile exists.
        /// </returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .Include(a => a.ApplicantAddresses)
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            if (applicant.ApplicantAddresses.Count == 0)
            {
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
            }

            ApplicantNumber = applicant.ApplicantNumber;
            return Page();
        }

        /// <summary>
        /// Handles POST requests. Generates a secure access code and saves it
        /// against the logged-in applicant's profile with a 30-day expiry.
        /// </summary>
        /// <returns>
        /// A redirect to the applicant's Details page on success, or the form page
        /// if validation fails or no applicant profile exists.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .Include(a => a.ApplicantAddresses)
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            if (applicant.ApplicantAddresses.Count == 0)
            {
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
            }

            AccessCode.ApplicantNumber = applicant.ApplicantNumber;
            AccessCode.Code = GenerateAccessCode();
            AccessCode.CreatedDate = DateTimeOffset.UtcNow;
            AccessCode.ExpiryDate = DateTimeOffset.UtcNow.AddDays(30);
            AccessCode.IsActive = true;

            ModelState.Remove("AccessCode.Code");
            ModelState.Remove("AccessCode.ApplicantNumber");

            if (!ModelState.IsValid)
                return Page();

            _context.AccessCodes.Add(AccessCode);
            await _context.SaveChangesAsync();

            return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
        }

        /// <summary>
        /// Generates a cryptographically secure 12-character alphanumeric access code.
        /// Characters I and O are excluded to avoid visual ambiguity with digits 1 and 0,
        /// improving usability when codes are shared and entered manually.
        /// </summary>
        /// <returns>A 12-character uppercase alphanumeric string.</returns>
        private static string GenerateAccessCode()
        {
            // Excludes I and O to prevent confusion with digits 1 and 0
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            return new string(RandomNumberGenerator.GetItems(chars.AsSpan(), 12));
        }
    }
}
