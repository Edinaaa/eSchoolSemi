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
    public class OdrzaniCasController : Controller
    {
        private MojContext _db;
        public OdrzaniCasController(MojContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from O in _db._OdrzanCas
                        join OD in _db._Odjeljenje on O.OdjeljenjeID equals OD.OdjeljenjeId
                        join NP in _db._NastavniPlan on OD.NastavniPlanId equals NP.NastavniPlanId
                        join NPP in _db._NastavniPlanPredmet on NP.NastavniPlanId equals NPP.NastavniPlanId
                        join P in _db._Predmet on NPP.PredmetId equals P.PredmetId
                        join A in _db._Angazovan on NPP.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                        where A.NastavnikId == Nastavnik.KorisnikId
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

        public IActionResult Brisi(int OdrzaniCasID) {

            var query = from OD in _db._OdrzanCasDetalji
                        where OD.OdrzanCasId == OdrzaniCasID
                        select new { OD.OdrzanCasDetaljiId, OD.Ocjena, OD.OdrzanCasId, OD.Odsutan, OD.Opravdano, OD.UpisUOdjeljenjeId };

            List<OdrzanCasDetalji> OCD = query.Select(x => new OdrzanCasDetalji
            {
                Ocjena = x.Ocjena,
                UpisUOdjeljenjeId = x.UpisUOdjeljenjeId,
                Opravdano = x.Opravdano,
                Odsutan = x.Odsutan,
                OdrzanCasId = x.OdrzanCasId,
                OdrzanCasDetaljiId = x.OdrzanCasDetaljiId
            }).ToList();

            _db._OdrzanCasDetalji.RemoveRange(OCD);

            OdrzanCas O = _db._OdrzanCas.Find(OdrzaniCasID);
            _db._OdrzanCas.Remove(O);

            _db.SaveChanges();

            return RedirectToAction("Index", "OdrzaniCas");
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
        

            //List<OdrzaniCasEditVM> VM2 = query.Select(x => new OdrzaniCasEditVM
            //{
            //    BrojUDnevniku = x.BrojUDnevniku.ToString(),
            //    Ime = x.FullName,
            //    Ocjena = x.Ocjena ?? 0,
            //    Odsutan = x.Odsutan,
            //    OdrzaniCasDetaljiID = x.OdrzanCasDetaljiId,
            //    Opravdano = x.Opravdano,
            //    OdrzaniCasID = OdrzaniCasID,
            //    Odjeljenje = _Odjeljenje,
            //    Predmet = _Predmet
            //}).ToList();


            return View(VM);
        }
    }
}