
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
    /// Handles the editing of the logged-in user's applicant profile.
    /// Ownership is verified server-side on both GET and POST —
    /// users can only edit their own profile.
    /// </summary>
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

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
        /// The applicant profile being edited, bound from the form post.
        /// </summary>
        [BindProperty]
        public Applicant Applicant { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads the applicant profile for editing,
        /// verifying it belongs to the logged-in user.
        /// </summary>
        /// <param name="id">The ApplicantNumber to edit.</param>
        /// <returns>The Edit page, or a redirect to Create if not found or unauthorised.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }

            string? userId = _userManager.GetUserId(User);
            Applicant? applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.ApplicantNumber == id && m.UserId == userId);

            if (applicant == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }
            Applicant = applicant;
            return Page();
        }

        /// <summary>
        /// Handles POST requests. Verifies ownership, sets UserId server-side,
        /// and saves the updated applicant profile.
        /// </summary>
        /// <returns>
        /// A redirect to Details on success, Create if ownership cannot be verified,
        /// or the form page if validation fails.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            string? userId = _userManager.GetUserId(User);
            Applicant.UserId = userId ?? string.Empty;

            // Verify the logged-in user owns this record
            Applicant? existing = await _context.Applicants
                .FirstOrDefaultAsync(a => a.ApplicantNumber == Applicant.ApplicantNumber && a.UserId == userId);

            if (existing == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ApplicantExistsAsync(Applicant.ApplicantNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = Applicant.ApplicantNumber });
        }

        /// <summary>
        /// Checks whether an applicant record exists for the given ID.
        /// Used during concurrency exception handling.
        /// </summary>
        /// <param name="id">The ApplicantNumber to check.</param>
        /// <returns>True if the record exists, false otherwise.</returns>
        private async Task<bool> ApplicantExistsAsync(int id)
        {
            return await _context.Applicants.AnyAsync(e => e.ApplicantNumber == id);
        }
    }
}
