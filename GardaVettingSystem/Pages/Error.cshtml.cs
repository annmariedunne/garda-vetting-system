using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GardaVettingSystem.Pages
{
    /// <summary>
    /// Page model for the Error page. Displays error details to the user.
    /// <para>Response caching is disabled to ensure error details are always fresh.</para>
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// Gets or sets the current request ID for error tracing.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Returns true if a request ID is available to display.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Handles GET requests.
        /// <para>Captures the current request ID for display in the error page.</para>
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
