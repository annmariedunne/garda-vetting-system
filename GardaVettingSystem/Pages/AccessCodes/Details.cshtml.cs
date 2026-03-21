using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    public class DetailsModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        public DetailsModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

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
    }
}
