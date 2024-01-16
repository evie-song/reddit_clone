using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reddit_clone_backend.Dtos.SavedPost
{
    public class AddSavedPostDto
    {
        public int PostId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}