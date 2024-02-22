using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Dtos.CommentVoteRegistration
{
    public class AddCommentVoteDto
    {
        public int CommentId { get; set; }
        public string ApplicationUserId { get; set; }
        public Models.VoteEnum VoteValue { get; set; }

    }
}