using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.NastavnikModul.ViewModels
{
    public class OdjeljenjeVM
    {
        public int OdjeljenjeID { get; set; }
        public string Godina { get; set; }
        public string Oznaka { get; set; }
    }
}
