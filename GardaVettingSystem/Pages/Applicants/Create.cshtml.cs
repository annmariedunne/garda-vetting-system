using GardaVettingSystem.Data;
using GardaVettingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GardaVettingSystem.Pages.Applicants
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly GardaVettingSystem.Data.GardaVettingSystemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(GardaVettingSystem.Data.GardaVettingSystemDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            Applicant.UserId = _userManager.GetUserId(User) ?? string.Empty;

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
