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
    /// Routes the logged-in user directly to their profile Details page,
    /// or to Create if no profile exists. Not used as a list page.
    /// </summary>
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initialises a new instance of <see cref="IndexModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userManager">The ASP.NET Identity user manager.</param>
        public IndexModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Not used — Index does not render a list view.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for Razor Pages model binding in scaffolded Index page.")]
        public IList<Applicant> Applicant { get;set; } = default!;

        /// <summary>
        /// Handles GET requests. Redirects the logged-in user to their profile Details page,
        /// or to Create if no profile exists.
        /// </summary>
        /// <returns>A redirect to Details or Create.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            string? userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage("/Applicants/Create");

            return RedirectToPage("/Applicants/Details", new { id = applicant.ApplicantNumber });
        }
    }
}
