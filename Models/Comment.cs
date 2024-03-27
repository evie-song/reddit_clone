using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; } 
        public Post Post { get; set; }
        public int? BaseCommentId { get; set; } 
        public Comment? BaseComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; }
        public string ApplicationUserId { get; set; }  
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public ICollection<CommentVoteRegistration> CommentVoteRegistrations { get; set; }
        public int EngagementScore { get; set; }

        public Comment() {
            ChildComments = new List<Comment>();
            CommentVoteRegistrations = new List<CommentVoteRegistration>();
            CreatedAt = DateTime.Now;
        }

        public int TotalVoteCount {
            get {
                int totalVote = 0;
                foreach (var commentVoteRegistration in CommentVoteRegistrations)
                {
                    totalVote += (int)commentVoteRegistration.VoteValue;
                }
                return totalVote;
            } 
        }


    }


}