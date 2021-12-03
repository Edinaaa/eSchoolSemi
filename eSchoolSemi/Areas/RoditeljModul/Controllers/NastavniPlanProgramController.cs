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
    public class NastavniPlanProgramController : Controller
    {
        private MojContext db { get; set; }
      public  NastavniPlanProgramController(MojContext mojContext ) { db = mojContext; }
        public IActionResult Index()
        {

            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Korisnik roditelj = db._Korisnik.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID)
                .Include(r => r.KorisnickiNalog).FirstOrDefault();
           
            NastavniPlanProgramIndexVM vm = new NastavniPlanProgramIndexVM() {

                Rows= db._UpisUOdjeljenje
                .Include(o=>o.Odjeljenje).Include(n=>n.Odjeljenje.NastavniPlan)
                .Include(u=>u.Ucenik).Include(g=>g.Odjeljenje.GodinaStudija)
                .Include(on=>on.Odjeljenje.Razrednik)
                .Where(x => x.Ucenik.RoditeljId == roditelj.KorisnikId)
                .Select(x=> new NastavniPlanProgramIndexVM.Row() {

                    UcenikId=x.UcenikId,
                    ImePrezime=x.Ucenik.Ime+" "+x.Ucenik.Prezime,
                    RazredikID=(int)x.Odjeljenje.RazrednikId,
                    ImePrezimeRazredika=x.Odjeljenje.RazrednikId==null?"Razrednik jos nije dodjeljen" :  x.Odjeljenje.Razrednik.Ime+" "+x.Odjeljenje.Razrednik.Prezime,
                    NastavniPlanID = (int)x.Odjeljenje.NastavniPlanId,
                    GodinaStudija=x.Odjeljenje.GodinaStudija.Godina,
                    Naziv=x.Odjeljenje.NastavniPlan.Naziv,
                    Odjeljenje=x.Odjeljenje.Oznaka,
                   


                }).ToList()

            };

            return View(vm);
        }


        public IActionResult Detalji(int NastavniPlanID)
        {
            
            NastavniPlanProgramDetaljiVM vm = new NastavniPlanProgramDetaljiVM()
            {

                Rows = db._Angazovan
                .Include(npp=>npp.NastavniPlanPredmet)
                .Include(p=>p.NastavniPlanPredmet.Predmet).Include(n=>n.Nastavnik)
                .Where(x => x.NastavniPlanPredmet.NastavniPlanId==NastavniPlanID)
                .Select(x => new NastavniPlanProgramDetaljiVM.Row()
                {
                   Predmet=x.NastavniPlanPredmet.Predmet.Naziv,
                   BrojCasova=x.NastavniPlanPredmet.BrojCasova,
                   Oznaka=x.NastavniPlanPredmet.Predmet.Oznaka,
                   ImePrezimeNastavnika=x.NastavnikId==null?"Nastavnik nije dodjeljen":x.Nastavnik.Ime+" "+x.Nastavnik.Prezime,
                   NastavnikID=(int)x.NastavnikId


                }).ToList()

            };

            return PartialView(vm);
        }
        public IActionResult NastavnikDetalji(int NastavnikID)
        {
            Nastavnik n = db._Nastavnik.Where(x => x.KorisnikId == NastavnikID).FirstOrDefault();
            Angazovan a = db._Angazovan
                .Include(npp => npp.NastavniPlanPredmet)
                .Include(p => p.NastavniPlanPredmet.Predmet)
                .Include(np => np.NastavniPlanPredmet.NastavniPlan)
                .Include(g => g.NastavniPlanPredmet.NastavniPlan.GodinaStudija)
                .Where(x => x.NastavnikId == n.KorisnikId && x.NastavniPlanPredmet.NastavniPlan.GodinaStudija.TrenutnaGodina)
                .FirstOrDefault();
            NastavniPlanProgramNastavnikVM vm = new NastavniPlanProgramNastavnikVM()
            {
                NastavnikID = n.KorisnikId,
                ImePrezime = n.Ime + " " + n.Prezime,
                DatumZaposlenja = n.DatumZaposlenja,
                Titula = n.Titula,
                Zvanje = n.Zvanje,
                PredajePredmet =a.NastavniPlanPredmet.Predmet.Naziv,
                OdjeljenjeRazredik = db._Odjeljenje.Include(x=>x.GodinaStudija).Where(x => x.RazrednikId == n.KorisnikId && x.GodinaStudija.TrenutnaGodina).FirstOrDefault().Oznaka

              

            };

            return PartialView(vm);
        }
    }
}