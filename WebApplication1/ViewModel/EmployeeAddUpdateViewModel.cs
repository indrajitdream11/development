using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class EmployeeAddUpdateViewModel
    {
        public Int32?  ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public Int32 CountryID { get; set; }
        [Required]
        public String Designation { get; set; }
        [Required]
        public String EmployerName { get; set; }
    }
    public class EmployeeRquestModel
    {
        public int? id { get; set; }
    }
}