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
    public class NastavnikController : Controller
    {
        private MojContext _db;
        public NastavnikController(MojContext db)
        {
            _db = db;
        }

        public int UcenikIndexVM { get; private set; }

        public IActionResult Index()
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
                        select new { O.OdjeljenjeId, O.Oznaka, G.Godina };

            List<OdjeljenjeVM> VM = query.Select(x => new OdjeljenjeVM
            {
                OdjeljenjeID = x.OdjeljenjeId,
                Godina = x.Godina,
                Oznaka = x.Oznaka
            }).ToList();

            return View(VM);
        }

        public IActionResult NoviCas(int ID)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var predmeti = from P in _db._Predmet
                           join NPP in _db._NastavniPlanPredmet on P.PredmetId equals NPP.PredmetId
                           join NP in _db._NastavniPlan on NPP.NastavniPlanId equals NP.NastavniPlanId
                           join A in _db._Angazovan on NPP.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                           join O in _db._Odjeljenje on NP.NastavniPlanId equals O.NastavniPlanId
                           where A.NastavnikId == Nastavnik.KorisnikId
                           select new { P.PredmetId, P.Naziv };


            OdrzaniCasDodajIndexVM VM = new OdrzaniCasDodajIndexVM
            {
                OdjeljenjeID = ID,
                Predmeti = predmeti.Distinct().Select(x => new SelectListItem { Value = x.PredmetId.ToString(), Text = x.Naziv }).ToList(),
                DatumOdrzavanja = DateTime.Now
            };

            return View(VM);
        }

        public IActionResult Snimi(OdrzaniCasDodajIndexVM VM) {
            OdrzanCas O = new OdrzanCas
            {
                DatumOdrzavanja = VM.DatumOdrzavanja,
                OdjeljenjeID = VM.OdjeljenjeID,
                NastavniPlanPredmetId = _db._NastavniPlanPredmet.Where(x => x.PredmetId == VM.PredmetID).First().NastavniPlanPredmetId
            };

            _db._OdrzanCas.Add(O);

            var query = from UO in _db._UpisUOdjeljenje
                        where UO.OdjeljenjeId == O.OdjeljenjeID
                        select new { UO.UpisUOdjeljenjeId };

            List<OdrzanCasDetalji> OD = query.Select(y => new OdrzanCasDetalji {
                Ocjena = 0,
                OdrzanCasId = O.OdrzanCasId,
                UpisUOdjeljenjeId = y.UpisUOdjeljenjeId,
                Odsutan = false,
                Opravdano = false
            }).ToList();
            _db._OdrzanCasDetalji.AddRange(OD);

            _db.SaveChanges();

            return RedirectToAction("OdrzaniCasDetalji", "Nastavnik", new { _OdjeljenjeID = O.OdjeljenjeID, _DatumOdrzavanja = O.DatumOdrzavanja, _OdrzaniCasID = O.OdrzanCasId });
        }

        public IActionResult OdrzaniCasDetalji(int _OdjeljenjeID, DateTime _DatumOdrzavanja, int _OdrzaniCasID)
        {
            var query = from UO in _db._UpisUOdjeljenje
                        join OD in _db._OdrzanCasDetalji on UO.UpisUOdjeljenjeId equals OD.UpisUOdjeljenjeId
                        join U in _db._Ucenik on UO.UcenikId equals U.KorisnikId
                        join K in _db._Korisnik on U.KorisnikId equals K.KorisnikId
                        let FullName = K.Ime + " " + K.Prezime
                        where UO.OdjeljenjeId == _OdjeljenjeID
                        where OD.OdrzanCasId == _OdrzaniCasID
                        select new { OD.OdrzanCasDetaljiId, UO.BrojUDnevniku, UO.UpisUOdjeljenjeId, FullName };

            List<OdrzaniCasDetaljiIndexVM> VM = query.Select(x => new OdrzaniCasDetaljiIndexVM
            {
                OdjeljenjeID = _OdjeljenjeID,
                DatumOdrzavanja = _DatumOdrzavanja,
                OdrzaniCasDetaljiID = x.OdrzanCasDetaljiId,
                UpisUOdjeljenjeID = x.UpisUOdjeljenjeId,
                Ucenik = x.FullName,
                Ocjena = 0,
                Odsutan = false,
                OdrzaniCasID = _OdjeljenjeID,
                Opravdano = false,
                BrojUDnevniku = x.BrojUDnevniku.ToString()
            }).ToList();

            //OdrzaniCasDetaljiIndexVM VM = new OdrzaniCasDetaljiIndexVM
            //{
            //    DatumOdrzavanja = _DatumOdrzavanja,
            //    Ucenici = query.Select(x => new string(x.FullName.ToCharArray())).ToList(),
            //    Odsutan = false,
            //    OdjeljenjeID = _OdjeljenjeID,
            //    UpisUOdjeljenjeID = query.Select(x => new UpisUOdjeljenje { UpisUOdjeljenjeId = x.UpisUOdjeljenjeId, BrojUDnevniku = x.BrojUDnevniku }).ToList(),
                
            //};
            
            return View(VM);
        }
    }
}