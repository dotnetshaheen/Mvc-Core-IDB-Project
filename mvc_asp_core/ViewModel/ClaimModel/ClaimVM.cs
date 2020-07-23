using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.ViewModel.ClaimModel
{
    public class ClaimVM
    {
        public ClaimVM()
        {
            userClaims = new List<UserClaim>();
        }
        public string Email { get; set; }
        public List<UserClaim> userClaims { get; set; }
    }
}
