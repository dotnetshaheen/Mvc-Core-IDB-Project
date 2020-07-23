using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models.UserIdentityModel
{
    public class IdentityModel:IdentityUser
    {
        public string NameInitial { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
        [NotMapped]
        public IFormFile imgFile { get; set; }
    }
}
