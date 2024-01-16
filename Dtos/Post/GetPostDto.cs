using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reddit_clone.Models;
using static reddit_clone.Models.Community;

namespace reddit_clone.Dtos.Post
{
    public class GetPostDto
    {

        public int Id {get; private set;}
        public string? Title {get; set;}
        public string? Content {get; set;}
        public DateTime CreatedTime { get; private set; }
        public int? UpVote {get; set;}
        public int? DownVote {get; set;}
        public int? CommunityId {get; set;}
        public string? CommunityName {get; set;}
        public string? Username {get; set;}
        public bool IsSaved { get; set; } = false;
        
        public GetPostDto(Models.Post post, bool isSaved){
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedTime = post.CreatedTime;
            UpVote = post.UpVote;
            DownVote = post.DownVote;
            CommunityId = post.CommunityId;
            CommunityName = post.Community != null ? post.Community.Title : "not found";
            Username = post.User != null? post.User.UserName : "not found";
            IsSaved = isSaved;
        }

        public GetPostDto(Models.Post post){
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            CreatedTime = post.CreatedTime;
            UpVote = post.UpVote;
            DownVote = post.DownVote;
            CommunityId = post.CommunityId;
            CommunityName = post.Community != null ? post.Community.Title : "not found";
            Username = post.User != null? post.User.UserName : "not found";
        }

        public GetPostDto(){}
    }
}