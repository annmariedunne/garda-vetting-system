using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.Applicants
{
    public class DetailsModel : PageModel
    {
        private readonly GardaVettingSystem.Data.GardaVettingSystemDbContext _context;

        public DetailsModel(GardaVettingSystem.Data.GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        public Applicant Applicant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FirstOrDefaultAsync(m => m.ApplicantNumber == id);

            if (applicant is not null)
            {
                Applicant = applicant;

                return Page();
            }

            return NotFound();
        }
    }
}
