using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class PatientServiceRelatedEntity
    {
        public IEnumerable<PatientServices> patientServices { get; set; }
        public IEnumerable<Services> services { get; set; }
        public IEnumerable<Patient> patients { get; set; }
        public IEnumerable<Employee> employees { get; set; }
        public IEnumerable<Invoice> invoices { get; set; }
        public IEnumerable<Department> departments { get; set; }
    }
}
