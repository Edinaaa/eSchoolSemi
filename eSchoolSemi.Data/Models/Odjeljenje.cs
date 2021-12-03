using eSchoolSemi.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Odjeljenje
    {
        [Key]
        public int OdjeljenjeId { get; set; }

        public int? RazredID { get; set; }
        public Razred Razred { get; set; }

        public bool Prebacen { get; set; }

        public string Oznaka { get; set; }
        public int Kapacitet { get; set; }

        public int? RazrednikId { get; set; }
        public Nastavnik Razrednik { get; set; }

        public int? UcenikID { get; set; }
        public Ucenik Ucenik { get; set; }

        public int? NastavniPlanId { get; set; }
        public NastavniPlan NastavniPlan { get; set; }

        public int? GodinaStudijaId { get; set; }
        public GodinaStudija GodinaStudija  { get; set; }

       
        public Raspored Raspored { get; set; }

    }
}
