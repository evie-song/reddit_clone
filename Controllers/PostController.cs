
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;


namespace reddit_clone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> Get() {
            return Ok(await _postService.GetAllPost());
        }

        [HttpGet("GetByUser/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> Get(string userId) {
            return Ok(await _postService.GetByUser(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPostDto>>> GetSingle(int id) {

            return Ok(await _postService.GetPostById(id));
        }

        [HttpGet("{id}/{userId}")]
        public async Task<ActionResult<ServiceResponse<GetPostDto>>> GetSingleByIdPerUser(int id, string userId) {

            return Ok(await _postService.GetPostByIdPerUser(id, userId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> AddPost(AddPostDto newPost)
        {
            return Ok(await _postService.AddPost(newPost));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPostDto>>> DeletePost(int id) {
            var response = await _postService.DeletePost(id);
            if (response.Data is null )
            {
                return NotFound(response);
            }
            return Ok(response);
        }


    }
}