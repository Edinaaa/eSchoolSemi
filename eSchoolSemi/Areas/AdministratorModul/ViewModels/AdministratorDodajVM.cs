using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AdministratorDodajVM
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Ime ne smije biti prazno!")]
        public string Ime { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Prezime ne smije biti prazno!")]
        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        
        [Required(ErrorMessage = "Username ne smije biti prazan!")]
        [Remote(action: "ProvjeriUsername", controller: "Ucenik")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password ne smije biti prazan!")]
        public string Password { get; set; }

        public int GradID { get; set; }
        public List<SelectListItem> Gradovi { get; set; }

    }
}
