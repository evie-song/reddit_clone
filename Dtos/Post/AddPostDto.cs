using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone.Dtos.Post
{
    public class AddPostDto
    {
        public string? Title {get; set;}
        public string? Content {get; set;}
        public int? UpVote {get; set;} = 0;
        public int? DownVote {get; set;} = 0;
        public int? CommunityId {get; set;}
        public string? UserId {get; set;}
        public string? ApplicationUserId { get; set;}

    }
}