using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class NastavniPlanProgramIndexVM
    {
        public List<Row> Rows { get; set; }
        public  class Row
        {
            public int UcenikId { get; set; }
            public string ImePrezime { get; set; }
            public string ImePrezimeRazredika { get; set; }
            public int RazredikID { get; set; }


            public string GodinaStudija { get; set; }
           

            public int NastavniPlanID { get; set; }

            public string Odjeljenje { get; set; }
            public string Naziv { get; set; }
        }
    }
}
