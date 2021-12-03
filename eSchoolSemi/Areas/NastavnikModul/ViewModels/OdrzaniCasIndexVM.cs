using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class OdrzaniCasIndexVM
    {
        public int OdrzaniCasID { get; set; }
        public int OdjeljenjeID { get; set; }
        public string Predmet { get; set; }
        public string OznakaOdjeljenja { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
    }
}
