using System;
using  static Domain.Activity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Persistence
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public  DbSet<Domain.Activity> Activities { get; set; }
        public  DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ActivityAttendee>(x => x.HasKey(a => new { a.ActivityId, a.UserId }));
            builder.Entity<ActivityAttendee>()
            .HasOne(x => x.User)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.UserId);

            builder.Entity<ActivityAttendee>()
            .HasOne(x => x.Activity)
            .WithMany(x => x.Attendees)
            .HasForeignKey(x => x.ActivityId);
        }
    }
}