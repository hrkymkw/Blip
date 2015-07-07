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
            context.SaveChanges();
        }
    }
}