using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVC.Models;
using DotNetCoreMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMVC.Pages
{
    public class FollowingMessagesModel : PageModel
    {
        private readonly ILogger<FollowingMessagesModel> _logger;
        private readonly IMessages _messages;

        [BindProperty]
        public IEnumerable<Message> Messages { get; set; }

        public FollowingMessagesModel(ILogger<FollowingMessagesModel> logger, IMessages messages)
        {
            _logger = logger;
            _messages = messages;
        }

        public IActionResult OnGet()
        {
            Messages = _messages.GetFollowingMessages(User.Identity.Name);
            return Page();
        }
    }
}