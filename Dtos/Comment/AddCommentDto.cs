using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Dtos.Comment
{
    public class AddCommentDto
    {
        public string Content {get; set;}
        public string ApplicationUserId { get; set;}
        public int PostId { get; set; }
        public int? BaseCommentId { get; set; } = null;
    }
}