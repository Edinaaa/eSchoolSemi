using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSchoolSemi.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Helper;
using eSchool.Data.Models;
using eSchoolSemi.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace eSchoolSemi.Controllers
{
    [Autorizacija(true, true, true, true)]
    public class HomeController : Controller
    {

        private readonly MojContext _context;

        public HomeController(MojContext context) => _context = context;

        public IActionResult Index()
        {


            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();

            Administrator prijavaA = _context.Administrators.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);
            Nastavnik prijavaN = _context._Nastavnik.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);
            Roditelj prijavaR = _context._Roditelj.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);
            Ucenik prijavaU = _context._Ucenik.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            if (prijavaA != null)
            {
               
                return RedirectToAction("Index", "Administrator", new { area = "AdministratorModul" });
            }

            if (prijavaN != null)
            {
                return RedirectToAction("Index", "Nastavnik", new { area = "NastavnikModul" });
            }

            if (prijavaR != null)
            {
                return RedirectToAction("Index", "Roditelj", new { area = "RoditeljModul" });
            }
           
            if (prijavaU != null)
            {
                return RedirectToAction("Index", "Ucenik", new { area = "UcenikModul" });
            }
            return View();
        }


        public JsonResult GetSearchValue(string search)
        {

            List<Grad> allsearch = _context._Grad.Where(x => x.Naziv.StartsWith(search)).Select(x => new Grad {

                GradId = x.GradId,
                Naziv = x.Naziv
            }).ToList();
            
            return new JsonResult(allsearch);
        }


    }
}
