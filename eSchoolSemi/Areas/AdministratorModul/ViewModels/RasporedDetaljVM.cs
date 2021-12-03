using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class RasporedDetaljVM
    {
        public int RasporedID { get; set; }

        public int OdjeljenjeId { get; set; }

        public int PredmetId { get; set; }
        public List<SelectListItem> Predmet { get; set; }

        public int DanId { get; set; }
        public List<SelectListItem> Dan { get; set; }

        public int PocetakCasaId { get; set; }
        public List<SelectListItem> PocetakCasa { get; set; }
    }
}
