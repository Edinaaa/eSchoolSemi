using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class OdrzaniCasDetaljiEditVM
    {
        public int OdrzanCasDetaljiId { get; set; }
        public bool Odsutan { get; set; }
        public bool Opravdano { get; set; }
        public string Ucenik { get; set; }
        public int Ocjena { get; set; }
    }
}
