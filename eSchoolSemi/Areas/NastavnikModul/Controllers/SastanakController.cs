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
    public class SastanakController : Controller
    {
        private MojContext _db;
        public SastanakController(MojContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var query = from S in _db._Sastanak
                        join O in _db._Odjeljenje on S.OdjeljenjeId equals O.OdjeljenjeId
                        select new { S.SastanakId, S.Naziv, O.Oznaka };

            List<SastanakIndexVM> VM = query.Select(x => new SastanakIndexVM
            {
                Naziv = x.Naziv,
                SastanakID = x.SastanakId,
                OznakaOdjeljenje = x.Oznaka
            }).ToList();

            return View(VM);
        }

        public IActionResult Detalji(int SastanakID)
        {
            Sastanak S = _db._Sastanak.Find(SastanakID);

            SastanakDodajVM VM = new SastanakDodajVM
            {
                Naziv = S.Naziv,
                Opis = S.Opis
            };

            return View(VM);
        }


        public IActionResult Dodaj()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from O in _db._Odjeljenje
                        join N in _db._NastavniPlan on O.NastavniPlanId equals N.NastavniPlanId
                        join NS in _db._NastavniPlanPredmet on N.NastavniPlanId equals NS.NastavniPlanId
                        join A in _db._Angazovan on NS.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                        join G in _db._GodinaStudija on N.GodinaStudiranjaId equals G.GodinaStudijaId
                        where A.NastavnikId == Nastavnik.KorisnikId
                        orderby G.Godina descending
                        select new { O.OdjeljenjeId, O.Oznaka };

            SastanakDodajVM VM = new SastanakDodajVM
            {
                Odjeljenja = query.Select(x=> new SelectListItem { Value = x.OdjeljenjeId.ToString(), Text = x.Oznaka }).ToList(),
                SastanakTip=_db._SastanakTip.Select(x => new SelectListItem { Value = x.SastanakTipId.ToString(), Text = x.Naziv }).ToList()
            };
            return View(VM);
        }

        public IActionResult Snimi(SastanakDodajVM VM)
        {
            Sastanak S = new Sastanak
            {
                DatumObavijest = DateTime.Now,
                Naziv = VM.Naziv,
                OdjeljenjeId = VM.OdjeljenjeID,
                Opis = VM.Opis,
              
                SastanakTipId=VM.SastanakTipID
            };

            _db._Sastanak.Add(S);
            _db.SaveChanges();

            return RedirectToAction("Index", "Sastanak");
        }

        public IActionResult Brisi(int SastanakID)
        {
            Sastanak S = _db._Sastanak.Find(SastanakID);

            _db._Sastanak.Remove(S);
            _db.SaveChanges();

            return RedirectToAction("Index", "Sastanak");
        }
    }
}