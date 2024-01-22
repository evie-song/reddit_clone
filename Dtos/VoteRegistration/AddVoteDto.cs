using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reddit_clone_backend.Models;


namespace reddit_clone_backend.Dtos.VoteRegistration
{
    public class AddVoteDto
    {
        public int PostId { get; set; }
        public string ApplicationUserId { get; set; }
        public Models.VoteEnum VoteValue { get; set; }

    }
}