using DotNetCoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMVC.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<FollowUser> Users { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
