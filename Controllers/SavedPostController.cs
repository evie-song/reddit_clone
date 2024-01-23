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

        [HttpDelete]
        public async Task<ActionResult<List<SavedPost>>> DeleteSavedPost (AddSavedPostDto request) 
        {
            try {
                var savedPost = await _context.SavedPosts.FindAsync(request.ApplicationUserId, request.PostId);
                if (savedPost is null) 
                    throw new Exception("Saved post is not found");
                
                _context.SavedPosts.Remove(savedPost);

                await _context.SaveChangesAsync();

                var savedPosts = await _context.SavedPosts
                    .Where(sp => sp.ApplicationUserId == request.ApplicationUserId)
                    .ToListAsync();

                return Ok(savedPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred during the deletion.", error = ex.Message });
            }
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
            var savedPosts = await _context.SavedPosts
                .Where(sp => sp.ApplicationUserId == request.ApplicationUserId)
                .ToListAsync();
            return Ok(savedPosts);
        }

        

    }
}