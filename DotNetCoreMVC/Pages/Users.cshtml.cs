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
        public IEnumerable<FollowUser> Users { get; set; }
        //public IEnumerable<FollowUser> Users;

        public UsersModel(ILogger<UsersModel> logger, IUsers users)
        {
            _logger = logger;
            _users = users;
        }
        public IActionResult OnGet()
        {
            Users = _users.Get();
            return Page();
        }
    }
}