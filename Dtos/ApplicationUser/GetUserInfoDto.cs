using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Dtos.ApplicationUser
{
    public class GetUserInfoDto
    {
        public List<int> SavedPostIds { get; set; }
        public Dictionary<int, bool> VotedPosts { get; set; }

        public Dictionary<int, bool> VotedComments { get; set; }

        public GetUserInfoDto(Models.ApplicationUser user)
        {
            SavedPostIds = user.SavedPosts != null
            ? user.SavedPosts.Select(sp => sp.PostId).ToList()
            : new List<int>();

            VotedPosts = user.VoteRegistrations != null
            ? user.VoteRegistrations.ToDictionary(vr => vr.PostId, vr => (int)vr.VoteValue == 1? true : false)
            : new Dictionary<int, bool>();

            VotedComments = user.CommentVoteRegistrations != null
            ? user.CommentVoteRegistrations.ToDictionary(vr => vr.CommentId, vr => (int)vr.VoteValue == 1? true : false)
            : new Dictionary<int, bool>();
        }
    }
}