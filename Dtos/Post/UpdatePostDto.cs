using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone.Dtos.Post
{
    public class UpdatePostDto
    {
        public int Id {get; set;}
        public string? Title {get; set;}
        public string? Content {get; set;}
        public DateTime CreatedTime { get; private set; }
        public int UpVote {get; set;}
        public int DownVote {get; set;}
        // public int? CommunityId {get; set;}
    }
}