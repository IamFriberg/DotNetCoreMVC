using DotNetCoreMVC.Data;
using DotNetCoreMVC.Services;
using System;
using Xunit;
using Moq;
using Castle.Core.Logging;

namespace DotNetCoreMVC.Tests
{
    public class UsersShould
    {
        [Fact]
        public void FollowUser()
        {
            Mock<UsersDbContext> usersDbContextMock = new Mock<UsersDbContext>();
            Mock<ApplicationDbContext> applicationDbContextMock = new Mock<ApplicationDbContext>();
            //Mock<ILogger<Users>> iLoggerMock = new Mock<ILogger<Users>>();
            Users users = new Users(usersDbContextMock.Object, applicationDbContextMock.Object, null);

            //Assert.True(users.FollowUser("MyUsername", "TheUserToFollow"));
        }
    }
}
