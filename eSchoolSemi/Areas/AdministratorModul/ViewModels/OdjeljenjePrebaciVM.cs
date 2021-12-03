using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class OdjeljenjePrebaciVM
    {

        
        public int OdjeljenjeID { get; set; }
        public string Oznaka { get; set; }

        [Remote(action: "ProvjeriKapacitet", controller: "Odjeljenje", AdditionalFields = nameof(OdjeljenjeID))]
        public int OdabranoOdljeljenjeID { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }
    }
}
