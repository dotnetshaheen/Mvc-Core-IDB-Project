using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_asp_core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [DataType(DataType.Date), DisplayName("Date of Birth")]
        public DateTime Dob { get; set; }
        [Range(18,30)]
        public int Age { get; set; }
        [DisplayName("Joining Date")]
        public DateTime JoiningDate { get; set; }
        [ForeignKey("department"), DisplayName("Department Id")]
        public int DeptID { get; set; }
        [ForeignKey("EmployeeCategory"), DisplayName("Category Id")]
        public int EmCategoryID { get; set; }
        [ForeignKey("designation"), DisplayName("Designation Id")]
        public int DesignationId { get; set; }
        public string Photo { get; set; }
        [NotMapped]
        public IFormFile imgFile { get; set; }
        //---------------------------------------------
        public virtual Department department { get; set; }
        public virtual Designation designation { get; set; }
        public virtual EmployeeCategory EmployeeCategory { get; set; }

        public ICollection<PEServices> PEServices { get; set; }
    }
}
