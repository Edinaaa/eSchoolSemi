using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class NastavniPlanDodajVM
    {
        public int NastavniPlanId { get; set; }

        [Required(ErrorMessage ="Polje ne smije biti prazno")]
        public string Naziv { get; set; }

        public int GodinaStudiranjaId { get; set; }
        public List<SelectListItem> GodinaStudiranja { get; set; }

        [Remote(action:"ProvjeriPlan",controller:"NastavniPlan",AdditionalFields =nameof(GodinaStudiranjaId))]
        public int RazredId { get; set; }
        public List<SelectListItem> Razred { get; set; }

    }
}
