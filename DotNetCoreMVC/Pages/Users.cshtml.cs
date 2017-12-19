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
    public class UsersModel : PageModel
    {
        private readonly ILogger<UsersModel> _logger;
        private readonly IUsers _users;

        [BindProperty]
        public IEnumerable<FollowUserStatus> Users { get; set; }
        //public IEnumerable<FollowUser> Users;

        public UsersModel(ILogger<UsersModel> logger, IUsers users)
        {
            _logger = logger;
            _users = users;
        }
        public IActionResult OnGet()
        {
            _logger.LogInformation(User.Identity.Name);
            Users = _users.Get(User.Identity.Name);
            return Page();
        }

        public void OnPost(string userName)
        {
            _logger.LogInformation("OnPost");
            _users.FollowUser(User.Identity.Name, userName);
        }

        public void OnDelete(string userName)
        {
            _logger.LogInformation("OnDelete");
            _users.UnfollowUser(User.Identity.Name, userName);
        }

    }
}