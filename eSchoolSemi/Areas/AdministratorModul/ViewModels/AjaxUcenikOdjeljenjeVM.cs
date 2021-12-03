using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AjaxUcenikOdjeljenjeVM
    {

        public int OdjeljenjeID { get; set; }

        public int UcenikId { get; set; }
        public string Naziv { get; set; }

        [Remote(action:"ProvjeriKapacitet",controller:"AjaxOdjeljenje")]
        public int OdjeljenjePrebacenoId { get; set; }
        public List<SelectListItem> Odjeljenja  { get; set; }
    }
}
