using DotNetCoreMVC.Data;
using DotNetCoreMVC.Models;
using DotNetCoreMVC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DotNetCoreMVC.Tests
{
    public class MessagesShould
    {
 

        [Fact]
        public void GetMyOwnMessages()
        {

            var data = new List<Message>
            {
                new Message {Id = 1, UserName = "user 1", Text = "Message from user 1", TimeStamp = DateTime.Now},
                new Message {Id = 2, UserName = "user 2", Text = "Message from user 2", TimeStamp = DateTime.Now},
                new Message {Id = 3, UserName = "user 3", Text = "Message from user 3", TimeStamp = DateTime.Now}
            }.AsQueryable();

            //Using empty constructor for mocking purpose
            var usersDbContextMock = new Mock<UsersDbContext>();
            //Mock the DbSet to interact with
            var mockSetOfMessages = new Mock<DbSet<Message>>();
            //Mock all methods we are using
            mockSetOfMessages.As<IQueryable<FollowUser>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetOfMessages.As<IQueryable<FollowUser>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSetOfUsers.As<IQueryable<FollowUser>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSetOfUsers.As<IQueryable<FollowUser>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //mockSetOfUsers.Setup(m => m.FirstOrDefault(It.IsAny)).
            //Specify when to return it
            usersDbContextMock.Setup(m => m.Messages).Returns(mockSetOfMessages.Object);

            //Using empty constructor for mocking purpose
            var applicationDbContextMock = new Mock<ApplicationDbContext>();
            //Mock the logger
            var logger = new Mock<ILogger<Messages>>();
            Messages messages = new Messages(
                usersDbContextMock.Object,
                logger.Object);

            //Assert that it was successful
            Assert.True(messages.GetMyOwnMessages("user 1").Count() == 1);

            //Verify that we have saved the change
            usersDbContextMock.Verify(m => m.Messages, Times.Once());
        }

        [Fact]
        public void SaveMessage()
        {
            var message = new Message { Id = 1, UserName = "user 1", Text = "Message from user 1", TimeStamp = DateTime.Now };

            //Using empty constructor for mocking purpose
            var usersDbContextMock = new Mock<UsersDbContext>();
            //Mock the DbSet to interact with
            var mockSetOfmessages = new Mock<DbSet<Message>>();
            //Specify when to return it
            usersDbContextMock.Setup(m => m.Messages).Returns(mockSetOfmessages.Object);
            //Using empty constructor for mocking purpose
            var applicationDbContextMock = new Mock<ApplicationDbContext>();
            //Mock the logger
            var logger = new Mock<ILogger<Messages>>();
            Messages messages = new Messages(
                usersDbContextMock.Object,
                logger.Object);

            //Assert that it was successful
            Assert.True(messages.SaveMessage(message));
            //Verify that we have added the message
            mockSetOfmessages.Verify(m => m.Add(It.IsAny<Message>()), Times.Once());
            //Verify that we have saved the change
            usersDbContextMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
