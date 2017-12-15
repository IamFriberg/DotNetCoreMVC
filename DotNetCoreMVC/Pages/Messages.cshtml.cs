using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMVC.Pages
{
    public class MessagesModel : PageModel
    {
        private readonly ILogger<MessagesModel> _logger;

        public MessagesModel(ILogger<MessagesModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}