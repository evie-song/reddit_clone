using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Dtos.ApplicationUser
{
    public class GetUserInfoDto
    {
        public List<int> SavedPostIds { get; set; }
        public Dictionary<int, int> VotedPosts { get; set; }

        public Dictionary<int, int> VotedComments { get; set; }

        public GetUserInfoDto(Models.ApplicationUser user)
        {
            SavedPostIds = user.SavedPosts != null
            ? user.SavedPosts.Select(sp => sp.PostId).ToList()
            : new List<int>();

            VotedPosts = user.VoteRegistrations != null
            ? user.VoteRegistrations.ToDictionary(vr => vr.PostId, vr => (int)vr.VoteValue)
            : new Dictionary<int, int>();

            VotedComments = user.CommentVoteRegistrations != null
            ? user.CommentVoteRegistrations.ToDictionary(vr => vr.CommentId, vr => (int)vr.VoteValue)
            : new Dictionary<int, int>();
        }
    }
}