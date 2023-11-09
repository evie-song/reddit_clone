using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using reddit_clone.Configurations;
using reddit_clone_backend.Dtos.Authentication;
using reddit_clone_backend.Models;

namespace reddit_clone_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        // private readonly JwtConfig _jwtConfig;
        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
            // JwtConfig jwtConfig
        )
        {   
            _userManager = userManager;
            _configuration = configuration;
            // _jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto request){
            // check if the input is valid
            if (ModelState.IsValid){
                // check if the user email already exists
                var user_exist = await _userManager.FindByEmailAsync(request.Email);

                if (user_exist != null) {
                    return BadRequest(new AuthResult() {
                        Result = false,
                        Errors = new List<string>() {
                            "Email already exist"
                        }
                    });
                }

                // Create a user
                var new_user = new IdentityUser(){
                    Email = request.Email,
                    UserName = request.Email
                };

                var is_created = await _userManager.CreateAsync(new_user, request.Password);

                if (is_created.Succeeded) {
                    // generate token
                    var token = GenerateGwtToken(new_user);
                    return Ok(new AuthResult(){
                        Result = true,
                        Token = token
                    });

                };

                return BadRequest(new AuthResult(){
                    Result = false, 
                    Errors = new List<string>(){
                        "Server error"
                    }
                });

            }
            return BadRequest();
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request) {
            if (ModelState.IsValid) {
                var existing_user = await _userManager.FindByEmailAsync(request.Email);
                if (existing_user == null) {
                    return BadRequest(new AuthResult(){
                        Result = false, 
                        Errors = new List<string>(){
                            "User doesn't exist"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existing_user, request.Password);
                if (!isCorrect) {
                    return BadRequest(new AuthResult(){
                        Result = false, 
                        Errors = new List<string>(){
                            "Invalid credentials"
                        }
                    });
                }

                var jwtToken = GenerateGwtToken(existing_user);
                return Ok(new AuthResult(){
                    Token = jwtToken,
                    Result = true
                });
            }

            return BadRequest(new AuthResult(){
                Result = false, 
                Errors = new List<string>(){
                    "Invalid Payload"
                }
            });
        }

        private string GenerateGwtToken(IdentityUser user){
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection(key:"JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity( new []{
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}