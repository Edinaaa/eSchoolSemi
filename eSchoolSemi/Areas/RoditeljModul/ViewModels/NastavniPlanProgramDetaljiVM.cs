using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class NastavniPlanProgramDetaljiVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int BrojCasova { get; set; }
            public string Predmet { get; set; }
            public string Oznaka { get; set; }
            public string ImePrezimeNastavnika { get; set; }
            public int NastavnikID { get; set; }

        }
    }
}
