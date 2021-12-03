using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class NastavnikGradViewModel
    {
        public int NastavniID { get; set; }

        private const string V = @"\(\+387\)0[6|3][0-9]\-\d{3}\-\d{3,4}";

        [StringLength(50)]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        public string Ime { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        public string Prezime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        public DateTime DatumRodjenja { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        public DateTime DatumZaposljenja { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        public string Titula { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Zvanje ne smije biti prazan!")]
        public string Zvanje { get; set; }

        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        [EmailAddress(ErrorMessage = "Pogresan format e-maila!")]
        [Remote(action: "ProvjeriEmail", controller: "Ucenik")]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        [RegularExpression(V, ErrorMessage = "Format telefona nije dobar.Primjer(+387)062-555-555")]
        [Remote("ProvjeriTelefon",controller: "Ucenik")]
        public string Telefon { get; set; }

        [Remote(action: "ProvjeriUsername", controller: "Ucenik")]
        [Required(ErrorMessage = "Ovo polje nesmije biti prazno!")]
        public string Username { get; set; }

        
        public string Password { get; set; }

        public int? GradID { get; set; }
        public List<SelectListItem> Gradovi { get; set; }

        public String MjestoRodjenja { get; set; }
    }
}
