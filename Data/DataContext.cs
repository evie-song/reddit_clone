using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using reddit_clone_backend.Models;


namespace reddit_clone.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Community> Communities => Set<Community>();
        public DbSet<SavedPost> SavedPosts => Set<SavedPost>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<SavedPost>()
                .HasKey(sp => new { sp.ApplicationUserId, sp.PostId });

            modelBuilder.Entity<SavedPost>()
                .HasOne(sp => sp.ApplicationUser)
                .WithMany(au => au.SavedPosts)
                .HasForeignKey(sp => sp.ApplicationUserId);

            modelBuilder.Entity<SavedPost>()
                .HasOne(sp => sp.Post)
                .WithMany(p => p.SavedPosts)
                .HasForeignKey(sp => sp.PostId);



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
}