using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class NastavniPlanPredmet
    {
        [Key]
        public int NastavniPlanPredmetId { get; set; }

        
        public int BrojCasova { get; set; }
        

        public int? NastavniPlanId { get; set; }
        public NastavniPlan NastavniPlan { get; set; }

        public int? PredmetId { get; set; }
        public Predmet Predmet { get; set; }

      
    }
}
