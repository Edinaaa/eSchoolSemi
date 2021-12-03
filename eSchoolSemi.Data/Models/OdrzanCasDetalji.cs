using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class OdrzanCasDetalji
    {
        [Key]
        public int OdrzanCasDetaljiId { get; set; }
        public bool Odsutan { get; set; }
        public bool Opravdano { get; set; }

        public int? Ocjena { get; set; }

        public int? UpisUOdjeljenjeId { get; set; }
        public UpisUOdjeljenje UpisUOdjeljenje { get; set; }

        public int? OdrzanCasId { get; set; }
        public OdrzanCas OdrzanCas { get; set; }

        
    }
}
