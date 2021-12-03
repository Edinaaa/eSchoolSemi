using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.UcenikModul.ViewModels
{
    public class UcenikIndexVM
    {
        public DateTime DatumRodenja { get; internal set; }
        public string Email { get; internal set; }
        public string ImePrezime { get; internal set; }
        public string Password { get; internal set; }
        public string Username { get; internal set; }
        public string Telefon { get; internal set; }

        public int UcenikID { get; internal set; }
    }
}
