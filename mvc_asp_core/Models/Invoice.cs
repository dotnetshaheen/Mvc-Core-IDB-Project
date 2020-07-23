using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal PaymentTotal { get; set; }
        public decimal DueTotal { get; set; }
        public DateTime Date { get; set; }

        public virtual Patient Patient { get; set; }

        
    }
}
