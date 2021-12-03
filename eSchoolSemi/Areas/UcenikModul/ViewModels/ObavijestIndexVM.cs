using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.UcenikModul.ViewModels
{
    public class ObavijestIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int ObavjestenjeId { get; set; }

            public string Naslov { get; set; }


            [DataType(DataType.Date)]
            public DateTime DatumObavjestenja { get; set; }

            public string TipObavijesti { get; set; }


            public string Autor { get; set; }


        }


    }
}
