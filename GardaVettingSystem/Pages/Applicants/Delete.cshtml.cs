using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GardaVettingSystem.Pages.Applicants
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string ApplicantsCreatePage = "/Applicants/Create";

        public DeleteModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Applicant Applicant { get; set; } = default!;

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
