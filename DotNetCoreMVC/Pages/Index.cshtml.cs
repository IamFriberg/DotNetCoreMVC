using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCoreMVC.Pages
{
    public class IndexModel : PageModel
    {
        private SignInManager<ApplicationUser> _SignInManager;
        private UserManager<ApplicationUser> _UserManager;

        [BindProperty]
        public String Username { get; set; }
        public IndexModel(SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager)
        {
            _SignInManager = SignInManager;
            _UserManager = UserManager;
        }
        public void OnGet()
        {
            if (_SignInManager.IsSignedIn(User))
            {
                Username = _UserManager.GetUserName(User);
            }
        }
    }
}
