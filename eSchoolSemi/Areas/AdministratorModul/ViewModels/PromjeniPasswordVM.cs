using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class PromjeniPasswordVM
    {
        public int KorisnickiNalogId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Stari password ne smije biti prazno!")]
        [Remote(action: "ProvjeriStari", controller: "Administrator", AdditionalFields = nameof(KorisnickiNalogId))]
        public string StariPassword { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Novi password ne smije biti prazno!")]
        [Remote(action: "ProvjeriNovi", controller: "Administrator", AdditionalFields = nameof(StariPassword))]
        public string NoviPassword { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Polje ne smije biti prazno!")]
        [Remote(action: "ProvjeriPromjeni", controller: "Administrator", AdditionalFields = nameof(NoviPassword))]
        public string PromjeniPassword { get; set; }
    }
}
