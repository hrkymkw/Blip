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
        public DbSet<MessagePacket> MessagePackets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().
                HasMany(u => u.SentMessagePackets).
                WithRequired(p => p.Sender).
                HasForeignKey(p => p.SenderID).
                WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().
                HasMany(u => u.ReceivedMessagePackets).
                WithRequired(p => p.Receiver).
                HasForeignKey(p => p.ReceiverID).
                WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>().
                HasMany(m => m.MessagePackets).
                WithRequired(p => p.Message).
                HasForeignKey(p => p.MessageID).
                WillCascadeOnDelete(false);
        }
    }
}