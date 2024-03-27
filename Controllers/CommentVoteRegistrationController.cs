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

                    // if (request.VoteValue == VoteEnum.UpVote) {
                    //     var baseIds = FindAllBaseCommentIds(commentVoteRegistration.CommentId);
                    //     UpdateEngagementScore(baseIds, -1);
                    //     // reduce the engagement score by 1 for itself as well as all parent comments
                    // }
                } else {
                    commentVoteRegistration.VoteValue = request.VoteValue;
                    await _context.SaveChangesAsync();

                    // if (request.VoteValue == VoteEnum.UpVote) {

                    //     var baseIds = FindAllBaseCommentIds(commentVoteRegistration.CommentId);
                    //     UpdateEngagementScore(baseIds, 1);
                    //     // increase the engagement score by 1 for itself as well as all parent comments
                    // }
                }


            } else {
                var newVoteRegistration = new CommentVoteRegistration {
                    CommentId = request.CommentId,
                    ApplicationUserId = request.ApplicationUserId,
                    VoteValue = request.VoteValue
                }; 
                _context.CommentVoteRegistrations.Add(newVoteRegistration);
                await _context.SaveChangesAsync();

                // if (request.VoteValue == VoteEnum.UpVote) {
                //     // increase the engagement score by 1 for itself as well as all parent comments
                //     var baseIds = FindAllBaseCommentIds(newVoteRegistration.CommentId);
                //     UpdateEngagementScore(baseIds, 1);
                // }
            }

            var votes = await _context.CommentVoteRegistrations
                .Where(sp => sp.ApplicationUserId == request.ApplicationUserId)
                .ToListAsync();
            return Ok(votes); 
        }


        // private void UpdateEngagementScore(List<int> commentIds, int value)
        // {
        //     foreach (var commentId in commentIds)
        //     {   
        //         var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);

        //         comment.EngagementScore += value;
        //     }
        //     _context.SaveChanges();
        // }

        // private List<int> FindAllBaseCommentIds(int commentId)
        // {
        //     List<int> baseCommentIds = new List<int>();
        //     FindAllBaseCommentIdsRecursive(commentId, baseCommentIds);
        //     return baseCommentIds;
        // }

        // private void FindAllBaseCommentIdsRecursive(int commentId, List<int> baseCommentIds)
        // {
        //     // Find the comment by its ID
        //     Comment comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
        //     if (comment != null)
        //     {
        //         // If the comment has a base comment, add its ID to the list
        //         if (comment.BaseCommentId.HasValue)
        //         {
        //             baseCommentIds.Add(comment.BaseCommentId.Value);
        //             // Recursively search for the base comment's base comments
        //             FindAllBaseCommentIdsRecursive(comment.BaseCommentId.Value, baseCommentIds);
        //         }
        //     }
        // }
    }
}