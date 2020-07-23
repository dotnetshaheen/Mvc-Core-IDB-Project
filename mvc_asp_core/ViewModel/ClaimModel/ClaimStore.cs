using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace mvc_asp_core.ViewModel.ClaimModel
{
    // Static Data 
    public static class ClaimStore
    {
        public static List<Claim> GetClaims = new List<Claim>
        {
            new Claim("Create","Create"),
            new Claim("Read","Read"),
            new Claim("Update","Update"),
            new Claim("Delete","Delete")
        };
    }
}
