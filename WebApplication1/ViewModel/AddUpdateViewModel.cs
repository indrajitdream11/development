using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class AddUpdateViewModel
    {
        [Required(ErrorMessage = "The ID is required.")]
        [DisplayName("ID")]
        public Int32 ID { get; set; }

        [Required(ErrorMessage = "The Message is required.")]
        [DisplayName("Message")]
        public String Message { get; set; }

        [Required(ErrorMessage = "The Successful is required.")]
        [DisplayName("Successful")]
        public Boolean Successful { get; set; }
    }
}