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
        //private ApplicationUser _applicationUser;
        private ILogger<Users> _logger;

        public Users(UsersDbContext usersDbContext, 
            ApplicationDbContext applicationDbContext,
        //    ApplicationUser applicationUser,
            ILogger<Users> logger)
        {
            _usersDbContext = usersDbContext;
            _applicationDbContext = applicationDbContext;
         //   _applicationUser = applicationUser;
            _logger = logger;
        }

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

        public IEnumerable<FollowUserStatus> Get(string username)
        {
            //Fetch all users from database
            IEnumerable<FollowUser> usersFromDb =_usersDbContext.Users
                .Where(user => user.User == username);

            foreach (var item in usersFromDb)
            {
                _logger.LogInformation(item.UserToFollow);
            }

            //Loop through and see if we are following that user
            List<FollowUserStatus> list = new List<FollowUserStatus>();
            foreach (var user in _applicationDbContext.Users.Where(user => user.UserName != username))
            {
                _logger.LogInformation(user.UserName);
                //_logger.LogInformation(usersFromDb.FirstOrDefault(u => u.UserToFollow == user.UserName).UserToFollow);
                
                list.Add(
                    new FollowUserStatus
                    {
                        User = username,
                        UserToFollow = user.UserName,
                        Following = usersFromDb.FirstOrDefault(u => u.UserToFollow == user.UserName) != null //TODO fix
                    }
                );
            }
            return list;
        }

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
