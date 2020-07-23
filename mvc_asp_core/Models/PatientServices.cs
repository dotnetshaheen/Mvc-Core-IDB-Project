using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class PatientServices
    {
        public int Id { get; set; }

        [ForeignKey("Patient"), DisplayName("Patient Id")]
        public int PatientId { get; set; }

        [ForeignKey("Employee"), DisplayName("Doctor Id")]  
        public int DoctorID { get; set; }

        [ForeignKey("Services"), DisplayName("Service Id")]
        public int ServiceId { get; set; }

        [DataType(DataType.Date), Display(Name ="Date")]
        public DateTime ServiceDate { get; set; }
        [Required, DisplayName("Payment Status")]
        public string PaymentStatus { get; set; }
        //----------------------

        public virtual Services Services { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Employee Employee { get; set; }
        
        public ICollection<Employee> employees { get; set; }
        

    }
}
