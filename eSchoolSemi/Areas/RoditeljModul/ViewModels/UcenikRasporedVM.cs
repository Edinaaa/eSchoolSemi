using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class UcenikRasporedVM
    {

        public string Odjeljenje { get; set; }
        public List<Row> Rows { get; set; }
        public List<SelectListItem> PocetankCasa { get; set; }
        public List<SelectListItem> Dan { get; set; }


        public class Row
        {
            
            public string Predmet { get; set; }

            public string PocetakCasa { get; set; }

            public string PocetakCasaID { get; set; }

            public string DanId { get; set; }

            public string Dan { get; set; }
        }

    }
}
