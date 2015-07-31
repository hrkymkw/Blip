namespace Blip.Web.Migrations
{
    using Blip.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blip.Web.DAL.BlipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Blip.Web.DAL.BlipContext context)
        {
            context.Users.AddOrUpdate(new User { UserID = 1, UserName = "admin", Password = "adminpassword", Role = "admin", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 2, UserName = "user1", Password = "user1password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 3, UserName = "user2", Password = "user2password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 4, UserName = "user3", Password = "user3password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 5, UserName = "user4", Password = "user4password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 6, UserName = "user5", Password = "user5password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 7, UserName = "user6", Password = "user6password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
            context.Users.AddOrUpdate(new User { UserID = 8, UserName = "user7", Password = "user7password", Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });

            context.SaveChanges();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 10; i < 1010; i++)
            {
                for (int j = 0; j < stringChars.Length; j++)
                {
                    stringChars[j] = chars[random.Next(chars.Length)];
                }

                var randomString = new String(stringChars);

                context.Users.AddOrUpdate(new User { UserID = i, UserName = randomString, Password = randomString, Role = "user", Active = true, ActiveDate = DateTime.Parse("2015-01-01") });
                int k = random.Next(8);
                int l = random.Next(8);
                context.Messages.AddOrUpdate(new Message { Title = "Message " + i, DateTime = DateTime.Parse("2015-07-01"), Body = "Message sent by " + randomString, SenderID = i, Receivers = { context.Users.Where(u => u.UserID == k).ToList().SingleOrDefault(), context.Users.Where(u => u.UserID == l).ToList().SingleOrDefault() } });
            }
            context.SaveChanges();
        }
    }
}