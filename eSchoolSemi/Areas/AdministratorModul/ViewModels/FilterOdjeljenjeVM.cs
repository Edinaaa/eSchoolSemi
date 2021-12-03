using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class FilterOdjeljenjeVM
    {
        public int GodinaStudijID { get; set; }
        public List<SelectListItem> GodineStudija { get; set; }

        public int RazredID { get; set; }
        public List<SelectListItem> Razred { get; set; }

        public bool ImalRazrednik { get; set; }

        public bool ImalPredstavnik { get; set; }

    }
}
