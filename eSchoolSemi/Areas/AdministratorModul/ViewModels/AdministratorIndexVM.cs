using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AdministratorIndexVM
    {


        public int KorisnikId { get; set; }

        public List<Row> Administratori { get; set; }

        public class Row {

            public int AdministratorId { get; set; }

            public string ImePrezime { get; set; }



            public string Username { get; set; }

            public string Password { get; set; }
        }
        
    }
}
