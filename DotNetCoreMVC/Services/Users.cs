using DotNetCoreMVC.Data;
using DotNetCoreMVC.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreMVC.Services
{
    public class Users : IUsers
    {
        private UsersDbContext _usersDbContext;
        private ApplicationDbContext _applicationDbContext;
        private ILogger<Users> _logger;

        public Users(UsersDbContext usersDbContext, 
            ApplicationDbContext applicationDbContext,
            ILogger<Users> logger)
        {
            _usersDbContext = usersDbContext;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        /**
        * Lets a user follow another user
        **/
        public bool FollowUser(string username, string usernameToFollow)
        {
            //TODO add error handling
            FollowUser followUser = new FollowUser
            {
                User = username,
                UserToFollow = usernameToFollow
            };
            _usersDbContext.Users.Add(followUser);
            _usersDbContext.SaveChanges();
            return true;
        }

        /**
        * Get all the users and status if the user is following them or not
        **/
        public IEnumerable<FollowUserStatus> GetFollowUser(string username)
        {
            //Fetch all users from database
            IEnumerable<FollowUser> usersFromDb =_usersDbContext.Users
                .Where(user => user.User == username);

            //Loop through all users and see if we are following that user
            List<FollowUserStatus> list = new List<FollowUserStatus>();
            foreach (var user in _applicationDbContext.Users.Where(user => user.UserName != username))
            {
                //TODO optimize this with a join instead
                list.Add(
                    new FollowUserStatus
                    {
                        User = username,
                        UserToFollow = user.UserName,
                        Following = usersFromDb.FirstOrDefault(u => u.UserToFollow == user.UserName) != null
                    }
                );
            }
            return list;
        }

        /**
        * Lets a user unfollow another user
        **/
        public bool UnfollowUser(string username, string usernameToFollow)
        {
            //TODO add error handling
            FollowUser followUser = _usersDbContext.Users.FirstOrDefault(
                user => user.User == username && user.UserToFollow == usernameToFollow);
            _usersDbContext.Users.Remove(followUser);
            _usersDbContext.SaveChanges();
            return true;
        }
    }
}
