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
    public class EditModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string ApplicantsDetailsPage = "/Applicants/Details";
        private const string ApplicantsCreatePage = "/Applicants/Create";

        public EditModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
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

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant == null)
                return RedirectToPage(ApplicantsCreatePage);

            // Verify ownership
            var existing = await _context.ApplicantAddresses
                .FirstOrDefaultAsync(a => a.AddressId == ApplicantAddress.AddressId && a.ApplicantNumber == applicant.ApplicantNumber);

            if (existing == null)
                return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });

            ModelState.Remove("ApplicantAddress.ApplicantNumber");
            ApplicantAddress.ApplicantNumber = applicant.ApplicantNumber;

            if (!ModelState.IsValid)
                return Page();

            _context.Attach(ApplicantAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ApplicantAddresses.AnyAsync(e => e.AddressId == ApplicantAddress.AddressId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage(ApplicantsDetailsPage, new { id = applicant.ApplicantNumber });
        }
    }
}
