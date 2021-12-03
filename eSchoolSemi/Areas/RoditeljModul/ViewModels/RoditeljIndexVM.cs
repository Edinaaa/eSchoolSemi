using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class RoditeljIndexVM

    {
        public int RoditeljID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string ImePrezime { get; set; }
        [DataType(DataType.Date)]
  
        public DateTime DatumRodenja { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }
    }
}
