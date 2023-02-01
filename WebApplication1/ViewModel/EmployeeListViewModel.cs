using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class EmployeeListViewModel
    {
        public Int32 ID { get; set; }
       
        public String Name { get; set; }
       
        public String Email { get; set; }
       
        public String Country { get; set; }
        public Int32 CountryID { get; set; }
       
        public String Designation { get; set; }
       
        public String EmployerName { get; set; }
    }
}