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
    public class RazrednikController : Controller
    {
        private MojContext _db;
        public RazrednikController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from O in _db._Odjeljenje
                        join N in _db._NastavniPlan on O.NastavniPlanId equals N.NastavniPlanId
                        join NS in _db._NastavniPlanPredmet on N.NastavniPlanId equals NS.NastavniPlanId
                        join A in _db._Angazovan on NS.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                        join G in _db._GodinaStudija on N.GodinaStudiranjaId equals G.GodinaStudijaId
                        where O.RazrednikId == Nastavnik.KorisnikId
                        orderby G.Godina descending
                        select new { O.OdjeljenjeId, O.Oznaka, G.Godina };

            List<OdjeljenjeVM> VM = query.Distinct().Select(x => new OdjeljenjeVM
            {
                OdjeljenjeID = x.OdjeljenjeId,
                Godina = x.Godina,
                Oznaka = x.Oznaka
            }).ToList();

            return View(VM);
        }

        public IActionResult Casovi(int OdjeljenjeID)
        {

            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from O in _db._OdrzanCas
                        join OD in _db._Odjeljenje on O.OdjeljenjeID equals OD.OdjeljenjeId
                        join NP in _db._NastavniPlan on OD.NastavniPlanId equals NP.NastavniPlanId
                        join NPP in _db._NastavniPlanPredmet on NP.NastavniPlanId equals NPP.NastavniPlanId
                        join P in _db._Predmet on NPP.PredmetId equals P.PredmetId
                        join A in _db._Angazovan on NPP.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                        where O.OdjeljenjeID == OdjeljenjeID
                        orderby O.DatumOdrzavanja descending
                        select new { P.Naziv, OD.Oznaka, O.OdjeljenjeID, O.OdrzanCasId, O.DatumOdrzavanja };

            List<OdrzaniCasIndexVM> VM = query.Select(x => new OdrzaniCasIndexVM
            {
                OdjeljenjeID = x.OdjeljenjeID,
                DatumOdrzavanja = x.DatumOdrzavanja,
                OdrzaniCasID = x.OdrzanCasId,
                Predmet = x.Naziv,
                OznakaOdjeljenja = x.Oznaka
            }).ToList();

            return View(VM);
        }

        public IActionResult Edit(int OdrzaniCasID, int OdjeljenjeID, string _Odjeljenje, string _Predmet)
        {
            var query = from O in _db._OdrzanCasDetalji
                        join U in _db._UpisUOdjeljenje on O.UpisUOdjeljenjeId equals U.UpisUOdjeljenjeId
                        join K in _db._Korisnik on U.UcenikId equals K.KorisnikId
                        where O.OdrzanCasId == OdrzaniCasID
                        let FullName = K.Ime + " " + K.Prezime
                        select new { U.BrojUDnevniku, FullName, O.OdrzanCasDetaljiId, O.Odsutan, O.Opravdano, O.Ocjena };

            OdrzaniCasEditVM VM = new OdrzaniCasEditVM()
            {
                Odjeljenje = _Odjeljenje,
                Predmet = _Predmet,
                OdrzaniCesEdit = query.Select(x => new OdrzaniCasEditVM.Rows
                {
                    BrojUDnevniku = x.BrojUDnevniku.ToString(),
                    Ime = x.FullName,
                    Ocjena = x.Ocjena ?? 0,
                    Odsutan = x.Odsutan,
                    OdrzaniCasDetaljiID = x.OdrzanCasDetaljiId,
                    Opravdano = x.Opravdano,
                    OdrzaniCasID = OdrzaniCasID,
                }).ToList()
            };


            return View(VM);
        }

        public IActionResult Vladanje(int OdjeljenjeID)
        {
            var query = from U in _db._Ucenik
                        join K in _db._Korisnik on U.KorisnikId equals K.KorisnikId
                        join UP in _db._UpisUOdjeljenje on U.KorisnikId equals UP.UcenikId
                        let FullName = K.Ime + " " + K.Prezime
                        where UP.OdjeljenjeId == OdjeljenjeID
                        select new { U.KorisnikId, FullName, U.Vladanje };

            List<VladanjeIndexVM> VM = query.Select(x => new VladanjeIndexVM
            {
                OdjeljenjeID = OdjeljenjeID,
                KorisnikID = x.KorisnikId,
                Ime = x.FullName,
                Vladanje = x.Vladanje
            }).ToList();

            return View(VM);
        }
    }
}