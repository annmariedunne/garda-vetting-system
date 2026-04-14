using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    /// <summary>
    /// Displays a list of all access codes. Not part of the main user flow —
    /// retained as a scaffolded page for potential future admin use.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        /// <summary>
        /// Initialises a new instance of <see cref="IndexModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        public IndexModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The list of all access codes.
        /// </summary>
        public IList<AccessCode> AccessCode { get;set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads all access codes including their associated applicant.
        /// </summary>
        public async Task OnGetAsync()
        {
            AccessCode = await _context.AccessCodes
                .Include(a => a.Applicant).ToListAsync();
        }
    }
}
