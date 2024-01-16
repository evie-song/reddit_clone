using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace reddit_clone_backend.Models
{
    public class SavedPost
    {
        public string ApplicationUserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
    }
}