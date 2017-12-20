using System.Collections.Generic;
using System.Linq;
using DotNetCoreMVC.Data;
using DotNetCoreMVC.Models;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMVC.Services
{
    public class Messages : IMessages
    {
        private UsersDbContext _usersDbContext;
        private ILogger<Messages> _logger;

        public Messages(UsersDbContext usersDbContext, ILogger<Messages> logger)
        {
            _usersDbContext = usersDbContext;
            _logger = logger;
        }

        /**
         * Returns all the messages for the users a specific user is following
         **/ 
        public IEnumerable<Message> GetFollowingMessages(string username)
        {
            IEnumerable<Message> list = _usersDbContext.Messages
                .Join(_usersDbContext.Users.Where(user => user.User == username),//Join against the matching set of data
                messages => messages.UserName,//Join against the other users message
                followUser => followUser.UserToFollow,//Only join rows for the user we are following
                (message, followUser) => message) //We are only interested in the messages
                .OrderBy(message => message.TimeStamp);//Sort the data in decending order
            return list;
        }

        /**
        * Returns All the messages for a specific user 
        **/
        public IEnumerable<Message> GetMyOwnMessages(string username)
        {
            return _usersDbContext.Messages
                .Where(message => message.UserName == username)
                .OrderBy(message => message.TimeStamp);
        }

        /**
        * Saves a message
        **/
        public bool SaveMessage(Message message)
        {
            _usersDbContext.Messages.Add(message);
            _usersDbContext.SaveChanges();
            //Fix error handling
            return true;
        }
    }
}
