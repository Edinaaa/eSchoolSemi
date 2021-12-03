using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class ObavjestiIndexVM
    {

        public IQueryable<Row> Obavjesti { get; set; }

        public class Row {

            public int ObavjestID { get; set; }

            public string Naslov { get; set; }

            public string Sadrzaj { get; set; }

            public string TipObavjesti { get; set; }

            public string Autor { get; set; }

            public string DatumPostavljanja { get; set; }

            public string  ZaKoga { get; set; }
        }
       
    }
}
