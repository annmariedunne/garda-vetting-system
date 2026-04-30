using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GardaVettingSystem.Pages.Applicants
{
    /// <summary>
    /// Handles the creation of a new applicant profile for the logged-in user.
    /// <para>Each user can only have one profile — if one already exists, the user is
    /// redirected to Edit instead.</para>
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

        /// <summary>
        /// The applicant profile being created, bound from the form post.
        /// </summary>
        [BindProperty]
        public Applicant Applicant { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Checks whether the logged-in user already has a profile.
        /// <para>If so, redirects to Edit to prevent duplicate profiles.</para>
        /// </summary>
        /// <returns>The Create page, or a redirect to Edit if a profile already exists.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            string? userId = _userManager.GetUserId(User);

            // Check if this user already has an Applicant profile
            Applicant? existing = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (existing != null)
            {
                // Redirect to their existing profile instead
                return RedirectToPage("./Edit", new { id = existing.ApplicantNumber });
            }

            return Page();
        }

        /// <summary>
        /// Handles POST requests. Sets UserId server-side and saves the new applicant profile.
        /// <para>If a profile already exists for this user, redirects to Edit instead.</para>
        /// </summary>
        /// <returns>
        /// A redirect to Details on success, Edit if profile already exists,
        /// or the form page if validation fails.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            string? userId = _userManager.GetUserId(User);
            Applicant.UserId = userId ?? string.Empty;

            Applicant? existing = await _context.Applicants
                .FirstOrDefaultAsync (a => a.UserId == userId);

            if (existing != null)
            {
                return RedirectToPage("./Edit", new { id = existing.ApplicantNumber });
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Applicants.Add(Applicant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Applicant.ApplicantNumber });
        }
    }
}
