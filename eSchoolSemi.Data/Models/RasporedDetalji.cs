using eSchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSchoolSemi.Data.Models
{
   public class RasporedDetalj
    {
        public int RasporedDetaljID { get; set; }

        public int RasporedID { get; set; }
        public Raspored Raspored { get; set; }

        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }

        public int PocetakCasaId { get; set;}
        public PocetakCasa PocetakCasa { get; set; }

        public int DanID { get; set; }
        public Dan Dan { get; set; }

    }
}
