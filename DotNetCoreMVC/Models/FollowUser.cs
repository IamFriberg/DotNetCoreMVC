using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Models
{
    public class FollowUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string User { get; set; }
        [Required]
        [EmailAddress]
        public string UserToFollow { get; set; }
    }
}
