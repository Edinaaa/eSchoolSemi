
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Sastanak
    {
        [Key]
        public int SastanakId { get; set; }
        public string Naziv { get; set; }
        public int? SastanakTipId { get; set; }
        public SastanakTip SastanakTip { get; set; }
        public int? NastavnikId { get; set; }
        public Nastavnik Nastavnik { get; set; }


        public int? OrganizatorId { get; set; }
        public Korisnik Organizator { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumSastanka { get; set; }
        public DateTime DatumObavijest { get; set; }
        public string Opis { get; set; }
        public int? OdjeljenjeId { get; set; }
        public Odjeljenje Odjeljenje { get; set; }

    }
}
