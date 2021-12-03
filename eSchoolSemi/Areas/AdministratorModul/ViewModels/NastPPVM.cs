using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class NastPPVM
    {
        public string sifra { get; set; }

        public string godinaStudiranja { get; set; }

        public int brojSati { get; set; }

        public int nastavniPlanID { get; set; }
        public List<SelectListItem> nastavniPlan { get; set; }

        public int predmetID { get; set; }
        public List<SelectListItem> predmet { get; set; }

    }
}