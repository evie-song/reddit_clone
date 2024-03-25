using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using reddit_clone.Data;
using Microsoft.AspNetCore.Identity;
using reddit_clone_backend.Models;


namespace reddit_clone.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PostService(IMapper mapper, DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<GetPostDto>> AddPost(AddPostDto newPost)
        {
            var servicesResponse = new ServiceResponse<GetPostDto>();
            var post = _mapper.Map<Post>(newPost);
            var community = await _context.Communities.FindAsync(newPost.CommunityId);
            var user = await _userManager.FindByIdAsync(newPost.UserId);
            post.User = user;
            post.Community = community;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            var id = post.Id;

            var dbPost = await GetSinglePostsWithRelations(id);

            servicesResponse.Data = new GetPostDto(dbPost);
            return servicesResponse;
        }


        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            var servicesResponse = new ServiceResponse<List<GetPostDto>>();

            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post is null)
                    throw new Exception($"Charactor with Id `{id}` not found");

                _context.Posts.Remove(post);

                await _context.SaveChangesAsync();

                servicesResponse.Data = _context.Posts.Select(p => _mapper.Map<GetPostDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message;
            }
            return servicesResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPost()
        {
            var servicesResponse = new ServiceResponse<List<GetPostDto>>();
            var posts = await GetAllPostsWithRelations();

            // servicesResponse.Data = posts.Select(p => new GetPostDto(p)).OrderBy(post => post.Id).ToList();

            servicesResponse.Data = posts
                .Select(p => new
                {
                    Post = p,
                    TotalVote = p.VoteRegistrations.Sum(v => (int)v.VoteValue)
                })
                .OrderByDescending(x => x.TotalVote)
                .Select(x => new GetPostDto(x.Post))
                .ToList();
            return servicesResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            var servicesResponse = new ServiceResponse<GetPostDto>();
            var dbPost = await GetSinglePostsWithRelations(id);
            servicesResponse.Data = new GetPostDto(
                dbPost
            );
            return servicesResponse;
        }

        private async Task<List<Post>> GetAllPostsWithRelations()
        {
            return await _context.Posts
                .Include(p => p.Community)
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.CommentVoteRegistrations)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.ChildComments)
                        .ThenInclude(cc => cc.CommentVoteRegistrations)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.VoteRegistrations)
                .ToListAsync();
        }


        private async Task<Post?> GetSinglePostsWithRelations(int postId)
        {
            return await _context.Posts
                .Include(p => p.Community)
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.CommentVoteRegistrations)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.ChildComments)
                        .ThenInclude(cc => cc.CommentVoteRegistrations)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.VoteRegistrations)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }
    }
}