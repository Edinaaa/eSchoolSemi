using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class OdrzaniCasDodajIndexVM
    {
        public int OdjeljenjeID { get; set; }
        public int PredmetID { get; set; }
        public string Odjeljenje { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
    }
}
