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
        public UsersDbContext() : base()
        {

        }

        public UsersDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public virtual DbSet<FollowUser> Users { get; set; }

        public virtual DbSet<Message> Messages { get; set; }
    }
}
