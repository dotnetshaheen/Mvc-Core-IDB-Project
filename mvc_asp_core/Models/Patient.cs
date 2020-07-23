using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public enum Title
    {
        Mr, Mrs, Miss
    }
    public class Patient
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        [NotMapped]
        public  IFormFile imgFile { get; set; }
        //-------------------
        public ICollection<PatientServices> PatientServices { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<PEServices> PEServices { get; set; }
        

    }
}
