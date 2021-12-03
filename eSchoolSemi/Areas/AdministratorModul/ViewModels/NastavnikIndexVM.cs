using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class NastavnikIndexVM
    {

        public List<Row> Nastavnici { get; set; }

        public class Row
        {
            public int NastavniID { get; set; }

            public string ImePrezime { get; set; }

            public string Zvanje { get; set; }

            public string Titula { get; set; }

            public string DatumZaposljenja { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

        }
    }
}
