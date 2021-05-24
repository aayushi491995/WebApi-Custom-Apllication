using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentWebApi.Models
{
    public class ProductView
    {
        public int id { get; set; }
        [Required]
        public string pname { get; set; }
        [Required]
        public string pdesc { get; set; }
        [Required]        
        public Nullable<double> price { get; set; }
    }
}