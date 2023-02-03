using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class EmployeeListViewModel
    {
        public Int32 ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
       
        public String Country { get; set; }
        [Required]
        public Int32 CountryID { get; set; }
       
        public String Designation { get; set; }
      
    }
}