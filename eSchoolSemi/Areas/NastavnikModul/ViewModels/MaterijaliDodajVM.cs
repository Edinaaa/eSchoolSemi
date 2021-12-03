using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class MaterijaliDodajVM
    {
        public int NastavnikID { get; set; }
        public int PredmetID { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public string FileURL { get; set; }
        public string FilePath { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
        public DateTime DatumObavjeljivanje { get; set; }
        public string Extension { get; set; }
    }
}
