using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eSchoolSemi.Web.ViewModels;
using eSchool.Data.Models;
using eSchoolSemi.Web.Helper;

namespace eSchoolSemi.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private readonly MojContext _context;

        public AutentifikacijaController(MojContext context) => _context = context;
       
        public IActionResult Index()
        {
           
            return View(new LoginVM()
            {
                ZapamtiPassword = true,
            });
        }

        public IActionResult Login(LoginVM input)
        {
            KorisnickiNalog korisnik = _context.korisnickiNalogs
                .SingleOrDefault(x => x.Username == input.username && x.Password == input.password);

            if (korisnik == null)
            {
                TempData["error_poruka"] = "Pogrešan username ili password";
                return View("Index", input);
            }

            Administrator prijavaA = _context.Administrators.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);


            Nastavnik prijavaN = _context._Nastavnik.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);
            Roditelj prijavaR = _context._Roditelj.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);
          

            Ucenik prijavaU = _context._Ucenik.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            if (prijavaA != null)
            {
                
                korisnik.Zapamti = input.ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik,input.ZapamtiPassword);
                return RedirectToAction("Index","Administrator",new { area = "AdministratorModul" });
            }

            if (prijavaN != null)
            {
                korisnik.Zapamti = input.ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);
                //Ovde stavis gdje hoces da ti redirecta Login na koju stranicu
                return RedirectToAction("Index", "Nastavnik", new { area = "NastavnikModul" });
            }
            if (prijavaR != null)
            {
                korisnik.Zapamti = input.ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);
                //Ovde stavis gdje hoces da ti redirecta Login na koju stranicu
                return RedirectToAction("Index", "Roditelj", new { area = "RoditeljModul" });
            } 

            if (prijavaU != null)
            {
                korisnik.Zapamti = input.ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);
                return RedirectToAction("Index", "Ucenik", new { area = "UcenikModul" });
            }
            HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);

            return RedirectToAction("Index");
        }
        
       
    }
}