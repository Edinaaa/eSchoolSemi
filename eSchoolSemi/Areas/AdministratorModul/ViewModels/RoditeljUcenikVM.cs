using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class RoditeljUcenikVM
    {
 
        public Roditelj Roditelj { get; set; }
        public List<Ucenik> Ucenici { get; set; }
    }
}
