using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reddit_clone.Data;
using reddit_clone_backend.Dtos.VoteRegistration;
using reddit_clone_backend.Models;


namespace reddit_clone_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]
    public class VoteRegistrationController : Controller
    {
        private readonly DataContext _context;


        public VoteRegistrationController(DataContext context)
        {
            _context = context;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }

        [HttpPost]
        public async Task<ActionResult<List<VoteRegistration>>> AddVote (AddVoteDto request)
        {
                var voteRegistration = await _context.VoteRegistrations
                    .SingleOrDefaultAsync(vr => vr.ApplicationUserId == request.ApplicationUserId && vr.PostId == request.PostId);

                if (voteRegistration != null) {
                    if (voteRegistration.VoteValue == request.VoteValue){
                        _context.VoteRegistrations.Remove(voteRegistration);
                        await _context.SaveChangesAsync();
                    } else {
                        voteRegistration.VoteValue = request.VoteValue;
                        await _context.SaveChangesAsync();
                    }
                } else {
                    var newVoteRegistration = new VoteRegistration {
                        PostId = request.PostId,
                        ApplicationUserId = request.ApplicationUserId,
                        VoteValue = request.VoteValue
                    }; 
                    _context.VoteRegistrations.Add(newVoteRegistration);
                    await _context.SaveChangesAsync();
                }

                var votes = await _context.VoteRegistrations
                    .Where(sp => sp.ApplicationUserId == request.ApplicationUserId)
                    .ToListAsync();
                return Ok(votes); 

        } 
    }
}