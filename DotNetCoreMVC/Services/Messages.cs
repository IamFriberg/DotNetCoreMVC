using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVC.Models;

namespace DotNetCoreMVC.Services
{
    public class Messages : IMessages
    {
        public IEnumerable<Message> GetFollowingMessages()
        {
            List<Message> list = new List<Message>
            {
                new Message {UserName = "User 1", Text = "User 1 Text.", TimeStamp = DateTime.Now},
                new Message {UserName = "User 2", Text = "User 2 Text.", TimeStamp = DateTime.Now},
                new Message {UserName = "User 3", Text = "User 3 Text.", TimeStamp = DateTime.Now},
                new Message {UserName = "User 4", Text = "User 4 Text.", TimeStamp = DateTime.Now},
            };

            return list;
        }

        public IEnumerable<Message> GetMyOwnMessages()
        {
            List<Message> list = new List<Message>
            {
                new Message {UserName = "User 0", Text = "My first message", TimeStamp = DateTime.Now},
                new Message {UserName = "User 0", Text = "My first message", TimeStamp = DateTime.Now},
                new Message {UserName = "User 0", Text = "My first message", TimeStamp = DateTime.Now},
                new Message {UserName = "User 0", Text = "My first message", TimeStamp = DateTime.Now},
            };

            return list;
        }
    }
}
