using ChatApp.EntityConfiguration;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<User>(new UserConfig());
            modelBuilder.ApplyConfiguration<Message>(new MessageConfig());            

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

    }
}
