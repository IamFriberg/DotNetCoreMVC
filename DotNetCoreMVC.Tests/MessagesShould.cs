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
        public void GetFollowingMessages()
        {
            //Create a "result set" of messages
            var messagesData = new List<Message>
            {
                new Message {Id = 1, UserName = "user 1", Text = "Message from user 1", TimeStamp = DateTime.Now},
                new Message {Id = 2, UserName = "user 2", Text = "Message from user 2", TimeStamp = DateTime.Now},
                new Message {Id = 3, UserName = "user 2", Text = "Message from user 2", TimeStamp = DateTime.Now},
                new Message {Id = 4, UserName = "user 3", Text = "Message from user 3", TimeStamp = DateTime.Now}
            }.AsQueryable();

            //Create a "result set" of users
            var followingUserData = new List<FollowUser>
            {
                new FollowUser {Id = 1, User = "user 1", UserToFollow = "user 2"},
                new FollowUser {Id = 2, User = "user 2", UserToFollow = "user 3"},
                new FollowUser {Id = 3, User = "user 3", UserToFollow = "user 4"},
                new FollowUser {Id = 4, User = "user 4", UserToFollow = "user 1"},
            }.AsQueryable();

            //Using empty constructor for mocking purpose
            var usersDbContextMock = new Mock<UsersDbContext>();

            //Mock the DbSet to interact with
            var mockSetOfMessages = new Mock<DbSet<Message>>();
            var mockSetOfFollowUser = new Mock<DbSet<FollowUser>>();

            //Mock all methods we are using
            //First follow users
            mockSetOfFollowUser.As<IQueryable<FollowUser>>().Setup(m => m.Provider).Returns(followingUserData.Provider);
            mockSetOfFollowUser.As<IQueryable<FollowUser>>().Setup(m => m.Expression).Returns(followingUserData.Expression);
            //Second messages
            mockSetOfMessages.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(messagesData.Provider);
            mockSetOfMessages.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(messagesData.Expression);

            //Specify when to return it
            usersDbContextMock.Setup(m => m.Messages).Returns(mockSetOfMessages.Object);
            usersDbContextMock.Setup(m => m.Users).Returns(mockSetOfFollowUser.Object);

            //Using empty constructor for mocking purpose
            var applicationDbContextMock = new Mock<ApplicationDbContext>();

            //Mock the logger
            var logger = new Mock<ILogger<Messages>>();
            Messages messages = new Messages(
                usersDbContextMock.Object,
                logger.Object);

            //Assert that it was successful
            Assert.True(messages.GetFollowingMessages("user 1").Count() == 2);

            //Verify that we have saved the change
            usersDbContextMock.Verify(m => m.Messages, Times.Once());
            usersDbContextMock.Verify(m => m.Users, Times.Once());
        }


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
