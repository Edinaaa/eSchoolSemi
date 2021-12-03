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
    public class RoditeljController : Controller
    {
        private MojContext _context;
        public RoditeljController(MojContext context) => _context = context;

        //Dodat ViewModel da se vidie i gradovi
        public IActionResult Index()
        {
            

            
            return View();
        }

        public async Task<IActionResult> tabelaRoditelji(string sortOrder,string search,int? page) {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ime_desc" : "";

            var Roditelji = _context._Roditelj.Include(x => x.MjestoRodenja).ToList();
            IQueryable<Roditelj> ListRoditelja = from x in _context._Roditelj
                                                 from s in _context._Grad
                                                 where x.GradId == s.GradId
                                                 select new Roditelj { KorisnikId = x.KorisnikId, Email = x.Email, Telefon = x.Telefon,
                                                                       KorisnickoIme = x.KorisnickoIme,Lozinka=x.Lozinka,Ime=x.Ime,Prezime=x.Prezime,
                                                                       GradId =x.GradId,MjestoRodenja=s};

            if (!String.IsNullOrEmpty(search))
            {

                ListRoditelja = ListRoditelja.Where(x => (x.Ime +" "+ x.Prezime).Contains(search) || (x.Prezime + " " + x.Ime).Contains(search));
            }

            switch (sortOrder)
            {
                case "ime_desc":
                    ListRoditelja = ListRoditelja.OrderByDescending(x => x.Prezime);
                    break;
                default:
                    ListRoditelja = ListRoditelja.OrderBy(x => x.Prezime);
                    break;

            }

            int pageSize = 10;
            return PartialView(await PaginatedList<Roditelj>.CreateAsync(ListRoditelja.AsNoTracking(), page ?? 1, pageSize));

        }

       

        #region DodavanjeRoditelja
        public IActionResult DodajRoditelja()
        {
            RoditeljDodajViewModel vm = new RoditeljDodajViewModel {

                Gradovi=_context._Grad.Select(x=>new SelectListItem {

                    Value=x.GradId.ToString(),
                    Text=x.Naziv
                }).ToList()
            };

            vm.DatumRodjenja = new DateTime(DateTime.Now.Year - 45, DateTime.Now.Month, DateTime.Now.Day);

            return PartialView(vm);
        }


        [HttpPost]
        public IActionResult DodajRoditelja(RoditeljDodajViewModel vm)
        {
            if (!ModelState.IsValid) {

                return View("DodajRoditelja", GetDefaultVM(vm));

            }

            string passwordGen = Guid.NewGuid().ToString("N").Substring(0, 10);

            KorisnickiNalog korisnicki = new KorisnickiNalog
            {
                Username=vm.Username,
                Password= passwordGen,
                Zapamti=false
            };

            _context.korisnickiNalogs.Add(korisnicki);
            _context.SaveChanges();

            Roditelj noviRoditelj = new Roditelj
            {

                Ime = vm.Ime,
                Prezime = vm.Prezime,
                Telefon = vm.Telefon,
                Email = vm.Email,
                GradId = vm.GradID,
                KorisnickoIme = vm.Username,
                Lozinka = passwordGen,
                KorisnickiNalogID = _context.korisnickiNalogs.FirstOrDefault(x => x.Username == vm.Username && x.Password == passwordGen).KorisnickiNalogID,
                DatumRodenja = vm.DatumRodjenja

            };

            _context._Roditelj.Add(noviRoditelj);
            _context.SaveChanges();
        
           

            
            return RedirectToAction(nameof(tabelaRoditelji));

        }


       
        #endregion

        public IActionResult Obrisi(int RoditeljID)
        {
            Roditelj izbrisi = _context._Roditelj.FirstOrDefault(x => x.KorisnikId == RoditeljID);
            KorisnickiNalog korisnicki = _context.korisnickiNalogs.FirstOrDefault(x => x.KorisnickiNalogID == izbrisi.KorisnickiNalogID);

            var Ucenik = _context._Ucenik.FirstOrDefault(x => x.RoditeljId == RoditeljID);
            if (Ucenik != null) {
                Ucenik.RoditeljId = null;
                _context.Update(Ucenik);
            }
           

            _context.korisnickiNalogs.Remove(korisnicki);
            
            _context._Roditelj.Remove(izbrisi);
            _context.SaveChanges();
            return RedirectToAction(nameof(tabelaRoditelji));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            Roditelj urediRoditelj = _context._Roditelj.FirstOrDefault(x => x.KorisnikId == id);

            RoditeljDodajViewModel novi = new RoditeljDodajViewModel
            {

                Ime= urediRoditelj.Ime,
                Prezime = urediRoditelj.Prezime,
                DatumRodjenja = urediRoditelj.DatumRodenja,
                Email = urediRoditelj.Email,
                Telefon = urediRoditelj.Telefon,
                GradID = urediRoditelj.GradId,
                Gradovi=_context._Grad.Select(x=>new SelectListItem {Value=x.GradId.ToString(),Text=x.Naziv }).ToList(),
                Username = urediRoditelj.KorisnickoIme,
                Password = urediRoditelj.Lozinka,
                RoditeljID=urediRoditelj.KorisnikId

            };

            

            return PartialView(novi);

        }

        public IActionResult UrediRoditelja(RoditeljDodajViewModel vm)
        {
            if (!ModelState.IsValid)
            {

                return View("Uredi", GetDefaultVM(vm));

            }

            

            Roditelj noviRoditelj = _context._Roditelj.FirstOrDefault(X => X.KorisnikId == vm.RoditeljID);

            string passwordGen = Guid.NewGuid().ToString("N").Substring(0, 10);
            if (String.IsNullOrEmpty(vm.Password)) {

                vm.Password = passwordGen;
            }

            KorisnickiNalog korisnik = _context.korisnickiNalogs.Where(x => x.KorisnickiNalogID == noviRoditelj.KorisnickiNalogID).FirstOrDefault();

            if (korisnik.Username!=vm.Username || korisnik.Password!=vm.Password)
            {
                korisnik.Password = vm.Password;
                korisnik.Username = vm.Username;

                _context.Update(korisnik);
            }


            noviRoditelj.Ime = vm.Ime;
            noviRoditelj.Prezime = vm.Prezime;
            noviRoditelj.Telefon = vm.Telefon;
            noviRoditelj.Email = vm.Email;
            noviRoditelj.GradId = vm.GradID;
            noviRoditelj.KorisnickoIme = vm.Username;
            noviRoditelj.Lozinka = vm.Password;
            noviRoditelj.DatumRodenja = vm.DatumRodjenja;

            

            _context.Update(noviRoditelj);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        
        public IActionResult Detalji(int? id)
        {
            Roditelj novi = _context._Roditelj.Where(s => s.KorisnikId == id).Include(s=>s.Uceniks).FirstOrDefault();

            return PartialView(novi);

        }

        public IActionResult ProvjeriTelefon(Roditelj roditelj)
        {
            if (_context._Korisnik.Any(x => x.Telefon == roditelj.Telefon))
                return Json($"Broj{roditelj.Telefon} je zauzet!");
            return Json(true);
        }


        public RoditeljDodajViewModel GetDefaultVM(RoditeljDodajViewModel vm) {

            if (vm.Gradovi == null)
            {
                vm.Gradovi = _context._Grad.Select(x => new SelectListItem
                {
                    Value = x.GradId.ToString(),
                    Text = x.Naziv
                }).ToList();
            }

            return vm;
        }


    }
}