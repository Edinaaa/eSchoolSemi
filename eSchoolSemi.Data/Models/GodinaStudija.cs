using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class GodinaStudija
    {
        [Key]
        public int GodinaStudijaId { get; set; }
        public string Godina { get; set; }
        public bool TrenutnaGodina { get; set; }
    }
}
