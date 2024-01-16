using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reddit_clone.Data;
using reddit_clone_backend.Dtos.SavedPost;
using reddit_clone_backend.Models;

namespace reddit_clone_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]

    public class SavedPostController : ControllerBase
    {
        private readonly DataContext _context;

        public SavedPostController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<SavedPost>>> AddSavedPost (AddSavedPostDto request)
        {
            var newSavedPost = new SavedPost {
                PostId = request.PostId,
                ApplicationUserId = request.ApplicationUserId,
            }; 
            _context.SavedPosts.Add(newSavedPost);
            await _context.SaveChangesAsync();
            return Ok(await _context.SavedPosts.ToListAsync());
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<GetPostDto>>> GetPerUser (string userId) {
            Console.WriteLine($"User ID: {userId}");

            var savedPosts = await _context.SavedPosts 
                .Where(sp => sp.ApplicationUserId == userId)
                .ToListAsync();
            Console.WriteLine($"Saved Post IDs: {string.Join(", ", savedPosts.Select(sp => sp.PostId))}");
            var postIds = savedPosts.Select(sp => sp.PostId).ToList();
            Console.WriteLine($"Post IDs: {string.Join(", ", postIds)}");
            var posts = await _context.Posts
                .Where(p => postIds.Contains(p.Id))
                .Select(p => new GetPostDto(p))
                .ToListAsync();
            return Ok(posts);
        }

    }
}