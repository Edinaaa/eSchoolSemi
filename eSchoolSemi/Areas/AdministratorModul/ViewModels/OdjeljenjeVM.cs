using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class OdjenjenjeVM
    {





        public int OdjeljenjeID { get; set; }

        [Range(1, 30, ErrorMessage = "Razred moze da sadrzi minimalno 1 ili maximalno 30 ucenika!")]
        [Remote(action: "ProvjeriOdjljenjeKapcitet", controller: "Odjeljenje", AdditionalFields = nameof(OdjeljenjeID))]
        public int Kapacitet { get; set; }


        public int? RasporedId { get; set; }


        [Required(ErrorMessage = "Ne smije bit prazno")]
        [RegularExpression("[5-8][-][a-z]", ErrorMessage = "Format oznake nije dobar.Primjer(5-a)")]
        [Remote(action:"ProvjeriOznaku",controller:"Odjeljenje", AdditionalFields = nameof(GodinaStudiranjaId) + "," + nameof(OdjeljenjeID))]
        public string Oznaka { get; set; }

        public int? UcenikID { get; set; }
        public List<SelectListItem> Predstavnik { get; set; }

        [Required(ErrorMessage = "Odaberite godinu studija")]
        [Remote(action: "ProvjeriRazred", controller: "Odjeljenje", AdditionalFields = nameof(Oznaka) + "," + nameof(RazredID))]
        public int GodinaStudiranjaId { get; set; }
        public List<SelectListItem> GodinaStudiranja { get; set; }

        [Remote(action: "ProvjeriRazrednika", controller: "Odjeljenje", AdditionalFields =nameof(GodinaStudiranjaId)+ "," + nameof(OdjeljenjeID))]
        public int NastavnikID { get; set; }
        public List<SelectListItem> Razrednik { get; set; }

        public int NastavniPlanId { get; set; }
        public List<SelectListItem> NastavniPlan { get; set; }

       
       
        
        

        [Remote(action: "ProvjeriRazred", controller: "Odjeljenje", AdditionalFields = nameof(Oznaka)+"," + nameof(GodinaStudiranjaId))]
        public int RazredID { get; set; }
        public List<SelectListItem> BrojRazreda { get; set; }

    }
}
