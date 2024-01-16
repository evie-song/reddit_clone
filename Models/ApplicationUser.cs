using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace reddit_clone_backend.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Post>? Posts { get; set; }
        public List<SavedPost> SavedPosts { get; set; }
        
    }
}