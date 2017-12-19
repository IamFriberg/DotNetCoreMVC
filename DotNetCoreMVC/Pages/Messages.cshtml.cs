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
    public class MessagesModel : PageModel
    {
        private readonly ILogger<MessagesModel> _logger;
        private readonly IMessages _messages;
        [BindProperty]
        public IEnumerable<Message> Messages { get; set; }
        public MessagesModel(ILogger<MessagesModel> logger, IMessages messages)
        {
            _logger = logger;
            _messages = messages;
        }
        public IActionResult OnGet()
        {
            Messages = _messages.GetMyOwnMessages();
            return Page();
        }
    }
}