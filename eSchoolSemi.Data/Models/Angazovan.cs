using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Angazovan
    {
        [Key]
        public int AngazovanId { get; set; }

        public int? NastavnikId { get; set; }
        public Nastavnik Nastavnik { get; set; }    

        public int? NastavniPlanPredmetId { get; set; }
        public NastavniPlanPredmet NastavniPlanPredmet { get; set; }

        public static implicit operator List<object>(Angazovan v)
        {
            throw new NotImplementedException();
        }
    }
}
