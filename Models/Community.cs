using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone.Models
{
    public class Community
    {
        public int Id {get; private set;}
        public string? Title {get; set;}
        public DateTime CreatedTime { get; private set; }
        public List<Post>? Posts { get; set;}

        public Community() {
            CreatedTime = DateTime.Now;
        }
    }
}