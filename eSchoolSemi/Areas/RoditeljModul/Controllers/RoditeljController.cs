using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.RoditeljModul.ViewModels;
using eSchoolSemi.Web.Helper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.RoditeljModul.Controllers
{
    [Area("RoditeljModul")]
    [Autorizacija(false, false, true, false)]
    public class RoditeljController : Controller
    {
        private MojContext db;
        public RoditeljController(MojContext _db) { db = _db; }
        public IActionResult Index()
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Korisnik roditelj = db._Korisnik.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).Include(r=>r.KorisnickiNalog).FirstOrDefault();
            RoditeljIndexVM vm = new RoditeljIndexVM()
            {
                DatumRodenja=roditelj.DatumRodenja,
                Email=roditelj.Email,
                ImePrezime=roditelj.Ime+" "+roditelj.Prezime,
                Password=roditelj.KorisnickiNalog.Password,
                Username=roditelj.KorisnickiNalog.Username,
                RoditeljID=roditelj.KorisnikId,
                Telefon=roditelj.Telefon
            };
            return View(vm); 
               
        }
        public IActionResult Uredi(int RoditeljID)
        {
            Korisnik roditelj = db._Korisnik.Include(r => r.KorisnickiNalog).Where(x => x.KorisnikId == RoditeljID).FirstOrDefault();
            RoditeljUrediVM vm = new RoditeljUrediVM() {
                DatumRodenja = roditelj.DatumRodenja,
                Email = roditelj.Email,
                Ime = roditelj.Ime,
                Prezime =  roditelj.Prezime,
                Password = roditelj.KorisnickiNalog.Password,
                Username = roditelj.KorisnickiNalog.Username,
                RoditeljID = roditelj.KorisnikId,
                Telefon=roditelj.Telefon

            };

            return View(vm);

        }
        public IActionResult Snimi(RoditeljUrediVM vm)
        {
            Korisnik roditelj = db._Korisnik.Include(r => r.KorisnickiNalog).Where(x => x.KorisnikId == vm.RoditeljID).FirstOrDefault();

            roditelj.DatumRodenja =vm.DatumRodenja;
            roditelj.Email = vm.Email;
            roditelj.Ime = vm.Ime;
            roditelj.Prezime = vm.Prezime;
            roditelj.KorisnickiNalog.Password = vm.Password;
            roditelj.KorisnickiNalog.Username = vm.Username;
            roditelj.Telefon = vm.Telefon;
            db._Korisnik.Update(roditelj);
            db.SaveChanges();
            

            return RedirectToAction("Index");

        }
    }
}