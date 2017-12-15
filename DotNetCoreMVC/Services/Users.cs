using DotNetCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Services
{
    public class Users : IUsers
    {
        
        public IEnumerable<FollowUser> Get()
        {
            //TODO Return users from DB
            List<FollowUser> users = new List<FollowUser>
            {
                new FollowUser {User = "Me", UserToFollow = "User 1", Following = true},
                new FollowUser {User = "Me", UserToFollow = "User 2", Following = false},
                new FollowUser {User = "Me", UserToFollow = "User 3", Following = true},
                new FollowUser {User = "Me", UserToFollow = "User 4", Following = true}
            };
            return users;
        }
    }
}
