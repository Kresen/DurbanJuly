using DurbanJuly.Common.Configuration;
using DurbanJuly.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DurbanJuly.Infrastructure.Data.Contexts
{
    public class DefaultDbContext : IdentityDbContext<User>
    {

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }
        public DbSet<EventDetailStatus> EventDetailStatus { get; set; }

        private readonly IOptions<DatabaseConfiguration> _config;

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
                 : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18, 7)");
            }
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid().ToString(), // primary key
                    UserName = "test@test.com",
                    NormalizedUserName = "test@test.com",
                    PasswordHash = hasher.HashPassword(null, "Test@123"),
                    EmailConfirmed = true
                });
            modelBuilder.Entity<EventDetailStatus>().HasData(
         new EventDetailStatus { Id = 1, Name = "Active" },
         new EventDetailStatus { Id = 2, Name = "Scratched" },
         new EventDetailStatus { Id = 3, Name = "Closed" }
     );
            modelBuilder.Entity<Tournament>().HasData(
               new Tournament
               {
                   Id = 1,
                   Name = "Vodacom Durban July"
               }); ;
        }
    }
}
//Events = new List<Event>
//                   {
//                       new Event
//                       {
//                           Id = 1,
//                           Name = "First Race",
//                           EventDateTime = DateTime.Now,
//                           EventEndDateTime = DateTime.Now.AddDays(1),
//                           AutoClose = true,
//                           Number = 1,
//                           TournamentId = 1,
//                           EventDetails = new List<EventDetail>
//                           {
                               
//                               new EventDetail
//                               {
//                                   Id = 1,
//                                   Name = "Lucky Number Sleven",
//                                   FinishingPosition = 1,
//                                   FirstTimer = true,
//                                   EventDetailStatusId = 1,
//                                   Number = 50,
//                                   Odd = 56000000999030,
                                   
                                   
//                               }
//                           }

//                       }
//                   }