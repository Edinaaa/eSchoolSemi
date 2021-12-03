using eSchoolSemi.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Obavjestenje
    {
        [Key]
        public int ObavjestenjeId { get; set; }

        public string Naslov { get; set; }

        public string Sadrzaj { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumObavjestenja { get; set; }

        public int? TipObavijestiId { get; set; }
        public TipObavijesti TipObavijesti { get; set; }

        public int? AdministatorID { get; set; }
        public Administrator Administrator { get; set; }

        public int? NastavnikID { get; set; }
        public Nastavnik Nastavnik { get; set; }

    }
}
