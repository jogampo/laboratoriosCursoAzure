using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportantCompanyApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string WriteMessage { get; private set; } = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost(string message)
        {
            GoWriteMessage(message);
        }

        public void GoWriteMessage(string message)
        {
            WriteMessage += "Message written: " + message + "!\n";
        }

        public void OnGetGoReadMessage()
        {
            WriteMessage += "No messages found";
        }

    }
}
