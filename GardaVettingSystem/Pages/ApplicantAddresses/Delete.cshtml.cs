using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string ApplicantsDetailsPage = "/Applicants/Details";
        private const string ApplicantsCreatePage = "/Applicants/Create";

        public DeleteModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicantAddress ApplicantAddress { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return RedirectToPage(ApplicantsDetailsPage);

            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            var address = await _context.ApplicantAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.ApplicantNumber == applicant.ApplicantNumber);

            if (address == null)
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });

            ApplicantAddress = address;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return RedirectToPage(ApplicantsDetailsPage);

            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            var address = await _context.ApplicantAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.ApplicantNumber == applicant.ApplicantNumber);

            if (address != null)
            {
                _context.ApplicantAddresses.Remove(address);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
        }
    }
}
