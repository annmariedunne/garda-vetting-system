using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GardaVettingSystem.Pages.ApplicantAddresses
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

        [BindProperty]
        public int ApplicantNumber { get; set; }

        [BindProperty]
        public ApplicantAddress ApplicantAddress { get; set; } = new();

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
            {
                return RedirectToPage("/Applicants/Create");
            }

            ApplicantNumber = applicant.ApplicantNumber;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
            {
                return RedirectToPage("/Applicants/Create");
            }

            ApplicantAddress.ApplicantNumber = applicant.ApplicantNumber;
            ModelState.Remove("ApplicantAddress.ApplicantNumber");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ApplicantAddresses.Add(ApplicantAddress);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Applicants/Details", new { id = applicant.ApplicantNumber });
        }
    }
}
