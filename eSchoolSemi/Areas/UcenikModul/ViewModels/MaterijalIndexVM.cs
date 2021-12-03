using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.UcenikModul.ViewModels
{
    public class MaterijalIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {

            public int MaterijalID { get; set; }
            public string Naziv { get; set; }
            public string  Nastavnik { get; set; }
            public string Predmet { get; set; }
            public string FilePath { get; set; }

            public DateTime DatumObjave { get; set; }
        }
    }
}
