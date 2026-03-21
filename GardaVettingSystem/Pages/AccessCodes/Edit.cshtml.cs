using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    public class EditModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        public EditModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccessCode AccessCode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accesscode =  await _context.AccessCodes.FirstOrDefaultAsync(m => m.AccessCodeId == id);
            if (accesscode == null)
            {
                return NotFound();
            }
            AccessCode = accesscode;
           ViewData["ApplicantNumber"] = new SelectList(_context.Applicants, "ApplicantNumber", "FirstName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AccessCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessCodeExists(AccessCode.AccessCodeId))
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

        private bool AccessCodeExists(int id)
        {
            return _context.AccessCodes.Any(e => e.AccessCodeId == id);
        }
    }
}
