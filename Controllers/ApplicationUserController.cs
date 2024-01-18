using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web.Http.Cors;
using reddit_clone.Data;


namespace reddit_clone_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DisableCors]

    public class ApplicationUserController : Controller
    {
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly DataContext _context;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        // [HttpGet("")]



        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}