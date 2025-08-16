using System;
using  static Domain.Activity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Persistence
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public  DbSet<Domain.Activity> Activities { get; set; }
        public   DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public  DbSet<Photo> Photos { get; set; }

        public  DbSet<Comment> Comments { get; set; }

        public DbSet<UserFollowing> UserFollowings { get; set; }
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

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
            // for converting time to utc time zone as in time ago
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                }
            }

            builder.Entity<UserFollowing>(x =>
            {
                x.HasKey(k => new { k.FollowerId, k.FolloweeId });

                x.HasOne(o => o.Follower)
                .WithMany(f => f.Followings)
                .HasForeignKey(k => k.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(o => o.Followee)
                .WithMany(f => f.Followers)
                .HasForeignKey(k => k.FolloweeId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}