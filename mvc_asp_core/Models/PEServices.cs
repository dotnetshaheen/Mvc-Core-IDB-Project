using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class PEServices
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        //---------------------------
        public Patient Patient { get; set; }
        public Employee Employee { get; set; }
        public Services Service { get; set; }
    }
}
