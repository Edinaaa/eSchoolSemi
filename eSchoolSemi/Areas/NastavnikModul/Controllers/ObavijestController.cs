using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.NastavnikModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.NastavnikModul.Controllers
{
    [Area("NastavnikModul")]
    [Autorizacija(false, true, false, false)]
    public class ObavijestController : Controller
    {
        private MojContext _db;
        public ObavijestController(MojContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var query = from O in _db._Obavjestenje
                        join K in _db._Korisnik on O.NastavnikID equals K.KorisnikId
                        join T in _db._TipObavijesti on O.TipObavijestiId equals T.TipObavijestiId
                        orderby O.DatumObavjestenja descending
                        let FullName = K.Ime + " " + K.Prezime
                        select new { FullName, O.ObavjestenjeId, O.Naslov, O.DatumObavjestenja, T.Tip };

            List<ObavijestIndexVM> VM = query.Select(x => new ObavijestIndexVM
            {
                ObavjestID = x.ObavjestenjeId,
                Tip = x.Tip,
                DatumObjave = x.DatumObavjestenja,
                ImeAutora = x.FullName,
                Naslov = x.Naslov
            }).ToList();

            return View(VM);
        }

        public IActionResult Detalji(int ObavijestID) {
            Obavjestenje O = _db._Obavjestenje.Find(ObavijestID);

            ObavijestIndexVM VM = new ObavijestIndexVM
            {
                DatumObjave = O.DatumObavjestenja,
                ObavjestID = O.ObavjestenjeId,
                Naslov = O.Naslov,
                Sadrzaj = O.Sadrzaj
            };

            return View(VM);
        }

        public IActionResult Dodaj() {

            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from T in _db._TipObavijesti
                        select new { T.Tip, T.TipObavijestiId };

            ObavijestiDodajVM VM = new ObavijestiDodajVM
            {
                DatumObavjeljivanje = DateTime.Now,
                NastavnikID = Nastavnik.KorisnikId,
                Tip = query.Select(x => new SelectListItem { Value = x.TipObavijestiId.ToString(), Text = x.Tip }).ToList()
            };

            return View(VM);
        }

        public IActionResult Snimi(ObavijestiDodajVM VM) {
            Obavjestenje O = new Obavjestenje {
                NastavnikID = VM.NastavnikID,
                Naslov = VM.Naslov,
                TipObavijestiId = VM.TipID,
                Sadrzaj = VM.Sadrzaj,
                DatumObavjestenja = DateTime.Now
            };

            _db._Obavjestenje.Add(O);
            _db.SaveChanges();

            return RedirectToAction("Index","Obavijest");
        }
        public IActionResult Brisi(int ObavijestID)
        {
            Obavjestenje O = _db._Obavjestenje.Find(ObavijestID);
            _db.Remove(O);
            _db.SaveChanges();

            return RedirectToAction("Index", "Obavijest");
        }
    }
}