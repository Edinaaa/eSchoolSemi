using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class AjaxOdrzaniCasDetaljiVM
    {
        public int OdrzaniCasDetaljiID { get; set; }
        public int? Ocjena { get; set; }
        public bool Odsutan { get; set; }
        public bool Opravdano { get; set; }
    }
}
