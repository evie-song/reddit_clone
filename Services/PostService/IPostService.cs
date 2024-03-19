using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace reddit_clone.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDto>>> GetAllPost();
        Task<ServiceResponse<GetPostDto>> GetPostById(int id);
        Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost);
        Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id);
    }
}