using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class OdjeljenjeUrediVm
    {
        public int OdjeljenjeId { get; set; }


        [Required(ErrorMessage = "Ne smije bit prazno")]
        [RegularExpression("[5-8][-][a-z]", ErrorMessage = "Format oznake nije dobar.Primjer(5-a)")]
        [Remote(action: "urediProvjeriOznaku", controller: "Odjeljenje", AdditionalFields = nameof(GodinaStudiranjaId) + "," + nameof(RazredId) + "," + nameof(OdjeljenjeId))]
        public string Oznaka { get; set; }

        [Range(1, 30, ErrorMessage = "Razred moze da sadrzi minimalno 1 ili maximalno 30 ucenika!")]
        [Remote(action: "ProvjeriOdjljenjeKapcitet", controller: "Odjeljenje", AdditionalFields = nameof(OdjeljenjeId))]
        public int Kapacitet { get; set; }

        public int GodinaStudiranjaId { get; set; }

        public int RazredId { get; set; }
        public string Razred { get; set; }

        [Remote(action: "ProvjeriRazrednika", controller: "Odjeljenje", AdditionalFields = nameof(GodinaStudiranjaId) + "," + nameof(OdjeljenjeId))]
        public int? NastavnikID { get; set; }
        public List<SelectListItem> Nastavnici { get; set; }

        public int? PredstavnikId { get; set; }
        public List<SelectListItem> Ucenici { get; set; }

        public int? NastavniPlanId { get; set; }
        public List<SelectListItem> NastavniPlan { get; set; }


    }
}
