
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
        private readonly GardaVettingSystem.Data.GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(GardaVettingSystem.Data.GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
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
                return NotFound();
            }

            var applicant =  await _context.Applicants.FirstOrDefaultAsync(m => m.ApplicantNumber == id);
            if (applicant == null)
            {
                return NotFound();
            }
            Applicant = applicant;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Applicant.UserId = _userManager.GetUserId(User) ?? string.Empty;
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
                if (!ApplicantExists(Applicant.ApplicantNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicants.Any(e => e.ApplicantNumber == id);
        }
    }
}
