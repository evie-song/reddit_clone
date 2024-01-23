using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web.Http.Cors;
using reddit_clone.Data;
using reddit_clone_backend.Models;
using System.ComponentModel;

namespace reddit_clone_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]

    public class ApplicationUserController : Controller
    {
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly DataContext _context;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{userId}/saved")]
        public async Task<ActionResult<List<GetPostDto>>> GetSavedPosts (string userId) {
            var savedPosts = await _context.SavedPosts 
                .Where(sp => sp.ApplicationUserId == userId)
                .ToListAsync();

            var postIds = savedPosts.Select(sp => sp.PostId).ToList();

            var posts = await _context.Posts
                .Include(p => p.Community )
                .Include(p => p.User )
                .Where(p => postIds.Contains(p.Id))
                .Select(p => new GetPostDto(
                    p,
                    _context.SavedPosts.Any(sp => sp.PostId == p.Id && sp.ApplicationUserId == userId),
                    _context.VoteRegistrations.Count(vr => vr.PostId == p.Id && vr.VoteValue == VoteEnum.UpVote),
                    _context.VoteRegistrations.Count(vr => vr.PostId == p.Id && vr.VoteValue == VoteEnum.DownVote),
                    _context.VoteRegistrations.Any(vr => vr.PostId == p.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.UpVote),
                    _context.VoteRegistrations.Any(vr => vr.PostId == p.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.DownVote)
                ))
                .ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{userId}/upvoted")]
        public async Task<ActionResult<List<GetPostDto>>> GetUpvotedPosts (string userId) {

            var upvotedPosts = await _context.VoteRegistrations
                .Where(vr => vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.UpVote)
                .ToListAsync();

            var postIds = upvotedPosts.Select(sp => sp.PostId).ToList();

            var posts = await _context.Posts
                .Include(p => p.Community )
                .Include(p => p.User )
                .Where(p => postIds.Contains(p.Id))
                .Select(p => new GetPostDto(
                    p,
                    _context.SavedPosts.Any(sp => sp.PostId == p.Id && sp.ApplicationUserId == userId),
                    _context.VoteRegistrations.Count(vr => vr.PostId == p.Id && vr.VoteValue == VoteEnum.UpVote),
                    _context.VoteRegistrations.Count(vr => vr.PostId == p.Id && vr.VoteValue == VoteEnum.DownVote),
                    _context.VoteRegistrations.Any(vr => vr.PostId == p.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.UpVote),
                    _context.VoteRegistrations.Any(vr => vr.PostId == p.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.DownVote)
                ))
                .ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{userId}/downvoted")]
        public async Task<ActionResult<List<GetPostDto>>> GetDownvotedPosts (string userId) {

            var downvotedPosts = await _context.VoteRegistrations
                .Where(vr => vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.DownVote)
                .ToListAsync();

            var postIds = downvotedPosts.Select(sp => sp.PostId).ToList();

            var posts = await _context.Posts
                .Include(p => p.Community )
                .Include(p => p.User )
                .Where(p => postIds.Contains(p.Id))
                .Select(p => new GetPostDto(
                    p,
                    _context.SavedPosts.Any(sp => sp.PostId == p.Id && sp.ApplicationUserId == userId),
                    _context.VoteRegistrations.Count(vr => vr.PostId == p.Id && vr.VoteValue == VoteEnum.UpVote),
                    _context.VoteRegistrations.Count(vr => vr.PostId == p.Id && vr.VoteValue == VoteEnum.DownVote),
                    _context.VoteRegistrations.Any(vr => vr.PostId == p.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.UpVote),
                    _context.VoteRegistrations.Any(vr => vr.PostId == p.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.DownVote)
                ))
                .ToListAsync();
            return Ok(posts);
        }


        // [HttpGet("")]



        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}