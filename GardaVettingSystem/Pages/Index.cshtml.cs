using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages
{
    /// <summary>
    /// Page model for the application root. Acts as a smart redirect entry point —
    /// authenticated users are routed to their profile or to Create if no profile exists.
    /// Unauthenticated users are shown the landing page.
    /// </summary>
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
        /// Handles GET requests. Redirects authenticated users to their profile page,
        /// or to Create if no profile exists. Unauthenticated users see the landing page.
        /// </summary>
        /// <returns>
        /// A redirect to Applicants/Index for authenticated users, or the landing page
        /// for unauthenticated users.
        /// </returns>
        public async Task<IActionResult> OnGet()
        {
            // Only redirect if the user is logged in
            if (User.Identity?.IsAuthenticated == true)
            {
                string? userId = _userManager.GetUserId(User);

                Applicant? existing = await _context.Applicants
                    .FirstOrDefaultAsync(a => a.UserId == userId);

                return existing == null
                    ? RedirectToPage("/Applicants/Create")
                    : RedirectToPage("/Applicants/Index", new { id = existing.ApplicantNumber });
            }

            return Page();
        }
    }
}
