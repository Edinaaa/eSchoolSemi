using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class NastavniPlanProgramNastavnikVM
    {

        public int NastavnikID { get; set; }
        public string ImePrezime { get; set; }
        public string PredajePredmet { get; set; }
        public string Titula { get; set; }
        public string Zvanje { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public string OdjeljenjeRazredik { get; set; }
    }
}
