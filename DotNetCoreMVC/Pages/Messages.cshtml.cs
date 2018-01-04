using System;
using System.Collections.Generic;
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
            Messages = _messages.GetMyOwnMessages(User.Identity.Name);
            return Page();
        }

        public void OnPost(string message)
        {
            //TODO Error handling, validation?
            Message myMessage = new Message { UserName = User.Identity.Name, Text = message, TimeStamp = DateTime.Now };
            _messages.SaveMessage(myMessage);
        }
    }
}