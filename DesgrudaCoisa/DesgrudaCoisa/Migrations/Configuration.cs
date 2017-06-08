namespace DesgrudaCoisa.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DesgrudaCoisa.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DesgrudaCoisa.Models.ApplicationDbContext context)
        {
            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(
            u => u.UserName,
            new ApplicationUser
            {
                UserName = "admin@s2b.br",
                PasswordHash = hasher.HashPassword("s2b"),
                SecurityStamp = Guid.NewGuid().ToString()
            },
              new ApplicationUser
              {
                  UserName = "hugo@s2b.br",
                  PasswordHash = hasher.HashPassword("s2b"),
                  SecurityStamp = Guid.NewGuid().ToString()
              },
              new ApplicationUser
              {
                  UserName = "luis@s2b.br",
                  PasswordHash = hasher.HashPassword("s2b"),
                  SecurityStamp = Guid.NewGuid().ToString()
              },
            new ApplicationUser
            {
                UserName = "ze@s2b.br",
               // FullName = "Administrador",
                PasswordHash = hasher.HashPassword("s2b"),
                SecurityStamp = Guid.NewGuid().ToString()
            });
        }
    }
}
