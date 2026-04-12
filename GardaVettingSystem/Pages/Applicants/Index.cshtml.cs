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
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Applicant> Applicant { get;set; } = default!;

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
