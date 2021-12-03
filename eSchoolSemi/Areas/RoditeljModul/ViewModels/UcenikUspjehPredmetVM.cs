using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class UcenikUspjehPredmetVM
    {
        public int UcenikID { get; set; }
        public string ImePrezime { get; set; }
        public int PredmetID { get; set; }
        public string Predmet { get; set; }
        public List<Row> Rows { get; set; }
        public int Zakljucna { get; set; }
        public bool Zakljucena { get; set; }
        public class Row
        {
            public int Ocjena { get; set; }
            public DateTime DatumOcjenjivanja { get; set; }

        }
    }
}
