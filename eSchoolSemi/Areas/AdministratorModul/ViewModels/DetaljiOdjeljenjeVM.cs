using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class DetaljiOdjeljenjeVM
    {
        public int OdjeljenjeID { get; set; }

        public string Oznaka { get; set; }

        public string Razrednik { get; set; }

        public string Prredstavnik { get; set; }

        public string GodinaStudija { get; set; }

        public int  Kapacitet { get; set; }
    }
}
