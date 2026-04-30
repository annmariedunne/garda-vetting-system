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
    /// Acts as a smart redirect entry point for the Applicants section.
    /// <para>Routes the logged-in user directly to their profile Details page,
    /// or to Create if no profile exists.</para>
    /// <para>Also handles post-deletion confirmation and full account deletion
    /// including the Identity user record.</para>
    /// </summary>
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Initialises a new instance of <see cref="IndexModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userManager">The ASP.NET Identity user manager.</param>
        /// <param name="signInManager">The ASP.NET Identity sign-in manager.</param>
        public IndexModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Not used — Index does not render a list view.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for Razor Pages model binding in scaffolded Index page.")]
        public IList<Applicant> Applicant { get;set; } = default!;

        /// <summary>
        /// Handles GET requests.
        /// <para>Redirects the logged-in user to their profile Details page,
        /// or to Create if no profile exists.</para>
        /// <para>Renders the post-deletion confirmation page if the deleted parameter is true.</para>
        /// </summary>
        /// <param name="deleted">Indicates whether the user has just deleted their profile.</param>
        /// <returns>A redirect to Details, Create, or the deletion confirmation page.</returns>
        public async Task<IActionResult> OnGetAsync(bool deleted = false)
        {
            if (deleted)
            {
                return Page();
            }

            string? userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage("/Applicants/Create");

            return RedirectToPage("/Applicants/Details", new { id = applicant.ApplicantNumber });
        }

        /// <summary>
        /// Handles POST requests.
        /// <para>Deletes the Identity user account and signs the user out.</para>
        /// <para>This permanently removes the user's email and login credentials from the system.</para>
        /// </summary>
        /// <returns>A redirect to the application root after sign out.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
