using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Roditelj:Korisnik
    {
            public List<Ucenik> Uceniks { get; set; }
    }
}
