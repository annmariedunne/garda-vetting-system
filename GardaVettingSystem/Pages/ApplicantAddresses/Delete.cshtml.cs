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
    /// Handles the deletion of an address belonging to the logged-in applicant.
    /// Ownership is verified server-side — users can only delete their own addresses.
    /// </summary>
    [Authorize]
    public class DeleteModel : PageModel
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
        /// Initialises a new instance of <see cref="DeleteModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userManager">The ASP.NET Identity user manager.</param>
        public DeleteModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The address to be deleted, bound from the form post.
        /// </summary>
        [BindProperty]
        public ApplicantAddress ApplicantAddress { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads the address for confirmation,
        /// verifying it belongs to the logged-in applicant.
        /// </summary>
        /// <param name="id">The AddressId to delete.</param>
        /// <returns>The Delete confirmation page, or a redirect if not found or unauthorised.</returns>
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
        /// Handles POST requests. Deletes the address after verifying ownership.
        /// </summary>
        /// <param name="id">The AddressId to delete.</param>
        /// <returns>A redirect to the applicant's Details page.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
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

            if (address != null)
            {
                _context.ApplicantAddresses.Remove(address);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
        }
    }
}
