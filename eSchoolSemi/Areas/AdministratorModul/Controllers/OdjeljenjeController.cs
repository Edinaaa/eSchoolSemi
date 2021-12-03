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
    public class OdjeljenjeController : Controller
    {

        private MojContext _context;
        public OdjeljenjeController(MojContext context) => _context = context;


        

        public IActionResult Index()
        {

            FilterOdjeljenjeVM noviFilter = new FilterOdjeljenjeVM {

                GodineStudija=_context._GodinaStudija.Select(x=>new SelectListItem {

                    Value=x.GodinaStudijaId.ToString(),
                    Text=x.Godina
                }).ToList(),

                Razred = _context.Razred.Select(x => new SelectListItem
                {

                    Value = x.RazredId.ToString(),
                    Text = x.OpisRazreda
                }).ToList(),

                

                
            };



            return View(noviFilter);
        }

        public IActionResult DodajOdjeljenje()
        {
            OdjenjenjeVM vm = new OdjenjenjeVM();
            vm = GetDefaultVM(vm);
            vm.GodinaStudiranja = _context._GodinaStudija.Where(x => x.TrenutnaGodina == true).Select(x => new SelectListItem {
                Value = x.GodinaStudijaId.ToString(),
                Text=x.Godina
            }).ToList();

            return PartialView(vm);
        }


        public async Task<IActionResult> OdljenjenjeTabela(int godinaStudijaId, int? razredID,bool? imalRazrednika,bool? imalPredstvnika, int? page)
        {

            var odjeljenjeLista = from x in _context._Odjeljenje
                           join r in _context._Nastavnik on x.RazrednikId equals r.KorisnikId into ps
                           from r in ps.DefaultIfEmpty() 
                           join p in _context._Ucenik on x.UcenikID equals p.KorisnikId into pl
                           from p in pl.DefaultIfEmpty()
                           select new { x.OdjeljenjeId, x.Oznaka, x.Kapacitet, x.GodinaStudijaId, x.RazredID, x.RazrednikId,x.UcenikID,r,p };

            

            if (godinaStudijaId!=0)
            {
                odjeljenjeLista = odjeljenjeLista.Where(x => x.GodinaStudijaId == godinaStudijaId);
            }

            if (razredID != null)
            {
                odjeljenjeLista = odjeljenjeLista.Where(x => x.RazredID == razredID);
            }

            if (imalRazrednika != null) {
                odjeljenjeLista = (imalRazrednika ?? false) ? odjeljenjeLista.Where(x => x.RazrednikId != null) : odjeljenjeLista.Where(x => x.RazrednikId == null);
            }

            if (imalPredstvnika != null)
            {
                odjeljenjeLista = (imalPredstvnika ?? false) ? odjeljenjeLista.Where(x => x.UcenikID != null) : odjeljenjeLista.Where(x => x.UcenikID == null);
            }




            OdjeljenjeIndexVM listaOdjeljenja = new OdjeljenjeIndexVM
            {

                Odjeljenja = odjeljenjeLista.Select(x => new OdjeljenjeIndexVM.Row
                {

                    OdjeljenjeID = x.OdjeljenjeId,
                    Oznka = x.Oznaka,
                    Kapacitet = x.Kapacitet,
                    Razrednik = x.r.Ime + " " + x.r.Prezime,
                    Predstavnik = x.p.Ime + " " + x.p.Prezime,
                    GodinaStudija=_context._GodinaStudija.First(o=>o.GodinaStudijaId==x.GodinaStudijaId).Godina,
                    BrojUcenika=_context._UpisUOdjeljenje.Where(s=>s.OdjeljenjeId==x.OdjeljenjeId).Count()
                    
                    
                })
            };

            int pageSize = 15;
            return PartialView(await PaginatedList<OdjeljenjeIndexVM.Row>.CreateAsync(listaOdjeljenja.Odjeljenja.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Uredi(int id)
        {
            

            var odjeljenje = _context._Odjeljenje.Where(x => x.OdjeljenjeId == id).Include(x=>x.Razred).FirstOrDefault();

            OdjeljenjeUrediVm odjeljenjeUredi = new OdjeljenjeUrediVm {

                OdjeljenjeId = odjeljenje.OdjeljenjeId,
                Oznaka = odjeljenje.Oznaka,
                GodinaStudiranjaId=odjeljenje.GodinaStudijaId??0,
                Kapacitet = odjeljenje.Kapacitet,
                RazredId = odjeljenje.RazredID ?? 0,
                Razred = odjeljenje.Razred.OpisRazreda,
                NastavnikID = odjeljenje.RazrednikId ?? 0,
                PredstavnikId = odjeljenje.UcenikID ?? 0,
                NastavniPlanId = odjeljenje.NastavniPlanId ?? 0
            };

            
            
            


            return PartialView(PopuniVm(odjeljenjeUredi, odjeljenjeUredi.GodinaStudiranjaId));
        }

        public IActionResult SnimiUredjeno(OdjeljenjeUrediVm vm)
        {
            if (!ModelState.IsValid)
            {
                

                return View("Uredi", PopuniVm(vm,vm.GodinaStudiranjaId));
            }

            Odjeljenje novoOdljeljenje = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == vm.OdjeljenjeId);


            novoOdljeljenje.Oznaka = vm.Oznaka;
            novoOdljeljenje.Kapacitet = vm.Kapacitet;
            novoOdljeljenje.RazrednikId = vm.NastavnikID;
            if (vm.NastavniPlanId != 0)
            {
                novoOdljeljenje.NastavniPlanId = vm.NastavniPlanId;
            }
          
            if (vm.PredstavnikId != 0) { novoOdljeljenje.UcenikID = vm.PredstavnikId; }
             

            

            _context.Update(novoOdljeljenje);
            _context.SaveChanges();
         

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalji(int id)
        {
            var odjeljenje = _context._Odjeljenje.Where(x => x.OdjeljenjeId == id).FirstOrDefault();

            var raspored = _context.Raspored.FirstOrDefault(x => x.OdjeljenjeID == odjeljenje.OdjeljenjeId);

            OdjenjenjeVM odjeljenjeUredi = new OdjenjenjeVM
            {

                OdjeljenjeID = odjeljenje.OdjeljenjeId,
                Oznaka = odjeljenje.Oznaka,
                Kapacitet = odjeljenje.Kapacitet,
                RazredID = odjeljenje.RazredID ?? 0,
                NastavnikID = odjeljenje.RazrednikId ?? 0,
                NastavniPlanId = odjeljenje.NastavniPlanId ?? 0,
                GodinaStudiranjaId = odjeljenje.GodinaStudijaId ?? 0,
                
                
            };

            if (raspored!=null)
            {
                odjeljenjeUredi.RasporedId = raspored.RasporedID;
            }



            return View(GetDefaultVM(odjeljenjeUredi));
        }


        private OdjenjenjeVM GetDefaultVM(OdjenjenjeVM vm)
        {
            if (vm.Oznaka == null) {

                vm.Oznaka = "";
            }

            if (vm.Predstavnik == null && vm.OdjeljenjeID!=0) {

                var uceniciOdjeljenja = from x in _context._UpisUOdjeljenje
                                        join p in _context._Ucenik on x.UcenikId equals p.KorisnikId
                                        where x.OdjeljenjeId==vm.OdjeljenjeID
                                        select new { p.KorisnikId, p.Prezime, p.Ime };


                vm.Predstavnik = uceniciOdjeljenja.Select(x => new SelectListItem
                { Value = x.KorisnikId.ToString(), Text = x.Ime + " " + x.Prezime }).ToList();
            }



            if (vm.Razrednik==null)
            vm.Razrednik = _context._Nastavnik.Select(x => new SelectListItem
            {
                Value = x.KorisnikId.ToString(),
                Text = x.Ime + " " + x.Prezime
            }).ToList();

            if (vm.NastavniPlan==null)
            {
                vm.NastavniPlan = _context._NastavniPlan.Select(x => new SelectListItem
                {
                    Value = x.NastavniPlanId.ToString(),
                    Text = x.Naziv
                }).ToList();
            }

            if (vm.GodinaStudiranja == null)
            {
                vm.GodinaStudiranja = _context._GodinaStudija.Select(x => new SelectListItem
                {
                    Value = x.GodinaStudijaId.ToString(),
                    Text = x.Godina
                }).ToList();
            }

            if (vm.BrojRazreda==null)
            {
                vm.BrojRazreda = _context.Razred.Select(x => new SelectListItem
                {
                    Value = x.RazredId.ToString(),
                    Text = x.OpisRazreda
                }
                ).ToList();
            }
            return vm;
        }
        private OdjeljenjeUrediVm PopuniVm(OdjeljenjeUrediVm vm,int godinaId) {

            

            var nastavniPlan = from x in _context._NastavniPlan
                               join s in _context._GodinaStudija on x.GodinaStudiranjaId equals s.GodinaStudijaId
                               where s.GodinaStudijaId == godinaId
                               select x;

            vm.NastavniPlan = nastavniPlan.Select(x => new SelectListItem
            {
                Value = x.NastavniPlanId.ToString(),
                Text = x.Naziv
            }).ToList();

            if (vm.Ucenici == null && vm.OdjeljenjeId != 0)
            {

                var uceniciOdjeljenja = from x in _context._UpisUOdjeljenje
                                        join p in _context._Ucenik on x.UcenikId equals p.KorisnikId
                                        where x.OdjeljenjeId == vm.OdjeljenjeId
                                        select new { p.KorisnikId, p.Prezime, p.Ime };


                vm.Ucenici = uceniciOdjeljenja.Select(x => new SelectListItem
                { Value = x.KorisnikId.ToString(), Text = x.Ime + " " + x.Prezime }).ToList();
            }



            if (vm.Nastavnici == null)
                vm.Nastavnici = _context._Nastavnik.Select(x => new SelectListItem
                {
                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }).ToList();


            return vm;
        }

        [HttpPost]
        public IActionResult Snimi(OdjenjenjeVM vm )
        {
            if (!ModelState.IsValid)
            {
                return View("DodajOdjeljenje", GetDefaultVM(vm));
            }

            var NastavniPlan = _context._NastavniPlan.First(x => x.GodinaStudiranjaId == vm.GodinaStudiranjaId && x.RazredId == vm.RazredID);

            Odjeljenje novoOdljeljenje = new Odjeljenje {

                Oznaka=vm.Oznaka,
                Kapacitet=vm.Kapacitet,
                RazredID = vm.RazredID,
                RazrednikId =vm?.NastavnikID,
                NastavniPlanId= NastavniPlan.NastavniPlanId,
                GodinaStudijaId=vm.GodinaStudiranjaId

            };
           
            _context._Odjeljenje.Add(novoOdljeljenje);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));

        }

        public IActionResult Obrisi(int OdjeljenjeID)
        {
           List<UpisUOdjeljenje> upisi = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeID).ToList();
            foreach (var item in upisi)
            {
                _context._UpisUOdjeljenje.Remove(_context._UpisUOdjeljenje.Find(item.UpisUOdjeljenjeId));
                _context.SaveChanges();
            }

            _context._Odjeljenje.Remove(_context._Odjeljenje.Find(OdjeljenjeID));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DodajUOdjeljenje(int OdljenjenjeID)
        {
            UcenikOdjeljenje ucenik = new UcenikOdjeljenje();

            ucenik.Ucenik = _context._Ucenik.Select(x => new SelectListItem
            {
                Value = x.KorisnikId.ToString(),
                Text = x.Prezime + " " + x.Ime
            }).ToList();


            //ucenik.Ucenik = _context._Ucenik.ToList();


            Odjeljenje odabirOdljenjenje = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == OdljenjenjeID);
            if (odabirOdljenjenje != null) {

                ucenik.OdjeljenjeId = odabirOdljenjenje.OdjeljenjeId;
                ucenik.Oznaka = odabirOdljenjenje.Oznaka;
            }
            



            return PartialView(ucenik);
        }


        [HttpPost]
        public IActionResult SnimiOdjeljenje(UcenikOdjeljenje vm)
        {



            UpisUOdjeljenje noviUpis = new UpisUOdjeljenje
            {
                BrojUDnevniku = vm.BrojUdnevniku,
                OdjeljenjeId = vm.OdjeljenjeId,
                UcenikId = vm.UcenikId

            };



            _context._UpisUOdjeljenje.Add(noviUpis);
            _context.SaveChanges();

            List<UpisUOdjeljenje> listaUcenika = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == vm.OdjeljenjeId).Include(x => x.Ucenik).ToList();
            

            int brojac = 1;

            foreach (var ucenik in listaUcenika)
            {
                ucenik.BrojUDnevniku = brojac;
                brojac++;
            }

            brojac = 1;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult PrebaciUOdljeljenje(int OdjeljenjeID) {

            //Zelimo da prebacimo ucenike iz nizeg odjeljenja u vise odljeljenje
            //Administrator cec imati mogucnost da bira iz kojeg odljenja tako da se oznake nece poreditit(jest da sam mogo i to napravit)
            //Treba napravit listu odjeljenja za jedan manji razred od tog odljenja
            //Ovo bi se trabalo samo desavati odjeljenjima od 6 do 8 do odjeljenja koja su peti razred trebali bi imat mogucnost u upis odjeljenja
            //samo
            //Npravili smo bool prebacen tako kad se radnja odradit ta value se mjenja te to odljenje se brise iz liste 


            var odjeljenje = _context._Odjeljenje.First(x => x.OdjeljenjeId == OdjeljenjeID);

            var Odjeljenja = _context._Odjeljenje.Where(x => x.GodinaStudijaId == odjeljenje.GodinaStudijaId - 1 && x.RazredID == odjeljenje.RazredID - 1 && x.Prebacen == false).ToList();

            OdjeljenjePrebaciVM prebacit = new OdjeljenjePrebaciVM {

                OdjeljenjeID= OdjeljenjeID,
                Oznaka=odjeljenje.Oznaka,

                Odjeljenja=Odjeljenja.Select(x=>new SelectListItem {

                    Value=x.OdjeljenjeId.ToString(),
                    Text=x.Oznaka

                }).ToList()

            };


            return PartialView(prebacit);

        }


        public IActionResult SnimiPrebaceno(OdjeljenjePrebaciVM vm) {

            if (!ModelState.IsValid)
            {
                var odjeljenje = _context._Odjeljenje.First(x => x.OdjeljenjeId ==vm.OdjeljenjeID);

                var Odjeljenja = _context._Odjeljenje.Where(x => x.GodinaStudijaId == odjeljenje.GodinaStudijaId - 1 && x.RazredID == odjeljenje.RazredID - 1 && x.Prebacen == false).ToList();
                vm.Odjeljenja = Odjeljenja.Select(x => new SelectListItem
                {

                    Value = x.OdjeljenjeId.ToString(),
                    Text = x.Oznaka

                }).ToList();

                return View("PrebaciUOdljeljenje", vm);
            }

            var staroOdjeljenje = _context._Odjeljenje.FirstOrDefault(x=>x.OdjeljenjeId==vm.OdabranoOdljeljenjeID);

            staroOdjeljenje.Prebacen = true;
            _context.Update(staroOdjeljenje);


            var Ucenici = from x in _context._UpisUOdjeljenje
                          where x.OdjeljenjeId == vm.OdabranoOdljeljenjeID
                          select new {x.UcenikId,x.BrojUDnevniku };


            int brojac = 0;

            foreach (var ucenik in Ucenici)
            {
                if (brojac < _context._Odjeljenje.First(x => x.OdjeljenjeId == vm.OdjeljenjeID).Kapacitet)
                {
                    UpisUOdjeljenje noviUpis = new UpisUOdjeljenje
                    {
                        UcenikId = ucenik.UcenikId,
                        OdjeljenjeId = vm.OdjeljenjeID,
                        BrojUDnevniku = ucenik.BrojUDnevniku
                    };

                    _context._UpisUOdjeljenje.Add(noviUpis);

                    brojac++;
                }
            }

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));

        }

        public IActionResult urediProvjeriOznaku(string Oznaka, int GodinaStudiranjaId, int RazredId,int OdjeljenjeId) {

            var odjeljenjeProvjera = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == OdjeljenjeId);
            if (odjeljenjeProvjera.Oznaka.Equals(Oznaka))
            {
                return Json(true);
            }

            var odjeljenja = _context._Odjeljenje.Where(x => x.GodinaStudijaId == odjeljenjeProvjera.GodinaStudijaId);
            bool provjera = false;
            foreach (var item in odjeljenja)
            {
                if (item.Oznaka.Equals(Oznaka)) {
                    provjera = true;
                }
            }

            if (provjera)
            {
                return Json("Oznaka se vec koristi");
            }

            var razred = _context.Razred.FirstOrDefault(x => x.RazredId == RazredId);
            string nesto = Oznaka.Substring(0, 1);


            if (!nesto.Equals(razred.BrojRazreda.ToString()))
            {

                return Json($"Razred i oznaka se odjeljenja se ne pododaraju!");
            }


            return Json(true);

        }


        public IActionResult ProvjeriKapacitet(int OdjeljenjeID,int OdabranoOdljeljenjeID) {

            int Kapacitet = _context._Odjeljenje.First(x => x.OdjeljenjeId == OdjeljenjeID).Kapacitet;
            int brojUcenikaUpisanih = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeID).Count();

            if (OdabranoOdljeljenjeID != 0)
            {
                int brojUpisanih= _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdabranoOdljeljenjeID).Count();
                int provjera = brojUcenikaUpisanih + brojUpisanih;

                if (provjera > Kapacitet)
                    return Json("Nije moguce prebaciti odbabrano odjeljenje.Prebacivanjem ucenika precice se maksimalni kapacitet!");
            }

            if (brojUcenikaUpisanih < Kapacitet) { return Json(true); }

            return Json("Dosegnut maximalni kapacitet ucenika!");

        }

        public IActionResult ProvjeriOdjljenjeKapcitet(int Kapacitet, int OdjeljenjeID) {

            if (OdjeljenjeID != 0) {

                var broj = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeID).Count();
                if (Kapacitet < broj)
                    return Json("Kapacitet ne smije biti manji od broja ucenika u razredu!");
            }

            return Json(true);
        }


        public IActionResult ProvjeriOznaku(string Oznaka, int GodinaStudiranjaId, int OdjeljenjeID)
        {

            //Posalje mi se oznaka 5-a i odljenje 2 iz Uredi
            //Ja uzmem to odjeljenje i njegovu oznaku u provjerim sa oznakom 5-a
            //Ako je isto onad je sve OK ako je razlicito samo pusti dalje da odradi kod

            if (OdjeljenjeID != 0) {

                var odjeljenjeProvjera = _context._Odjeljenje.FirstOrDefault(x =>x.OdjeljenjeId== OdjeljenjeID);
                bool provjera = odjeljenjeProvjera.Oznaka.Equals(Oznaka);
                if (provjera) {
                    return Json(true);
                }

            }

            var odjeljenje = _context._Odjeljenje.FirstOrDefault(x => x.Oznaka == Oznaka && x.GodinaStudijaId == GodinaStudiranjaId);

            if (odjeljenje != null)
            {

                return Json($"Napisana oznaka se vec koristu u toj skolokoj godini");
            }

            return Json(true);
        }

        public IActionResult ProvjeriRazrednika(int NastavnikID, int GodinaStudiranjaId,int OdjeljenjeID) {

            if (OdjeljenjeID !=0)
            {

                var odjeljenjeProvjera = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == OdjeljenjeID);
                if (odjeljenjeProvjera.RazrednikId==NastavnikID)
                {
                    return Json(true);
                }

            }

            var odjeljenje = _context._Odjeljenje.FirstOrDefault(x => x.RazrednikId == NastavnikID && x.GodinaStudijaId == GodinaStudiranjaId);

            if (odjeljenje != null)
            {

                return Json($"Odabrani nastavnik je vec razrednik u toj skolskoj godini!");
            }

            return Json(true);
        }

        public IActionResult ProvjeriRazred(int RazredID, string Oznaka,int GodinaStudiranjaId)
        {

            var NastavniPlan = _context._NastavniPlan.FirstOrDefault(x => x.GodinaStudiranjaId == GodinaStudiranjaId && x.RazredId == RazredID);
             
            if (NastavniPlan == null) {

                return Json("Nepostoji nastavni plan za taj razred.Molimo prvo napravit nastavni plan!");
            }

            if (String.IsNullOrEmpty(Oznaka)) {
                return Json($"Razred i oznaka se ne podudaraju");
            };

            var razred = _context.Razred.FirstOrDefault(x => x.RazredId == RazredID);
            string nesto = Oznaka.Substring(0, 1);
            bool provjera = nesto.Equals(razred.BrojRazreda.ToString());


            if (!provjera)
            {

                return Json($"Razred i oznaka se odjeljenja se ne pododaraju!");
            }

            return Json(true);
        }


    }
}