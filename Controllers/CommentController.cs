using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Services;
using reddit_clone.Data;
using reddit_clone_backend.Dtos.Comment;
using reddit_clone_backend.Models;

namespace reddit_clone_backend.Controllers
{
    [ApiController]
    [DisableCors]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentController(ILogger<CommentController> logger, DataContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCommentDto>>> AddComment(AddCommentDto request) {
            var newComment = _mapper.Map<Comment>(request);
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            var comments = await _context.Comments
                .Include(c => c.ApplicationUser)
                .Include(c => c.ChildComments)
                .Select(c => new GetCommentDto(c))
                .ToListAsync();
            return Ok(comments);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCommentDto>>> GetAll () {
            var comments = await _context.Comments
                .Include(c => c.ApplicationUser)
                .Include(c => c.ChildComments)
                .Select(c => new GetCommentDto(c))
                .ToListAsync();
            return Ok(comments);
        }




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