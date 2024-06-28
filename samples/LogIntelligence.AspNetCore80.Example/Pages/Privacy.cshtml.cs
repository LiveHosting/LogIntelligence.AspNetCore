using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogIntelligence.AspNetCore80.Example.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            int n1 = 1;
            int n2 = 2;


            EventId eventId = new EventId(1, "sdgsdg");
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogInformation(eventId, ex, "xxxxxxxxxx", this.Url );
                
                throw;
            }
            
        }
    }

}
