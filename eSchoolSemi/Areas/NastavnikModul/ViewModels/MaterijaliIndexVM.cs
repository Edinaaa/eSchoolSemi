using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class MaterijaliIndexVM
    {
        public int MaterijalID { get; set; }
        public string Autor { get; set; }
        public string Naslov { get; set; }
        public string FileURL { get; set; }
        public string FilePath { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumObjave { get; set; }
        public string Extension { get; set; }
    }
}
