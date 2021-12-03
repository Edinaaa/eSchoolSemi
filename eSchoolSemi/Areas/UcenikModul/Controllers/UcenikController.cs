using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.UcenikModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.UcenikModul.Controllers
{
    [Area("UcenikModul")]
    [Autorizacija(true, false, false, false)]
    public class UcenikController : Controller
    {
        private MojContext db;
        public UcenikController(MojContext _db) { db = _db; }
        public IActionResult Index()
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Korisnik Ucenik = db._Korisnik.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).Include(r => r.KorisnickiNalog).FirstOrDefault();
            UcenikIndexVM vm = new UcenikIndexVM()
            {
                DatumRodenja = Ucenik.DatumRodenja,
                Email = Ucenik.Email,
                ImePrezime = Ucenik.Ime + " " + Ucenik.Prezime,
                Password = Ucenik.KorisnickiNalog.Password,
                Username = Ucenik.KorisnickiNalog.Username,
                UcenikID = Ucenik.KorisnikId,
                Telefon=Ucenik.Telefon

            };
            return View(vm);

        }
        public IActionResult Uredi(int UcenikID)
        {
            Korisnik Ucenik = db._Korisnik.Include(r => r.KorisnickiNalog).Where(x => x.KorisnikId == UcenikID).FirstOrDefault();
            UcenikUrediVM vm = new UcenikUrediVM()
            {
                DatumRodenja = Ucenik.DatumRodenja,
                Email = Ucenik.Email,
                Ime = Ucenik.Ime,
                Prezime = Ucenik.Prezime,
                Password = Ucenik.KorisnickiNalog.Password,
                Username = Ucenik.KorisnickiNalog.Username,
                UcenikID = Ucenik.KorisnikId,
                Telefon=Ucenik.Telefon
            };

            return View(vm);

        }
        public IActionResult Snimi(UcenikUrediVM vm)
        {
            Korisnik Ucenik = db._Korisnik.Include(r => r.KorisnickiNalog).Where(x => x.KorisnikId == vm.UcenikID).FirstOrDefault();

            Ucenik.DatumRodenja = vm.DatumRodenja;
            Ucenik.Email = vm.Email;
            Ucenik.Ime = vm.Ime;
            Ucenik.Prezime = vm.Prezime;
            Ucenik.KorisnickiNalog.Password = vm.Password;
            Ucenik.KorisnickiNalog.Username = vm.Username;
            Ucenik.Telefon = vm.Telefon;
            db._Korisnik.Update(Ucenik);
            db.SaveChanges();


            return RedirectToAction("Index");

        }
    }
}