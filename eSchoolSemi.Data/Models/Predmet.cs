using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Predmet
    {
        [Key]
        public int PredmetId { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
    }
}
