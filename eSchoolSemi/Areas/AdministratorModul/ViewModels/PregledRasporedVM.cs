using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class PregledRasporedVM
    {
        public IEnumerable<Row> listaPredmeta { get; set; }

        public class Row {

            public string Predmet { get; set; }

            public string Dan { get; set; }

            public string PocetakCasa { get; set; }

        }
    }
}
