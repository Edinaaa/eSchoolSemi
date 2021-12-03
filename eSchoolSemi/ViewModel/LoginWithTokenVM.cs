using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.ViewModels
{
    public class LoginWithTokenVM
    {
       
        public string username { get; set; }
       
        public string password { get; set; }
        [Required(ErrorMessage = "Unesite token")]
        public string Token { get; set; }
        public bool ZapamtiPassword { get; set; }
    }
}
