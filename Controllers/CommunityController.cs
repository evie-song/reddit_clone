using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using Microsoft.Extensions.Logging;
using reddit_clone.Data;

namespace reddit_clone.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]
    public class CommunityController : ControllerBase
    {
        private readonly DataContext _context;

        public CommunityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Community>>> GetAllCommunity()
        {   
            return Ok(await _context.Communities.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<List<Community>>> AddCommunity (AddCommunityDto request)
        {   
            var newCommunity = new Community {
                Title = request.Title,
            };

            _context.Communities.Add(newCommunity);
            await _context.SaveChangesAsync();
            return Ok(await _context.Communities.ToListAsync());
        }
    }
}