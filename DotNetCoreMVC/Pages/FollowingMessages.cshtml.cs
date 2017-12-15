using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMVC.Pages
{
    public class FollowingMessagesModel : PageModel
    {
        private readonly ILogger<FollowingMessagesModel> _logger;

        public FollowingMessagesModel(ILogger<FollowingMessagesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
           
        }
    }
}