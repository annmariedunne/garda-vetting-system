using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GardaVettingSystem.Pages.Applicants
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
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

        [BindProperty]
        public Applicant Applicant { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
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

            return RedirectToPage("./Index");
        }
    }
}
