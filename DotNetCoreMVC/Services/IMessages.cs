using DotNetCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Services
{
    public interface IMessages
    {
        IEnumerable<Message> GetMyOwnMessages(string username);
        IEnumerable<Message> GetFollowingMessages(string username);
        bool SaveMessage(Message message);
    }
}
