using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    /// <summary>
    /// Displays a list of all applicant addresses. Not part of the main user flow —
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

        /// <summary>The list of all applicant addresses.</summary>
        public IList<ApplicantAddress> ApplicantAddress { get;set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads all addresses including their associated applicant.
        /// </summary>
        public async Task OnGetAsync()
        {
            ApplicantAddress = await _context.ApplicantAddresses
                .Include(a => a.Applicant).ToListAsync();
        }
    }
}
