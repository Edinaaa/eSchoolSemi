using eSchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSchoolSemi.Data.Models
{
    public class ZakljucnaOcjena
    {
        public int ZakljucnaOcjenaID { get; set; }

        public int Ocjena { get; set; }

        public DateTime DatumZakljucivanja { get; set; }

        public int UpisUOdjeljenjeId { get; set; }
        public UpisUOdjeljenje UpisUOdjeljenje { get; set; }

        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }
    }
}
