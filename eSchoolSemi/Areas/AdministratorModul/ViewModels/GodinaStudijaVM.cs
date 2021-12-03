using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class GodinaStudijaVM
    {

        public int TretnutnaGodinaID { get; set; }
        public string Opis { get; set; }

        public int GodinaStudijaId { get; set; }
        public List<SelectListItem> GodinaStudija { get; set; }
    }
}
