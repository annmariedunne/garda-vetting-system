using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.ApplicantAddresses
{
    public class IndexModel : PageModel
    {
        private readonly GardaVettingSystem.Data.GardaVettingSystemDbContext _context;

        public IndexModel(GardaVettingSystem.Data.GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        public IList<ApplicantAddress> ApplicantAddress { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ApplicantAddress = await _context.ApplicantAddresses
                .Include(a => a.Applicant).ToListAsync();
        }
    }
}
