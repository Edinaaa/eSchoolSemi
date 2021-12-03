using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class SastanakDetaljiVM
    {
        public int SastanakId { get; set; }
        public DateTime DatumSastanka { get; set; }
        public DateTime DatumObjave { get; set; }

        public string Naziv { get; set; }

        public string TipSastanka { get; set; }

   

        public string Odjeljenje { get; set; }
        public string Opis { get; set; }
        public string Komentar { get; set; }
        public string Nastavnik { get; internal set; }
        public string Organizator { get; internal set; }
    }
}
