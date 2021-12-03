using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using eSchoolSemi;
using eSchoolSemi.Data.Models;

namespace eSchool.Data.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikId { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodenja { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        [EmailAddress]
        public string Email { get; set; }

       

        public string  Telefon { get; set; }
        public Grad MjestoRodenja { get; set; }
        public int? GradId { get; set; }

        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }
    }
}
