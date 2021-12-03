using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using eSchoolSemi.Web.Helper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class NastavniPlanController : Controller
    {
        private MojContext _context;
        public NastavniPlanController(MojContext context) => _context = context;

        public IActionResult Index(int id)
        {
            var trenutnaGodina = _context._GodinaStudija.First(x => x.TrenutnaGodina == true);

            NastavniPlanIndexVM Plan = new NastavniPlanIndexVM {

                GodineStudija=_context._GodinaStudija.Select(x=> new SelectListItem {
                    Value=x.GodinaStudijaId.ToString(),
                    Text=x.Godina
                }).ToList(),

                Planovi= _context._NastavniPlan.Where(x => x.GodinaStudiranjaId == trenutnaGodina.GodinaStudijaId).ToList()
        };

            if (id!=0)
            {
                Plan.Planovi = _context._NastavniPlan.Where(x => x.GodinaStudiranjaId == id).ToList();
            }

           
            return View(Plan);
        }

        public IActionResult DodajNastavniPlan()
        {

            NastavniPlanDodajVM NoviNastavniPlan = new NastavniPlanDodajVM();

               
            
            return PartialView(PopuniVm(NoviNastavniPlan));
        }

        [HttpPost]
        public IActionResult DodajNastavniPlan(NastavniPlanDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("DodajNastavniPlan", PopuniVm(vm));
            }

            NastavniPlan novi = new NastavniPlan {
                Naziv=vm.Naziv,
                GodinaStudiranjaId=vm.GodinaStudiranjaId,
                RazredId=vm.RazredId
            };

            _context._NastavniPlan.Add(novi);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult DodajPredmetUProgram(int id) {

            var plan = _context._NastavniPlan.First(x => x.NastavniPlanId == id);

            AnagazmanNaPredmet angazovanje = new AnagazmanNaPredmet
            {
                NastavniPlanProgramID= plan.NastavniPlanId,
                Naziv=plan.Naziv,

                predmet =_context._Predmet.Select(x=>new SelectListItem {

                    Value = x.PredmetId.ToString(),
                    Text = x.Naziv
                }).ToList(),

                nastavnik= _context._Nastavnik.Select(x => new SelectListItem
                {

                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime+" "+x.Prezime
                }).ToList()

            };


            return PartialView(angazovanje);


        }

        public IActionResult SnimiPlanPredmet(AnagazmanNaPredmet viewModel) {

            if (!ModelState.IsValid) {

                viewModel.Naziv = _context._NastavniPlan.First(x => x.NastavniPlanId == viewModel.NastavniPlanProgramID).Naziv;

                viewModel.predmet = _context._Predmet.Select(x => new SelectListItem
                {

                    Value = x.PredmetId.ToString(),
                    Text = x.Naziv
                }).ToList();

                viewModel.nastavnik = _context._Nastavnik.Select(x => new SelectListItem
                {

                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }).ToList();

                return View("DodajPredmetUProgram", viewModel);
            }

            NastavniPlanPredmet noviNastavniPredmet = new NastavniPlanPredmet {

                
                BrojCasova=viewModel.BrojCasova,
                NastavniPlanId=viewModel.NastavniPlanProgramID,
                PredmetId=viewModel.PredmetID

            };

            _context._NastavniPlanPredmet.Add(noviNastavniPredmet);


            Angazovan angazovanNastavnik = new Angazovan
            {

                NastavnikId = viewModel.NastavnikID,
                NastavniPlanPredmetId = noviNastavniPredmet.NastavniPlanPredmetId
            };

            _context._Angazovan.Add(angazovanNastavnik);

            _context.SaveChanges();


            return RedirectToAction("Detalji",new { nastavniPlanId= viewModel.NastavniPlanProgramID });
        }

     


            //Koje sve predmete ima nastavni plan i program
            public IActionResult Detalji(int nastavniPlanId)
        {
            var nastavniPlan = _context._NastavniPlan.FirstOrDefault(x => x.NastavniPlanId == nastavniPlanId);

            ListaPredmetaNastavniPlanVM temp = new ListaPredmetaNastavniPlanVM {

                Naziv = nastavniPlan.Naziv,
                NastavniPlanId = nastavniPlanId,
                GodinaStudijaId = nastavniPlan.GodinaStudiranjaId,
                prebacen = nastavniPlan.Prebacen
              
        };

            

            List<NastavniPlanPredmet> nastavniPredmeti = _context._NastavniPlanPredmet.Where(x => x.NastavniPlanId == nastavniPlanId).ToList();
                     

            temp.Angazovani = new List<ListaPredmetaNastavniPlanVM.Row>();

            foreach (var item in nastavniPredmeti)
            {
                temp.Angazovani.Add
                (
                    _context._Angazovan.Where(x => x.NastavniPlanPredmetId == item.NastavniPlanPredmetId).Select
                    (x => new ListaPredmetaNastavniPlanVM.Row
                    {
                        AngazovanID = x.AngazovanId,
                        NazivPredmeta = x.NastavniPlanPredmet.Predmet.Naziv,
                        BrojCasova = x.NastavniPlanPredmet.BrojCasova,
                        NazivNastavnika = (x.Nastavnik != null) ? x.Nastavnik.Ime + " " + x.Nastavnik.Prezime : "Nastavnik izbrisan"

                    }).First()
                
                );
            }

           



            return View(temp);
        }


        

        public IActionResult ObrisiNastavniPlan(int nastavniPlanId)
        {
            //Brisanje citavog nastavnog programa sto znaci sve predmete na njemu sve azurirane nastavnike na tim predmetima i nulliranje 
            //OdljenjaID tako da ne pravi problem prilikom brisanja(sada bit trebao dodati uredivanja odljenja sto se tice nastavnog plana)
            NastavniPlan nastavniPlan = _context._NastavniPlan.FirstOrDefault(x => x.NastavniPlanId == nastavniPlanId);

            if (nastavniPlan == null) { return Content("Nema tog nastavnog plana"); }

            List<NastavniPlanPredmet> predmeti = _context._NastavniPlanPredmet.Where(x => x.NastavniPlanId == nastavniPlan.NastavniPlanId).ToList();

            if (predmeti !=null)
            {
                List<Angazovan> nastavnici = new List<Angazovan>();
                foreach (var item in predmeti)
                {
                    nastavnici.Add(_context._Angazovan.FirstOrDefault(x => x.NastavniPlanPredmetId == item.NastavniPlanPredmetId));
                }

                if (nastavnici!=null)
                {
                    foreach (var item in nastavnici)
                    {
                        if(item!=null)
                        _context._Angazovan.Remove(_context._Angazovan.Find(item.AngazovanId));
                        
                    }
                    _context.SaveChanges();
                }

                foreach (var item in predmeti)
                {
                    _context._NastavniPlanPredmet.Remove(_context._NastavniPlanPredmet.Find(item.NastavniPlanPredmetId));
                }
                _context.SaveChanges();
            }
            
            List<Odjeljenje> Odljeljnja = _context._Odjeljenje.Where(x => x.NastavniPlanId == nastavniPlanId).ToList();
            if (Odljeljnja != null) {
                foreach (var item in Odljeljnja)
                {
                    _context._Odjeljenje.Find(item.OdjeljenjeId).NastavniPlanId = null;

                }
                _context.SaveChanges();
            }
           

            _context._NastavniPlan.Remove(_context._NastavniPlan.Find(nastavniPlanId));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult ObrisiPredmet(int id) {

            Angazovan obrisiAngazovanog = _context._Angazovan.FirstOrDefault(x => x.AngazovanId == id);

            NastavniPlanPredmet obrisiPlanPredmet = _context._NastavniPlanPredmet.FirstOrDefault(x => x.NastavniPlanPredmetId == obrisiAngazovanog.NastavniPlanPredmetId);

            int NastavniPlan = obrisiPlanPredmet.NastavniPlanId??0;

            _context._NastavniPlanPredmet.Remove(obrisiPlanPredmet);

            _context._Angazovan.Remove(obrisiAngazovanog);
            _context.SaveChanges();

            return RedirectToAction("Detalji",new { nastavniPlanId = NastavniPlan });
        }


        public NastavniPlanDodajVM PopuniVm(NastavniPlanDodajVM vm) {

            var godinaStudija = from x in _context._GodinaStudija
                                where x.TrenutnaGodina == true
                                select x;

            vm.GodinaStudiranja = godinaStudija.Select(x => new SelectListItem
            {
                Value = x.GodinaStudijaId.ToString(),
                Text = x.Godina

            }).ToList();

            vm.Razred = _context.Razred.Select(x => new SelectListItem
            {
                Value = x.RazredId.ToString(),
                Text = x.OpisRazreda

            }).ToList();

            return vm;


        }

        public IActionResult Prebaci(int NastavniPlanId) {

            

            var nastavniPlan = _context._NastavniPlan.First(x => x.NastavniPlanId == NastavniPlanId);

            var stariNastavniPlan = _context._NastavniPlan.FirstOrDefault(x => x.RazredId==nastavniPlan.RazredId&&x.GodinaStudiranjaId==nastavniPlan.GodinaStudiranjaId-1);

            if (stariNastavniPlan!=null) { 
            var Nastavnici = from x in _context._Angazovan
                             join s in _context._NastavniPlanPredmet on x.NastavniPlanPredmetId equals s.NastavniPlanPredmetId
                             where s.NastavniPlanId == stariNastavniPlan.NastavniPlanId
                             select new { x.NastavnikId,s.PredmetId,s.BrojCasova };
            foreach (var item in Nastavnici)
            {
                NastavniPlanPredmet noviNpp = new NastavniPlanPredmet {
                    NastavniPlanId = NastavniPlanId,
                    PredmetId=item.PredmetId,
                    BrojCasova=item.BrojCasova
                    
                };

                _context._NastavniPlanPredmet.Add(noviNpp);

                Angazovan angazovan = new Angazovan {
                    NastavniPlanPredmetId=noviNpp.NastavniPlanPredmetId,
                    NastavnikId=item.NastavnikId
                };

                _context._Angazovan.Add(angazovan);

                    nastavniPlan.Prebacen = true;
                    _context.Update(nastavniPlan);
            }

            _context.SaveChanges();
            }

            return RedirectToAction("Detalji", new { nastavniPlanId = NastavniPlanId });
           
        }


        public IActionResult ProvjeriZvanje(int NastavnikID, int PredmetID,int NastavniPlanProgramID) {

            var Nastavnik = _context._Nastavnik.First(x => x.KorisnikId == NastavnikID).Zvanje;
            var Predmet = _context._Predmet.FirstOrDefault(x => x.PredmetId == PredmetID);

            var predmetNaziv = Predmet?.Naziv;


            var Provjera = from x in _context._NastavniPlanPredmet
                           join s in _context._Angazovan on x.NastavniPlanPredmetId equals s.NastavniPlanPredmetId
                           where x.NastavniPlanId == NastavniPlanProgramID && x.PredmetId == PredmetID && s.NastavnikId == NastavnikID
                           select x.NastavniPlanPredmetId;

            if (Provjera.Any()) {

                return Json("Taj nastavnik je vec upisan u plan i program!");
            }


            bool provjera = Nastavnik.Equals(predmetNaziv);

            if (!provjera) {

                return Json("Taj nastavnik ne predaje taj predmet!");
            }

            return Json(true);            

        }


        public IActionResult ProvjeriPlan(int RazredId, int GodinaStudiranjaId) {

            var NastavniPlan = _context._NastavniPlan.FirstOrDefault(x => x.RazredId == RazredId && x.GodinaStudiranjaId == GodinaStudiranjaId);

            if (NastavniPlan!=null)
            {
                return Json("Vec postoji nastavni plan za taj razred u toj skolskoj godini!");
            }

            return Json(true);
        }
    }
}