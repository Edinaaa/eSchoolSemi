using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.UcenikModul.ViewModels
{
    public class ObavijestDetaljiVM
    {
        public int ObavjestenjeId { get; set; }

        public string Naslov { get; set; }

        public string Sadrzaj { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumObavjestenja { get; set; }

        public string TipObavijesti { get; set; }

        public string Autor { get; set; }
    }
}
