using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
// using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using reddit_clone.Data;
using reddit_clone_backend.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.UserSecrets;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Bogus;
using System.Runtime.InteropServices;


namespace reddit_clone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class DemoDataController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _context;
        public DemoDataController(
            DataContext context,
            UserManager<IdentityUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Create100DemoUsers")]

        public async Task<IActionResult> CreateUsers()

        {
            var createdUsers = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                string randomEmail = GenerateRandomEmail();

                var user_exist = await _userManager.FindByEmailAsync(randomEmail);

                if (user_exist == null)
                {
                    var new_user = new IdentityUser()
                    {
                        Email = randomEmail,
                        UserName = randomEmail
                    };

                    var is_created = await _userManager.CreateAsync(new_user, "Test@ccount123");

                    if (is_created.Succeeded)
                    {
                        var new_application_user = new ApplicationUser()
                        {
                            Id = new_user.Id,
                            Username = new_user.UserName
                        };
                        _context.ApplicationUsers.Add(new_application_user);
                        await _context.SaveChangesAsync();

                        createdUsers.Add(randomEmail);
                    }
                }
            }

            return Ok(new { CreatedUsers = createdUsers });
        }

        [HttpPost("AddCommunitiesFromFile")]
        public async Task<ActionResult<List<Community>>> AddCommunitiesFromFile()
        {
            var addedCommunities = new List<String>();
            try
            {
                var filePath = "community_titles.txt"; // Assuming the file is named community_titles.txt and located in the root directory
                var communityTitles = await System.IO.File.ReadAllLinesAsync(filePath);

                foreach (var title in communityTitles)
                {
                    var is_exist = _context.Communities.Any(c => c.Title == title);
                    if (!is_exist)
                    {

                        var newCommunity = new Community
                        {
                            Title = title.Trim(), // Trim any leading or trailing whitespace
                        };

                        _context.Communities.Add(newCommunity);

                        await _context.SaveChangesAsync();

                        addedCommunities.Add(title);

                    }

                }
                return Ok(new { NewCommunities = addedCommunities });
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, "Error occurred while adding communities from file.");
            }
        }

        public class RedditPost
        {
            public string Subreddit { get; set; }
            public string SelfText { get; set; }
            public string Title { get; set; }
            public string Selftext_html { get; set; }
            public string Permalink { get; set; }
        }


        [HttpPost("AddPostsFromFile")]
        public async Task<ActionResult> AddPostsFromFile()
        {
            var allUserIds = _context.ApplicationUsers.Select(au => au.Id).ToList();
            var random = new Random();

            try
            {
                var filePath = "listing_data.json";
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                var posts = JsonConvert.DeserializeObject<List<RedditPost>>(json);

                // var post = posts[0];

                var newPosts = new List<Post>();

                foreach (var post in posts) {
                    var community_name = post.Subreddit;
                    var community = await _context.Communities.FirstOrDefaultAsync(c => c.Title == community_name);
                    int communityId;
                    if (community == null) {
                        communityId = 106;
                    } else {
                        communityId = community.Id;
                    }

                    var randomIndex = random.Next(0, allUserIds.Count);
                    var userId = allUserIds[randomIndex];

                    var newPost = new Post() {
                        Title = post.Title,
                        Content = post.Selftext_html,
                        CommunityId = communityId,
                        UserId = userId,
                        ApplicationUserId = userId
                    };

                    newPosts.Add(newPost);
                    
                }
                _context.Posts.AddRange(newPosts);
                await _context.SaveChangesAsync();

                return Ok(new {message = newPosts});
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred while adding posts from file.");
            }
        }

        [HttpPost("AddPostVotes")]
        public async Task<ActionResult> AddPostVotes() {
            var allUserIds = _context.ApplicationUsers.Select(au => au.Id).ToList();
            var random = new Random();

            var allPosts = _context.Posts.Where(p => p.Id > 443).Select(p => p.Id).ToList();

            foreach (var postId in allPosts) {
                var shuffledUserIds = allUserIds.OrderBy(x => random.Next()).ToList();
                int numberOfVotes = random.Next(0, 81);
                var usersToVote = shuffledUserIds.Take(numberOfVotes).ToList();
                foreach (string user in usersToVote) {
                    int randomNumber = random.Next(0, 100);
                    int voteValue = randomNumber < 90? 1 : -1;
                    VoteEnum enumVoteValue = voteValue == 1 ? VoteEnum.UpVote : VoteEnum.DownVote;
                    var voteRegistration = new VoteRegistration(){
                        ApplicationUserId = user, 
                        PostId = postId,
                        VoteValue = enumVoteValue,
                    };
                    _context.VoteRegistrations.Add(voteRegistration);
                }
            }
            await _context.SaveChangesAsync();

            return Ok("Post votes added successfully.");

        }

        [HttpPost("AddCommentVotes/{max_comment_id}/{max_vote_per_user}")]
        public async Task<IActionResult> AddCommentVotes(int max_comment_id, int max_vote_per_user) {
            var random = new Random();
            var userIds = _context.ApplicationUsers.Select(au => au.Id).ToList();
            var commentIds = _context.Comments.Where(c => c.Id < max_comment_id).Select(c => c.Id ).ToList();

            foreach ( var userId in userIds) {
                var shuffledCommentIds = commentIds.OrderBy(x => random.Next()).ToList();
                int numberOfVotes = random.Next(0, max_vote_per_user);
                var commentsToVote = shuffledCommentIds.Take(numberOfVotes).ToList();
                foreach (int commentId in commentsToVote) {
                    int randomNumber = random.Next(0, 100);
                    int voteValue = randomNumber < 90? 1 : -1;
                    VoteEnum enumVoteValue = voteValue == 1 ? VoteEnum.UpVote : VoteEnum.DownVote;
                    var exist_vote = _context.CommentVoteRegistrations.FirstOrDefault(cv => cv.ApplicationUserId == userId && cv.CommentId == commentId );
                    if (exist_vote == null) {
                        var commentVote = new CommentVoteRegistration() {
                            ApplicationUserId = userId,
                            CommentId = commentId,
                            VoteValue = enumVoteValue,
                        };
                        _context.CommentVoteRegistrations.Add(commentVote);
                    }
                    
                }
            }
            await _context.SaveChangesAsync();

            return Ok("Comment votes added successfully.");

        }


        [HttpPost("AddBaseComments/{count}/{isChildComment}")]
        public async Task<IActionResult> AddComments(int count, bool isChildComment = false ) {
            var random = new Random();
            var postIds = _context.Posts.Select(p => p.Id).ToList();
            var userIds = _context.ApplicationUsers.Select(au => au.Id).ToList();
            var commentIds = _context.Comments.Select(c => c.Id ).ToList();

            for (int i = 0; i < count; i++ ) {
                var randomPostId = postIds[random.Next(postIds.Count)];
                var randomUserId = userIds[random.Next(userIds.Count)];
                var randomBaseCommentId = isChildComment ? commentIds[random.Next(commentIds.Count)] : 0 ;

                var lorem = new Bogus.DataSets.Lorem();
                string randomText = lorem.Paragraph(); // Generates a random Lorem Ipsum paragraph
                
                Comment newComment;
                if (isChildComment) {
                    var baseComment = _context.Comments.FirstOrDefault(c => c.Id == randomBaseCommentId);
                    var baseCommentPostId = baseComment.PostId;
                    newComment = new Comment(){ 
                    PostId = baseCommentPostId,
                    Content = randomText,
                    ApplicationUserId = randomUserId,
                    BaseCommentId = randomBaseCommentId
                }; }
                else {
                    newComment = new Comment(){
                    PostId = randomPostId,
                    Content = randomText,
                    ApplicationUserId = randomUserId,
                    };
                }

                _context.Comments.Add(newComment);
            }

            await _context.SaveChangesAsync();

            return Ok("Comments added successfully.");

        }


        private string GenerateRandomEmail()
        {
            // Your implementation to generate a random email address
            return $"user_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com"; // Example email address format
        }

    }
}