using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class SastanakTip
    {
        [Key]
        public int SastanakTipId { get; set; }
        public string Naziv { get; set; }
    }
}
