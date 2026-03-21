using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        public IndexModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        public IList<AccessCode> AccessCode { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AccessCode = await _context.AccessCodes
                .Include(a => a.Applicant).ToListAsync();
        }
    }
}
