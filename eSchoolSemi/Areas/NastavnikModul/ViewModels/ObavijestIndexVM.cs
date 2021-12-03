using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class ObavijestIndexVM
    {
        public int ObavjestID { get; set; }
        public string ImeAutora { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public string Tip { get; set; }
        public DateTime DatumObjave { get; set; }
    }
}
