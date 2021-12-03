using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class TipOcjene
    {
        [Key]
        public int TipOcjeneId { get; set; }
        public string Tip { get; set; }
    }
}
