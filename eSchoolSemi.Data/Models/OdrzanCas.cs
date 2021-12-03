using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class OdrzanCas
    {
        [Key]
        public int OdrzanCasId { get; set; }
        [DataType(DataType.Date)]

        public string Naziv { get; set; }
        public DateTime DatumOdrzavanja { get; set; }

        public int NastavniPlanPredmetId { get; set; }
        public NastavniPlanPredmet NastavniPlanPredmet { get; set; }

        public int OdjeljenjeID { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
    }
}
