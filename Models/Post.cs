using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace reddit_clone.Models
{
    public class Post
    {
        public int Id {get; private set;}
        public string? Title {get; set;}
        public string? Content {get; set;}
        public DateTime CreatedTime { get; private set; }
        public int UpVote {get; set;} = 0; 
        public int DownVote {get; set;} = 0; 
        public int? CommunityId {get; set;}
        public Community? Community {get; set;}

        public Post() {
            CreatedTime = DateTime.Now;
        }

    }
}