using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class ObavijestiDodajVM
    {
        public int NastavnikID { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public int TipID { get; set; }
        public List<SelectListItem> Tip { get; set; }
        public DateTime DatumObavjeljivanje { get; set; }
    }
}
