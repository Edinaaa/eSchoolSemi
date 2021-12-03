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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.UcenikModul.Controllers
{
    [Area("UcenikModul")]
    [Autorizacija(true, false, false, false)]
    public class RasporedController : Controller
    {
        private MojContext db;
        public RasporedController(MojContext _db) { db = _db; }
        public IActionResult Index()
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Korisnik ucenik = db._Korisnik.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).FirstOrDefault();

        
            UpisUOdjeljenje uuo = db._UpisUOdjeljenje
             .Include(o => o.Odjeljenje).Include(n => n.Odjeljenje.NastavniPlan).Include(g => g.Odjeljenje.GodinaStudija)
             .Where(y => y.UcenikId == ucenik.KorisnikId
         && y.Odjeljenje.GodinaStudija.TrenutnaGodina).FirstOrDefault();

            RasporedIndexVM vm = new RasporedIndexVM()
            {
                Odjeljenje = uuo.Odjeljenje.Oznaka,
                Dan = db.Dan.Select(x => new SelectListItem() { Value = x.DanID.ToString(), Text = x.Naziv }).ToList(),
                PocetankCasa = db.PocetakCasa.Select(x => new SelectListItem() { Value = x.PocetakCasaID.ToString(), Text = x.Pocetak }).ToList(),

                Rows = db.RasporedDetalj.Include(d => d.Dan).Include(p => p.PocetakCasa).Include(pr => pr.Predmet).Where(r => r.Raspored.OdjeljenjeID == uuo.OdjeljenjeId).Select(x => new RasporedIndexVM.Row()
                {
                    Dan = x.Dan.Naziv,
                    DanId = x.DanID.ToString(),
                    PocetakCasaID = x.PocetakCasaId.ToString(),
                    PocetakCasa = x.PocetakCasa.Pocetak,
                    Predmet = x.Predmet.Naziv

                }).ToList()
            };
            return View(vm);
        }
     
    }
}