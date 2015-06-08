using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Blip.Web.Models;

namespace Blip.Web.DAL
{
    public class BlipContext : DbContext
    {
        public BlipContext() : base("BlipContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany<Message>(u => u.SentMessages)
                .WithMany(m => m.Senders)
                .Map(um =>
                {
                    um.MapLeftKey("UserRefId");
                    um.MapRightKey("MessageRefId");
                    um.ToTable("UserMessageSent");
                }
                );

            modelBuilder.Entity<User>()
                .HasMany<Message>(u => u.ReceivedMessages)
                .WithMany(m => m.Receivers)
                .Map(um =>
                    {
                        um.MapLeftKey("UserRefId");
                        um.MapRightKey("MessageRefId");
                        um.ToTable("UserMessageReceived");
                    }          
                );

        }
    }
}