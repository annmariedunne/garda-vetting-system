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
    /// Handles the deletion of the logged-in user's applicant profile.
    /// Ownership is verified server-side — users can only delete their own profile.
    /// After deletion the user is redirected to the application root as they no longer have a profile.
    /// </summary>
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

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
        /// The applicant profile to be deleted, bound from the form post.
        /// </summary>
        [BindProperty]
        public Applicant Applicant { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads the applicant profile for confirmation,
        /// verifying it belongs to the logged-in user.
        /// </summary>
        /// <param name="id">The ApplicantNumber to delete.</param>
        /// <returns>The Delete confirmation page, or a redirect if not found or unauthorised.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }

            string? userId = _userManager.GetUserId(User);
            Applicant? applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.ApplicantNumber == id && m.UserId == userId);

            if (applicant is not null)
            {
                Applicant = applicant;

                return Page();
            }

            return RedirectToPage(ApplicantsCreatePage);
        }

        /// <summary>
        /// Handles POST requests. Deletes the applicant profile after verifying ownership.
        /// Redirects to the application root on success as the user no longer has a profile.
        /// </summary>
        /// <param name="id">The ApplicantNumber to delete.</param>
        /// <returns>A redirect to the application root on success.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }

            string? userId = _userManager.GetUserId(User);
            Applicant? applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.ApplicantNumber == id && m.UserId == userId);

            if (applicant != null)
            {
                Applicant = applicant;
                _context.Applicants.Remove(Applicant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
