using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class ObavjestFilterVm
    {
        public string Naslov { get; set; }

        public int? NastavnikID { get; set; }
        public List<SelectListItem> Nastavnik { get; set; }

        public int? TipObavjestiID { get; set; }
        public List<SelectListItem> TipObavjesti { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DatumOd { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DatumDo { get; set; }
    }
}
