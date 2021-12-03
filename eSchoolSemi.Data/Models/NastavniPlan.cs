using eSchoolSemi.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class NastavniPlan
    {
        [Key]
        public int NastavniPlanId { get; set; }
        public string Naziv { get; set; }

        public int GodinaStudiranjaId { get; set; }
        public GodinaStudija GodinaStudija { get; set; }

        public int RazredId { get; set; }
        public Razred Razred { get; set; }

        public bool Prebacen { get; set; }
    }
}
