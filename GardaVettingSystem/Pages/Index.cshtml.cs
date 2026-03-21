using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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
