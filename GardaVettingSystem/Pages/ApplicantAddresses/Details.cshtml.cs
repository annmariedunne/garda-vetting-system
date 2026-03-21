using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    public class DetailsModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        public DetailsModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        public ApplicantAddress ApplicantAddress { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantaddress = await _context.ApplicantAddresses.FirstOrDefaultAsync(m => m.AddressId == id);

            if (applicantaddress is not null)
            {
                ApplicantAddress = applicantaddress;

                return Page();
            }

            return NotFound();
        }
    }
}
