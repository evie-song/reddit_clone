using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reddit_clone.Models;
using reddit_clone_backend.Dtos.Comment;
using reddit_clone_backend.Models;
using static reddit_clone.Models.Community;

namespace reddit_clone.Dtos.Post
{
    public class GetPostDto
    {

        public int Id { get; private set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedTime { get; private set; }
        public int TotalVote {get; set; } = 0;
        public bool UpVoted { get; set; } = false;
        public bool DownVoted { get; set; } = false;
        public int? UpVote { get; set; }
        public int? DownVote { get; set; }
        public int? CommunityId { get; set; }
        public string? CommunityName { get; set; }
        public string? Username { get; set; }
        public bool IsSaved { get; set; } = false;
        public int CommentCount { get; set; } = 0;
        public List<GetCommentDto>? Comments { get; set; } = new List<GetCommentDto>();

        // public GetPostDto(Models.Post post, bool isSaved){
        //     Id = post.Id;
        //     Title = post.Title;
        //     Content = post.Content;
        //     CreatedTime = post.CreatedTime;
        //     UpVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.UpVote).Count();
        //     DownVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.DownVote).Count();
        //     CommunityId = post.CommunityId;
        //     CommunityName = post.Community != null ? post.Community.Title : "not found";
        //     Username = post.User != null? post.User.UserName : "not found";
        //     IsSaved = isSaved;
        //     CommentCount = post.Comments.Count();
        //     Comments = post.Comments != null ? post.Comments.Select(c => new GetCommentDto(c)).ToList() : new List<GetCommentDto>();
        // }

        public GetPostDto(Models.Post post, bool isSaved, bool hasUpVoted, bool hasDownVoted)
        {
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedTime = post.CreatedTime;
            UpVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.UpVote).Count();
            DownVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.DownVote).Count();
            CommunityId = post.CommunityId;
            CommunityName = post.Community != null ? post.Community.Title : "not found";
            Username = post.User != null ? post.User.UserName : "not found";
            IsSaved = isSaved;
            UpVoted = hasUpVoted;
            DownVoted = hasDownVoted;
            CommentCount = post.Comments.Count();
            Comments = post.Comments != null ? post.Comments.Where(c => c.BaseCommentId == null).Select(c => new GetCommentDto(c)).ToList() : new List<GetCommentDto>();
            TotalVote = post.TotalVoteCount;
        }


        public GetPostDto(Models.Post post, bool isSaved, string userId)
        {
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedTime = post.CreatedTime;
            UpVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.UpVote).Count();
            DownVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.DownVote).Count();
            CommunityId = post.CommunityId;
            CommunityName = post.Community != null ? post.Community.Title : "not found";
            Username = post.User != null ? post.User.UserName : "not found";
            IsSaved = isSaved;
            UpVoted = post.VoteRegistrations.Any(vr => vr.PostId == post.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.UpVote);
            DownVoted = post.VoteRegistrations.Any(vr => vr.PostId == post.Id && vr.ApplicationUserId == userId && vr.VoteValue == VoteEnum.DownVote);
            CommentCount = post.Comments.Count();
            Comments = post.Comments != null ? post.Comments.Where(c => c.BaseCommentId == null).Select(c => new GetCommentDto(c)).ToList() : new List<GetCommentDto>();
            TotalVote = post.TotalVoteCount;
        }

        public GetPostDto(Models.Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedTime = post.CreatedTime;
            // UpVote = post.UpVote;
            // DownVote = post.DownVote;
            CommunityId = post.CommunityId;
            CommunityName = post.Community != null ? post.Community.Title : "not found";
            Username = post.User != null ? post.User.UserName : "not found";
            CommentCount = post.Comments.Count();
            UpVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.UpVote).Count();
            DownVote = post.VoteRegistrations.Where(vr => vr.VoteValue == VoteEnum.DownVote).Count();
            Comments = post.Comments != null ? post.Comments.Where(c => c.BaseCommentId == null).Select(c => new GetCommentDto(c)).ToList() : new List<GetCommentDto>();
            TotalVote = post.TotalVoteCount;
        }

        public GetPostDto() { }
    }
}