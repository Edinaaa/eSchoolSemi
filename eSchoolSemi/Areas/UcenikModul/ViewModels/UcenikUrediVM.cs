using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.UcenikModul.ViewModels
{
    public class UcenikUrediVM
    {
        public DateTime DatumRodenja { get;  set; }
        public string Ime { get;  set; }
        public string Email { get;  set; }
        public string Prezime { get;  set; }
        public string Password { get;  set; }
        public string Username { get;  set; }
        public string Telefon { get; set; }

        public int UcenikID { get;  set; }
    }
}
