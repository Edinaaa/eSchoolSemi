using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Nastavnik:Korisnik  {
       
        public string Titula { get; set; }

        
        public string Zvanje { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumZaposlenja { get; set; }

     


    }
}
