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
        public int? BaseCommentId { get; set; }
        public int TotalVoteCount { get; set; } 
        public ICollection<GetCommentDto>? ChildComments { get; set; }

        public GetCommentDto(Models.Comment comment) {
            Id = comment.Id;
            Content = comment.Content;
            CreatedAt = comment.CreatedAt;
            Username = comment.ApplicationUser.Username;
            PostId = comment.PostId;
            BaseCommentId = comment.BaseCommentId;
            TotalVoteCount = comment.TotalVoteCount;
            ChildComments = comment.ChildComments.Select(c => new GetCommentDto(c)).ToList();
            // ChildComments = comment.ChildComments != null ? comment.ChildComments.Select(c => new GetCommentDto(c)).ToList() : new List<GetCommentDto>();
        }
        public GetCommentDto() {

        }
    }
}