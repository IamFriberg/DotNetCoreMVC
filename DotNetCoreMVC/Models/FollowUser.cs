using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Models
{
    public class FollowUser
    {
        public string User { get; set; }
        public string UserToFollow { get; set; }
        public bool Following { get; set; }

    }
}
