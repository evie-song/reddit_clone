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
                return Ok(new { NewCommunities = addedCommunities});
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, "Error occurred while adding communities from file.");
            }
        }




        private string GenerateRandomEmail()
        {
            // Your implementation to generate a random email address
            return $"user_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com"; // Example email address format
        }

    }
}