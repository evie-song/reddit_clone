using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reddit_clone_backend.Dtos.Comment;
using reddit_clone_backend.Models;

namespace reddit_clone
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<Post, GetPostDto>();
            CreateMap<AddPostDto, Post>();
            CreateMap<AddCommentDto, Comment>();
        }
    }
}