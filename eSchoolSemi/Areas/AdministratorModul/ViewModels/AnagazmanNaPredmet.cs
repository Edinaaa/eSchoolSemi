using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AnagazmanNaPredmet
    {
        [Range(1,42,ErrorMessage ="Broj casova mora da bude izmedju 1-42")]
        public int BrojCasova { get; set; }

        public int NastavniPlanProgramID { get; set; }
        public string Naziv { get; set; }

        [Required(ErrorMessage ="Izaberite predmet")]
        public int PredmetID { get; set; }
        public List<SelectListItem> predmet { get; set; }

        [Required(ErrorMessage = "Izaberite nastavnika")]
        [Remote(action:"ProvjeriZvanje",controller:"NastavniPlan", AdditionalFields = nameof(PredmetID) + "," + nameof(NastavniPlanProgramID))]
        public int NastavnikID { get; set; }
        public List<SelectListItem> nastavnik { get; set; }
    }
}
