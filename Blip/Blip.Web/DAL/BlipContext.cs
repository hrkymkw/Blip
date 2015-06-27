using Blip.Web.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

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
                .WithRequired(m => m.Sender)
                .HasForeignKey(m => m.SenderID)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation( new IndexAttribute("UX_UserName") { IsUnique = true })
                );

        }
    }
}