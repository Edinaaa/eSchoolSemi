using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace eSchool.Data.Models
{
    public class Materijal
    {
        [Key]
        public int MaterijalId { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }
        [DataType(DataType.Date)]
        //public HttpPostedFileBase file { get; set; }
        public DateTime DatumObjave { get; set; }
        public int? PredmetId { get; set; }
        public string FileURL { get; set; }
        public string FilePath { get; set; }
        public Predmet Predmet { get; set; }
        public int? NastavnikId { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public int? NastavniPlanPredmetId { get; set; }
        public NastavniPlanPredmet NastavniPlanPredmet { get; set; }
    }
}
