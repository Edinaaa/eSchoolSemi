using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class UcenikOdjeljenje
    {
        
        public int BrojUdnevniku { get; set; }

        public int UcenikId { get; set; } //ono sto se bira
        public List<SelectListItem> Ucenik { get; set; } //ono iz cega se bira

        public int OdjeljenjeId { get; set; }
        public string Oznaka { get; set; }


    }
}
