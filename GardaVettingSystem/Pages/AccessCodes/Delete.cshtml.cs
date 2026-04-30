using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    /// <summary>
    /// Handles revocation of an access code for the logged-in applicant.
    /// <para>Revocation sets IsActive to false rather than deleting the record,
    /// preserving the audit trail in line with GDPR accountability principles.</para>
    /// </summary>
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string ApplicantsDetailsPage = "/Applicants/Details";
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
        /// The access code to be revoked, bound from the form post.
        /// </summary>
        [BindProperty]
        public AccessCode AccessCode { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads the access code for confirmation,
        /// verifying it belongs to the logged-in applicant.
        /// </summary>
        /// <param name="id">The AccessCodeId to revoke.</param>
        /// <returns>The Revoke confirmation page, or a redirect if not found or unauthorised.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return RedirectToPage(ApplicantsDetailsPage);

            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            var accessCode = await _context.AccessCodes
                .FirstOrDefaultAsync(a => a.AccessCodeId == id && a.ApplicantNumber == applicant.ApplicantNumber);

            if (accessCode == null)
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });

            AccessCode = accessCode;
            return Page();
        }

        /// <summary>
        /// Handles POST requests. Sets IsActive to false on the access code
        /// rather than deleting it, preserving the audit trail.
        /// </summary>
        /// <param name="id">The AccessCodeId to revoke.</param>
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

            var accessCode = await _context.AccessCodes
                .FirstOrDefaultAsync(a => a.AccessCodeId == id && a.ApplicantNumber == applicant.ApplicantNumber);

            if (accessCode != null)
            {
                // Revoke the code rather than deleting it — preserves audit trail
                accessCode.IsActive = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
        }
    }
}
