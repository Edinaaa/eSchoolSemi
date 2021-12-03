using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AngazujNastavnikVM
    {

        public int NastavniPlanPredmetID { get; set; }

        public int NastavnikID { get; set; }
        public List<SelectListItem> Nastavnici { get; set; }     


    }
}
