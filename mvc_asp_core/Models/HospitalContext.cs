using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class HospitalContext:DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options):base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeCategory> EmployeeCategorie { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientServices> PatientService { get; set; }
        public DbSet<Services> Services { get; set; }

    }
}
