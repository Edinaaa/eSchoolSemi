using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class OdjeljenjeIndexVM
    {

        public IQueryable<Row> Odjeljenja { get; set; }

        public class Row {

            public int OdjeljenjeID { get; set; }

            public string GodinaStudija { get; set; }

            public string Oznka { get; set; }

            public int Kapacitet { get; set; }

            public string Razrednik { get; set; }

            public string Predstavnik { get; set; }

            public int BrojUcenika { get; set; }

        }

    }
}
