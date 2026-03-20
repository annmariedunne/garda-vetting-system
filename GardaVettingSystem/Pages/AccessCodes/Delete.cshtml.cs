using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    public class DeleteModel : PageModel
    {
        private readonly GardaVettingSystem.Data.GardaVettingSystemDbContext _context;

        public DeleteModel(GardaVettingSystem.Data.GardaVettingSystemDbContext context)
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

            var accesscode = await _context.AccessCodes.FirstOrDefaultAsync(m => m.AccessCodeId == id);

            if (accesscode is not null)
            {
                AccessCode = accesscode;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accesscode = await _context.AccessCodes.FindAsync(id);
            if (accesscode != null)
            {
                AccessCode = accesscode;
                _context.AccessCodes.Remove(AccessCode);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
