
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
    public class EditModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string ApplicantsCreatePage = "/Applicants/Create";

        public EditModel(GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
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

            if (applicant == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }
            Applicant = applicant;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            string? userId = _userManager.GetUserId(User);
            Applicant.UserId = userId ?? string.Empty;

            // Verify the logged-in user owns this record
            Applicant? existing = await _context.Applicants
                .FirstOrDefaultAsync(a => a.ApplicantNumber == Applicant.ApplicantNumber && a.UserId == userId);

            if (existing == null)
            {
                return RedirectToPage(ApplicantsCreatePage);
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ApplicantExistsAsync(Applicant.ApplicantNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = Applicant.ApplicantNumber });
        }

        private async Task<bool> ApplicantExistsAsync(int id)
        {
            return await _context.Applicants.AnyAsync(e => e.ApplicantNumber == id);
        }
    }
}
