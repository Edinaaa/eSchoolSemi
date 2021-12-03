using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class OdrzaniCasDetaljiIndexVM
    {
        public int OdjeljenjeID { get; set; }
        public int OdrzaniCasID { get; set; }
        public int OdrzaniCasDetaljiID { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public int UpisUOdjeljenjeID { get; set; }
        public string BrojUDnevniku { get; set; }
        public string Ucenik { get; set; }
        public bool Odsutan { get; set; }
        public string Napomena { get; set; }
        public int Ocjena { get; set; }
        public bool Opravdano { get; set; }
    }
}
