using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class OdjeljenjeUcenikVm
    {
        [Required(ErrorMessage = "Ime ne smije biti prazno!")]
        public int UcenikId { get; set; }
        public List<SelectListItem> Ucenici { get; set; }

        public int GodinaStudijaId { get; set; }
        public string GodineStudija { get; set; }

        public int RazredID { get; set; }
        public List<SelectListItem> Razredi { get; set; }

        [Required(ErrorMessage = "Ime ne smije biti prazno!")]
        [Remote(action: "ProvjeriKapacitet", controller: "Odjeljenje")]
        public int OdjeljenjeId { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }

    }
}
