using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GardaVettingSystem.Data;
using GardaVettingSystem.Models;

namespace GardaVettingSystem.Pages.AccessCodes
{
    /// <summary>
    /// Handles validation of an access code entered by an organisation.
    /// Allows an organisation to verify a code and view the associated applicant's vetting data.
    /// </summary>
    /// <remarks>
    /// This page is not yet fully implemented — currently retains the scaffolded Edit structure
    /// and will be reworked as part of the organisation-side validation feature.
    /// </remarks>
    public class ValidateModel : PageModel
    {
        private readonly GardaVettingSystemDbContext _context;

        /// <summary>
        /// Initialises a new instance of <see cref="ValidateModel"/> with the required services.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ValidateModel(GardaVettingSystemDbContext context)
        {
            _context = context;
        }

        /// <summary>The access code being validated, bound from the form post.</summary>
        [BindProperty]
        public AccessCode AccessCode { get; set; } = default!;

        /// <summary>
        /// Handles GET requests. Loads an access code by ID for validation.
        /// </summary>
        /// <param name="id">The AccessCodeId to load.</param>
        /// <returns>The Validate page, or NotFound if the access code does not exist.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accesscode =  await _context.AccessCodes.FirstOrDefaultAsync(m => m.AccessCodeId == id);
            if (accesscode == null)
            {
                return NotFound();
            }
            AccessCode = accesscode;
           ViewData["ApplicantNumber"] = new SelectList(_context.Applicants, "ApplicantNumber", "FirstName");
            return Page();
        }

        /// <summary>
        /// Handles POST requests. Placeholder — to be fully implemented as part of
        /// the organisation-side validation feature.
        /// </summary>
        /// <returns>A redirect to the Index page on success.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AccessCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessCodeExists(AccessCode.AccessCodeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AccessCodeExists(int id)
        {
            return _context.AccessCodes.Any(e => e.AccessCodeId == id);
        }
    }
}
