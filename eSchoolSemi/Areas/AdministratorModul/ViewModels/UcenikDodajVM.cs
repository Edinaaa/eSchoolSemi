using eSchool.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class UcenikDodajVM
    {
        private const string V = @"\(\+387\)0[6|3][0-9]\-\d{3}\-\d{3,4}";

        public int UcenikID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Ime ne smije biti prazno!")]
        public string Ime { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Prezime ne smije biti prazno!")]
        public string Prezime { get; set; }

        
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Datum rodjenja ne smije biti prazan!")]
        public DateTime DatumRodjenja { get; set; }

        //Provjeri u bazi imal vakvih e-mailova
        [Required(ErrorMessage = "Email ne smije biti prazan!")]
        [EmailAddress(ErrorMessage ="Pogresan format e-maila!")]
        [Remote(action: "ProvjeriEmail", controller: "Ucenik")]
        public string Email { get; set; }

        //Dodat mozda regex u formatu BIH brojeva?
        [Phone]
        [RegularExpression(V, ErrorMessage = "Format telefona nije dobar.Primjer(+387)062-555-555")]
        [Required(ErrorMessage = "Telefon ne smije biti prazan!")]
        [Remote(action: "ProvjeriTelefon", controller: "Ucenik")]
        public string Telefon { get; set; }

        [Remote(action: "ProvjeriUsername", controller: "Ucenik")]
        [Required(ErrorMessage = "Username ne smije biti prazan!")]
        public string Username { get; set; }

        
        
        public int? BrojUDnevniku { get; set; }

        
        [DataType(DataType.Upload)]
        [Remote(action: "VerifyFile", controller: "Ucenik")]
        public IFormFile KorinickaSlika { get; set; }



        public int? RoditeljID { get; set; }
        public List<SelectListItem> Roditleji { get; set; }

        [Remote(action:"ProvjeriRoditelj",controller:"Ucenik")]
        public string Roditelj { get; set; }

        public int? GradID { get; set; }
        public List<SelectListItem> Gradovi { get; set; }

        
    }
}
