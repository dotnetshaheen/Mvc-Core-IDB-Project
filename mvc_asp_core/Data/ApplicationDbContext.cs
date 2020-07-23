using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvc_asp_core.Models.UserIdentityModel;

namespace mvc_asp_core.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
