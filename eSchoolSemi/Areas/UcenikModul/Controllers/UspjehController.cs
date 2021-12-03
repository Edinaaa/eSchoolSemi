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
    public class UspjehController : Controller
    {
        private MojContext db;
        public UspjehController(MojContext _db) { db = _db; }
        public IActionResult Index()
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Korisnik Ucenik = db._Korisnik.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).FirstOrDefault();
            UpisUOdjeljenje uuo = db._UpisUOdjeljenje
               .Include(o => o.Odjeljenje).Include(n => n.Odjeljenje.NastavniPlan).Include(g => g.Odjeljenje.GodinaStudija)
               .Where(y => y.UcenikId == Ucenik.KorisnikId
           && y.Odjeljenje.GodinaStudija.TrenutnaGodina).FirstOrDefault();

            UspjehIndexVM vm = new UspjehIndexVM()
            {
                UcenikID = Ucenik.KorisnikId,
                ImePrezime = Ucenik.Ime + " " + Ucenik.Prezime,
                UkupnoProsjek = db._OdrzanCasDetalji.Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Average(a => a.Ocjena),
                UkupnoBrojIzostanaka = db._OdrzanCasDetalji.Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(a => a.Odsutan),

                UkupnoBrojOpravdanih = db._OdrzanCasDetalji.Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(a => a.Opravdano),

                UkupnoBrojNeOpravdanih = db._OdrzanCasDetalji.Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(a => a.Odsutan)
                - db._OdrzanCasDetalji.Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(a => a.Opravdano),

                Rows = db._NastavniPlanPredmet.Where(y => y.NastavniPlanId == uuo.Odjeljenje.NastavniPlanId)
                .Include(p => p.Predmet)
                .Select(x => new UspjehIndexVM.Row()
                {
                    Predmet = x.Predmet.Naziv,
                    PredmetID = x.PredmetId == null ? 0 : (int)x.PredmetId,
                    Prosjek = db._OdrzanCasDetalji
                    .Where(y => y.OdrzanCas.NastavniPlanPredmetId == x.NastavniPlanPredmetId && y.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Average(s => s.Ocjena),
                    BrojIzostanaka = db._OdrzanCasDetalji
                    .Where(y => y.OdrzanCas.NastavniPlanPredmetId == x.NastavniPlanPredmetId && y.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(s => s.Odsutan),
                    BrojOpravdanih = db._OdrzanCasDetalji
                    .Where(y => y.OdrzanCas.NastavniPlanPredmetId == x.NastavniPlanPredmetId && y.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(s => s.Opravdano),

                    BrojNeOpravdanih = db._OdrzanCasDetalji
                    .Where(y => y.OdrzanCas.NastavniPlanPredmetId == x.NastavniPlanPredmetId && y.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(s => s.Odsutan)
                    - db._OdrzanCasDetalji
                    .Where(y => y.OdrzanCas.NastavniPlanPredmetId == x.NastavniPlanPredmetId && y.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId).Count(s => s.Opravdano),
                }).ToList()
            };
            return View(vm);
        }
       
        public IActionResult UspjehPredmet(int PredmetId, int UcenikID)
        {
            Ucenik u = db._Ucenik.Where(x => x.KorisnikId == UcenikID).FirstOrDefault();
            UpisUOdjeljenje uuo = db._UpisUOdjeljenje
                .Include(o => o.Odjeljenje).Include(n => n.Odjeljenje.NastavniPlan).Include(g => g.Odjeljenje.GodinaStudija)
                .Where(y => y.UcenikId == UcenikID
            && y.Odjeljenje.GodinaStudija.TrenutnaGodina).FirstOrDefault();
            NastavniPlanPredmet npp = db._NastavniPlanPredmet
                .Include(p => p.Predmet)
                .Where(x => x.PredmetId == PredmetId && x.NastavniPlan.GodinaStudiranjaId == uuo.Odjeljenje.GodinaStudijaId).FirstOrDefault();
            ZakljucnaOcjena zakljucna = db.ZakljucnaOcjena.Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId && x.PredmetID == PredmetId).FirstOrDefault();
            UspjehPredmetVM vm = new UspjehPredmetVM()
            {
                UcenikID = u.KorisnikId,
                ImePrezime = u.Ime + " " + u.Prezime,
                Predmet = npp.Predmet.Naziv,
                PredmetID = (int)npp.PredmetId,
                Zakljucena = zakljucna == null ? false : true,
                Zakljucna = zakljucna == null ? 0 : zakljucna.Ocjena,
                //Odrzani casovi detalji za odredzenog ucenika uuo( koji je upisan u odredzeno odjeljenje i slusa odredzenu godinu studija po odredzenom nastavnom planu)
                //za kojeg su se odrzali casovi iz odredzenog predmeta npp.
                Rows = db._OdrzanCasDetalji
                .Where(x => x.UpisUOdjeljenjeId == uuo.UpisUOdjeljenjeId && x.OdrzanCas.NastavniPlanPredmetId == npp.NastavniPlanPredmetId)
                .Select(z => new UspjehPredmetVM.Row()
                {
                    DatumOcjenjivanja = z.OdrzanCas.DatumOdrzavanja,
                    Ocjena = (int)z.Ocjena


                }).ToList()
            };
            return PartialView(vm);
        }
      
    }
}