using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Dtos.Comment
{
    public class GetCommentDto
    {
        public int Id { get; private set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; private set; } 
        public string Username {get; set;}
        public int PostId { get; set; }

        public GetCommentDto(Models.Comment comment) {
            Id = comment.Id;
            Content = comment.Content;
            CreatedAt = comment.CreatedAt;
            Username = comment.ApplicationUserId;
            PostId = comment.PostId;
        }
        public GetCommentDto() {

        }
    }
}