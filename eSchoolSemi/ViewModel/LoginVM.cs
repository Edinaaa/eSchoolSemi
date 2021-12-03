using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Unesite korisnicko ime")]
        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati mininalno 3 karaktera.", MinimumLength = 3)]
        public string username { get; set; }
        [Required(ErrorMessage ="Unesite lozinku")]
        [StringLength(100, ErrorMessage = "Password mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool ZapamtiPassword { get; set; }
    }
}