using eSchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSchoolSemi.Data.Models
{
    public class Raspored
    {
        public int RasporedID { get; set; }

        public string  Naziv { get; set; }

        public int OdjeljenjeID { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
    }
}
