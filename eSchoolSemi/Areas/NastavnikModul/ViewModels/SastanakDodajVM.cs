using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class SastanakDodajVM
    {
        public int NastavnikID { get; set; }
        public int OdjeljenjeID { get; set; }
        public DateTime DatumObavijest { get; set; }
        public DateTime DatumSastanka { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }
        public string Opis { get; set; }
        public string Naziv { get; set; }
        public int SastanakTipID { get; set; }

        public List<SelectListItem> SastanakTip { get; set; }

    }
}
