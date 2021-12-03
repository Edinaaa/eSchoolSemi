using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class ListaPredmetaNastavniPlanVM
    {
        public int NastavniPlanId { get; set; }

        public string Naziv { get; set; }

        public int GodinaStudijaId { get; set; }

        public bool prebacen { get; set; }

        public List<Row> Angazovani { get; set; }

        public class Row {

            public int AngazovanID { get; set; }

            public string NazivPredmeta { get; set; }

            public int BrojCasova { get; set; }

            public string NazivNastavnika { get; set; }

               }
    }
}
