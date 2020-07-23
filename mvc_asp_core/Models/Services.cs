using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public Decimal Cost { get; set; }

        //----------------------
        public ICollection<PatientServices> PatientServices { get; set; }
        public ICollection<PEServices> PEServices { get; set; }
    }
}
