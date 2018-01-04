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
    public class UserShould
    {
        [Fact]
        public void FollowUser()
        {
            //Using empty constructor for mocking purpose
            var usersDbContextMock = new Mock<UsersDbContext>();
            //Mock the DbSet to interact with
            var mockSetOfUsers = new Mock<DbSet<FollowUser>>();
            //Specify when to return it
            usersDbContextMock.Setup(m => m.Users).Returns(mockSetOfUsers.Object);
            //Using empty constructor for mocking purpose
            var applicationDbContextMock = new Mock<ApplicationDbContext>();
            //Mock the logger
            var logger = new Mock<ILogger<Users>>();
            Users users = new Users(
                usersDbContextMock.Object,
                applicationDbContextMock.Object,
                logger.Object);

            //Assert that it was successful
            Assert.True(users.FollowUser("MyUsername", "TheUserToFollow"));
            //Verify that we have added the user
            mockSetOfUsers.Verify(m => m.Add(It.IsAny<FollowUser>()), Times.Once());
            //Verify that we have saved the change
            usersDbContextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetFollowUser()
        {

            //Create a "result set" of users to follow
            var followingUserData = new List<FollowUser>
            {
                new FollowUser {Id = 1, User = "user 1", UserToFollow = "user 2"},
                new FollowUser {Id = 2, User = "user 1", UserToFollow = "user 3"}
            }.AsQueryable();

            //Using empty constructor for mocking purpose
            //Service that handles the following, messages etc.
            var usersDbContextMock = new Mock<UsersDbContext>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DotNetCoreMVC")
                .Options;
            //Service that handles the following, messages etc.
            var applicationDbContext = new ApplicationDbContext(options);

            //Create a "result set" of all users
            applicationDbContext.Users.Add(new ApplicationUser { UserName = "user 1" });
            applicationDbContext.Users.Add(new ApplicationUser { UserName = "user 2" });
            applicationDbContext.Users.Add(new ApplicationUser { UserName = "user 3" });
            applicationDbContext.Users.Add(new ApplicationUser { UserName = "user 4" });
            applicationDbContext.SaveChanges();

            //Mock the DbSet to interact with
            //Follow User 
            var mockSetOfFollowingUsers = new Mock<DbSet<FollowUser>>();

            //Mock all methods we are using
            //Follow User 
            mockSetOfFollowingUsers.As<IQueryable<FollowUser>>().Setup(m => m.Provider).Returns(followingUserData.Provider);
            mockSetOfFollowingUsers.As<IQueryable<FollowUser>>().Setup(m => m.Expression).Returns(followingUserData.Expression);

            //Specify when to return it
            usersDbContextMock.Setup(m => m.Users).Returns(mockSetOfFollowingUsers.Object);

            //Mock the logger
            var logger = new Mock<ILogger<Users>>();
            Users users = new Users(
                usersDbContextMock.Object,
                applicationDbContext,
                logger.Object);

            IEnumerable<FollowUserStatus> list = users.GetFollowUser("user 1");

            //Assert that it was successful
            Assert.Equal(3, list.Count());

            //Verify that we have added the user
            usersDbContextMock.Verify(m => m.Users, Times.Once());
        }

        [Fact]
        public void UnFollowUser()
        {

            var data = new List<FollowUser>
            {
                new FollowUser {Id = 1, User = "user 1", UserToFollow = "user 2"},
                new FollowUser {Id = 2, User = "user 1", UserToFollow = "user 3"},
                new FollowUser {Id = 3, User = "user 1", UserToFollow = "user 4"}
            }.AsQueryable();

            //Using empty constructor for mocking purpose
            var usersDbContextMock = new Mock<UsersDbContext>();
            //Mock the DbSet to interact with
            var mockSetOfUsers = new Mock<DbSet<FollowUser>>();
            //Mock all methods we are using
            mockSetOfUsers.As<IQueryable<FollowUser>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetOfUsers.As<IQueryable<FollowUser>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSetOfUsers.As<IQueryable<FollowUser>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSetOfUsers.As<IQueryable<FollowUser>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //mockSetOfUsers.Setup(m => m.FirstOrDefault(It.IsAny)).
            //Specify when to return it
            usersDbContextMock.Setup(m => m.Users).Returns(mockSetOfUsers.Object);

            //Using empty constructor for mocking purpose
            var applicationDbContextMock = new Mock<ApplicationDbContext>();
            //Mock the logger
            var logger = new Mock<ILogger<Users>>();
            Users users = new Users(
                usersDbContextMock.Object,
                applicationDbContextMock.Object,
                logger.Object);

            //Assert that it was successful
            Assert.True(users.UnfollowUser("user 1", "user 2"));

            //Verify that we have added the user
            mockSetOfUsers.Verify(m => m.Remove(It.IsAny<FollowUser>()), Times.Once());
            //Verify that we have saved the change
            usersDbContextMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
