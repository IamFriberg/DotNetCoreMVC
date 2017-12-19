using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Models
{
    [NotMapped]
    public class FollowUserStatus : FollowUser
    {
        public bool Following { get; set; }
    }
}
