using DotNetCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Services
{
    public interface IUsers
    {
        IEnumerable<FollowUser> Get();
    }
}
