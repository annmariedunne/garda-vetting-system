using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    /// <summary>
    /// Displays the details of a single access code.
    /// <para>Not part of the main user flow — retained as a scaffolded page for potential future use.</para>
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
        /// The access code to display.
        /// </summary>
        public AccessCode AccessCode { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads a single access code by its ID.
        /// </summary>
        /// <param name="id">The AccessCodeId to display.</param>
        /// <returns>The Details page, or NotFound if the access code does not exist.</returns>
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
