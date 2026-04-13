using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    /// <summary>
    /// Handles the creation of a new address for the logged-in applicant.
    /// ApplicantNumber is always set server-side from the logged-in user's identity —
    /// it is never accepted from the form to prevent cross-user data manipulation.
    /// </summary>
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

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

        /// <summary>The applicant number of the logged-in user, used to populate the Cancel back link.</summary>
        [BindProperty]
        public int ApplicantNumber { get; set; }

        /// <summary>The address being created, bound from the form post.</summary>
        [BindProperty]
        public ApplicantAddress ApplicantAddress { get; set; } = new();

        /// <summary>
        /// Handles GET requests. Verifies the logged-in user has an applicant profile
        /// before displaying the Add Address form.
        /// </summary>
        /// <returns>
        /// The Add Address page, or a redirect to Create Profile if no profile exists.
        /// </returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
            {
                return RedirectToPage("/Applicants/Create");
            }

            ApplicantNumber = applicant.ApplicantNumber;
            return Page();
        }

        /// <summary>
        /// Handles POST requests. Sets ApplicantNumber server-side and saves the new address.
        /// </summary>
        /// <returns>
        /// A redirect to the applicant's Details page on success, or the form page
        /// if validation fails or no applicant profile exists.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
            {
                return RedirectToPage("/Applicants/Create");
            }

            // Set ApplicantNumber server-side — never from the form
            ApplicantAddress.ApplicantNumber = applicant.ApplicantNumber;

            // Remove server-set field from ModelState to prevent false validation failures
            ModelState.Remove("ApplicantAddress.ApplicantNumber");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ApplicantAddresses.Add(ApplicantAddress);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Applicants/Details", new { id = applicant.ApplicantNumber });
        }
    }
}
