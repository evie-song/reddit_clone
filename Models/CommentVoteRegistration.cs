using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Models
{
    public class CommentVoteRegistration
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public VoteEnum VoteValue { get; set; }
    }
}