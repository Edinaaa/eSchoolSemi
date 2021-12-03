using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class SastanakDodajVM
    {
       public List<SelectListItem> Ucenici { get; set; }
       public int UcenikId { get; set; }
        public DateTime DatumSastanka { get; set; }
        public DateTime DatumObjave { get; set; }

        public string Naziv { get; set; }

        public List<SelectListItem> TipSastanka { get; set; }

        public int RoditeljID { get; set; }

        public string    Opis { get; set; }
        public string Komentar { get; set; }
     
    }
}
