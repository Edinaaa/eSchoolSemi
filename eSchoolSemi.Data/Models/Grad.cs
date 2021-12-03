using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Grad
    {
        [Key]
        public int GradId { get; set; }
        public string Naziv { get; set; }
    }
}
