using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Ucenik:Korisnik
    {
        
        public string Vladanje { get; set; }
        public int? RoditeljId { get; set; }
        public Roditelj Roditelj { get; set; }
        public byte[] KorisnickaSlika { get; set; }
    }
}
