using DotNetCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Services
{
    public interface IUsers
    {
        IEnumerable<FollowUserStatus> GetFollowUser(string username);
        bool FollowUser(string username, string usernameToFollow);
        bool UnfollowUser(string username, string usernameToFollow);
    }
}
