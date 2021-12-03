using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class UcenikIndexVM

    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int UcenikID { get; set; }
            public string ImePrezime { get; set; }
            public string Vladanje { get; set; }
            public string Email { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string Telefon { get; set; }

        }
    }
}
