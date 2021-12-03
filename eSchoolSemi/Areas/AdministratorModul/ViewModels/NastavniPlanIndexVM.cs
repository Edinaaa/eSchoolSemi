using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class NastavniPlanIndexVM
    {

        public int GodinaStudijaId { get; set; }
        public List<SelectListItem> GodineStudija { get; set; }
        public List<NastavniPlan> Planovi { get; set; }


    }
}
