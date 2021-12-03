using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class UpisUOdjeljenje
    {
        [Key]
        public int UpisUOdjeljenjeId { get; set; }

        public int BrojUDnevniku { get; set; }

        public int UcenikId { get; set; }
        public Ucenik Ucenik { get; set; }

        public int OdjeljenjeId { get; set; }
        public Odjeljenje Odjeljenje { get; set; }

    }
}
