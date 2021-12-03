using eSchool.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchoolSemi.Data.Models
{
    public class ObavjestenjeOdjeljenje
    {
        [Key]
        public int ObavjestenjeOdjeljenjeID { get; set; }

        public int OdjeljenjeID { get; set; }
        public Odjeljenje Odjeljenje { get; set; }

        public int ObavjestenjeID { get; set; }
        public Obavjestenje Obavjestenje { get; set; }
    }
}
