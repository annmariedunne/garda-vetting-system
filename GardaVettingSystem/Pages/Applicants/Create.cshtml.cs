using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.Applicants
{
    public class CreateModel : PageModel
    {
        private readonly GardaVettingSystem.Data.GardaVettingSystemDbContext _context;

        public CreateModel(GardaVettingSystem.Data.GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Applicant Applicant { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Applicants.Add(Applicant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
