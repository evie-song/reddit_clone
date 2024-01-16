using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Models
{
    public class AuthResult

    {
        public string Token { get; set; } 
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
        public string? Username {get; set;}
        public string? Email {get; set; }
        public string? UserId {get; set; }
        
    }
}