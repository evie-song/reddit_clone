using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace reddit_clone.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Community> Communities => Set<Community>();

    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Community>()
    //         .HasMany(a => a.Posts)
    //         .WithOne(b => b.Community)
    //         .HasForeignKey(b => b.CommunityId);

    //     modelBuilder.Entity<Post>()
    //         .HasOne(b => b.Community)
    //         .WithMany(a => a.Posts)
    //         .HasForeignKey(b => b.CommunityId);
    // }
    }
}