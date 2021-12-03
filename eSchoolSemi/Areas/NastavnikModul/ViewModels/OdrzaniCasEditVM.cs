using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class OdrzaniCasEditVM
    {
        public string Predmet { get; set; }
        public string Odjeljenje { get; set; }
        public List<Rows> OdrzaniCesEdit { get; set; }

        public class Rows
        {
            public int OdrzaniCasID { get; set; }
            public int RazrednikID { get; set; }
            public int OdrzaniCasDetaljiID { get; set; }
            public string Ime { get; set; }
            public string BrojUDnevniku { get; set; }
            public bool Odsutan { get; set; }
            public bool Opravdano { get; set; }
            public int Ocjena { get; set; }
        }
    }
}
