using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required, DisplayName("Department Name"), StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(300)]
        public string Details { get; set; }        
        public ICollection<Employee> Employees { get; set; }
    }
}
