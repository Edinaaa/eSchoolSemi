using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class VladanjeIndexVM
    {
        public int KorisnikID { get; set; }
        public int OdjeljenjeID { get; set; }
        public string Ime { get; set; }
        public string Vladanje { get; set; }
    }
}
