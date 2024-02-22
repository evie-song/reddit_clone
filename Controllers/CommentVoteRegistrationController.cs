using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using reddit_clone_backend.Dtos.VoteRegistration;
using reddit_clone_backend.Models;
using reddit_clone_backend.Dtos.CommentVoteRegistration;
using reddit_clone.Data;



namespace reddit_clone_backend.Controllers
{       
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]
    public class CommentVoteRegistrationController : Controller
    {
        private readonly DataContext _context;

        public CommentVoteRegistrationController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<CommentVoteRegistration>>> SaveOrUpdateCommentVote (AddCommentVoteDto request)
        {
            var commentVoteRegistration = await _context.CommentVoteRegistrations
                .SingleOrDefaultAsync(vr => vr.ApplicationUserId == request.ApplicationUserId && vr.CommentId == request.CommentId);

            if (commentVoteRegistration != null) {
                if (commentVoteRegistration.VoteValue == request.VoteValue){
                    _context.CommentVoteRegistrations.Remove(commentVoteRegistration);
                    await _context.SaveChangesAsync();
                } else {
                    commentVoteRegistration.VoteValue = request.VoteValue;
                    await _context.SaveChangesAsync();
                }
            } else {
                var newVoteRegistration = new CommentVoteRegistration {
                    CommentId = request.CommentId,
                    ApplicationUserId = request.ApplicationUserId,
                    VoteValue = request.VoteValue
                }; 
                _context.CommentVoteRegistrations.Add(newVoteRegistration);
                await _context.SaveChangesAsync();
            }

            var votes = await _context.CommentVoteRegistrations
                .Where(sp => sp.ApplicationUserId == request.ApplicationUserId)
                .ToListAsync();
            return Ok(votes); 

        }
    }
}