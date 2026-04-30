using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    /// <summary>
    /// Handles the editing of an existing address belonging to the logged-in applicant.
    /// <para>Ownership is verified server-side on both GET and POST — users can only edit their own addresses.</para>
    /// <para>ApplicantNumber is always set server-side and never accepted from the form.</para>
    /// </summary>
    [Authorize]
    public class EditModel : PageModel
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
        /// Initialises a new instance of <see cref="EditModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userManager">The ASP.NET Identity user manager.</param>
        public EditModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The address being edited, bound from the form post.
        /// </summary>
        [BindProperty]
        public ApplicantAddress ApplicantAddress { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads the address for editing,
        /// verifying it belongs to the logged-in applicant.
        /// </summary>
        /// <param name="id">The AddressId to edit.</param>
        /// <returns>The Edit page, or a redirect if not found or unauthorised.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return RedirectToPage(ApplicantsDetailsPage);

            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            var address = await _context.ApplicantAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.ApplicantNumber == applicant.ApplicantNumber);

            if (address == null)
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });

            ApplicantAddress = address;
            return Page();
        }

        /// <summary>
        /// Handles POST requests.
        /// <para>Verifies ownership, sets ApplicantNumber server-side,
        /// and saves the updated address.</para>
        /// </summary>
        /// <returns>
        /// A redirect to the applicant's Details page on success, or the form page
        /// if validation fails or ownership cannot be verified.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            // Verify ownership
            var existing = await _context.ApplicantAddresses
                .FirstOrDefaultAsync(a => a.AddressId == ApplicantAddress.AddressId && a.ApplicantNumber == applicant.ApplicantNumber);

            if (existing == null)
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });

            ModelState.Remove("ApplicantAddress.ApplicantNumber");
            ApplicantAddress.ApplicantNumber = applicant.ApplicantNumber;

            if (!ModelState.IsValid)
                return Page();

            existing.AddressLine = ApplicantAddress.AddressLine;
            existing.Postcode = ApplicantAddress.Postcode;
            existing.Country = ApplicantAddress.Country;
            existing.ResidentFrom = ApplicantAddress.ResidentFrom;
            existing.ResidentTo = ApplicantAddress.ResidentTo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ApplicantAddresses.AnyAsync(e => e.AddressId == ApplicantAddress.AddressId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
        }
    }
}
