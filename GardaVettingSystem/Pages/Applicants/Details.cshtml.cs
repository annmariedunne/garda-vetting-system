
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GardaVettingSystem.Pages.Applicants
{
    /// <summary>
    /// Displays the logged-in user's applicant profile, including their address history
    /// and access codes. This is the central hub page of the application as documented
    /// in the system workflow.
    /// </summary>
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initialises a new instance of <see cref="DetailsModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userManager">The ASP.NET Identity user manager.</param>
        public DetailsModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>The applicant profile to display, including addresses and access codes.</summary>
        public Applicant Applicant { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads the applicant profile with address history and access codes,
        /// verifying the record belongs to the logged-in user.
        /// </summary>
        /// <param name="id">The ApplicantNumber to display.</param>
        /// <returns>
        /// The Details page on success, or a redirect to Create if the profile
        /// is not found or does not belong to the logged-in user.
        /// </returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);
            Applicant? applicant = await _context.Applicants
                .Include(a => a.ApplicantAddresses)
                .Include(a => a.AccessCodes)
                .FirstOrDefaultAsync(m => m.ApplicantNumber == id && m.UserId == userId);

            if (applicant is not null)
            {
                Applicant = applicant;

                return Page();
            }

            // Record not found or doesn't belong to this user - redirect to their own profile
            return RedirectToPage("/Applicants/Create");
        }
    }
}
