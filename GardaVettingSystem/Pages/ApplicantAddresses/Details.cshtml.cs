using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    /// <summary>
    /// Displays the details of a single applicant address.
    /// Not part of the main user flow — retained as a scaffolded page for potential future use.
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        /// <summary>
        /// Initialises a new instance of <see cref="DetailsModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DetailsModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The address to display.
        /// </summary>
        public ApplicantAddress ApplicantAddress { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads a single address by its ID.
        /// </summary>
        /// <param name="id">The AddressId to display.</param>
        /// <returns>The Details page, or NotFound if the address does not exist.</returns>
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
