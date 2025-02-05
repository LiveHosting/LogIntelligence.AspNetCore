using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogIntelligence.AspNetCore80.Example.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Navigated to Index Page");
        }

        public void OnPostGenerateTrace()
        {
            _logger.LogTrace("This is a trace message from the Index Page");
        }

        public void OnPostGenerateDebug()
        {
            _logger.LogDebug("This is a debug message from the Index Page");
        }

        public void OnPostGenerateInformation()
        {
            _logger.LogInformation("This is an information message from the Index Page");
        }

        public void OnPostGenerateWarning()
        {
            _logger.LogWarning("This is a warning message from the Index Page");
            RedirectToPage();
        }

        public void OnPostGenerateError()
        {
            _logger.LogError("This is an error message from the Index Page");
        }

        public void OnPostGenerateCritical()
        {
            _logger.LogCritical("This is an critical message from the Index Page");
        }

        public void OnPostGenerateNotImplementedException()
        {
            throw new NotImplementedException();
        }

        public void OnPostGenerateDivideByZeroException()
        {
            // Intentionally causing a DivideByZeroException for demonstration purposes
            var n1 = 10;
            var n2 = 0;
            var result = n1 / n2;
        }
    }
}
