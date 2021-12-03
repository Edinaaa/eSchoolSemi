using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchoolSemi.Data;
using Microsoft.AspNetCore.Mvc;
using eSchool.Data.Models;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSchoolSemi.Web.Helper;
using eSchoolSemi.Data.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    

    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class UcenikController : Controller
    {
        private MojContext _context;


        public UcenikController(MojContext context) => _context = context;

        public string sortFilter = "empty";
       

        public IActionResult Index()
        {

           



           
            



            return View();
        }

        

        public async Task<IActionResult> _tabela(string sortOrder,string currentFilter,string searchString,int? page) {

            
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; 

            IQueryable<Ucenik> listaUcenika = from x in _context._Ucenik
                                              select x;
            

            if (!String.IsNullOrEmpty(searchString))
            {
                           
               
                listaUcenika = listaUcenika.Where(x => (x.Ime + x.Prezime).Contains(searchString) ||(x.Prezime +" "+ x.Ime).Contains(searchString));
            }

           

                switch (sortOrder)
                {
                    case "name_desc":
                        listaUcenika = listaUcenika.OrderByDescending(x => x.Prezime);
                        break;
                    default:
                        listaUcenika = listaUcenika.OrderBy(x => x.Prezime);
                        break;
                }

            
            




           




            int pageSize = 10;
            return PartialView(await PaginatedList<Ucenik>.CreateAsync(listaUcenika.AsNoTracking(), page ?? 1, pageSize));

        }

        public IActionResult DodajUcenikaOdlj() {


            OdjeljenjeUcenikVm dodavanjeUcenik = new OdjeljenjeUcenikVm();
                      

            return PartialView(DefaultVM(dodavanjeUcenik));
        }

        public JsonResult vratiOdjlejenje(int GodinaID, int RazredID) {


             var odabranaOdljenja = _context._Odjeljenje.Where(x => x.GodinaStudijaId == GodinaID && x.RazredID == RazredID && _context._UpisUOdjeljenje.Where(s=>s.OdjeljenjeId==x.OdjeljenjeId).Count()<x.Kapacitet).
                                            Select(x => new SelectListItem {
                                                Value=x.OdjeljenjeId.ToString(),
                                                Text=x.Oznaka

                                            }).ToList();


            return Json(odabranaOdljenja);
        }

        public IActionResult snimiUpis(int UcenikId,int OdjeljenjeId) {


            if (UcenikId != 0 && OdjeljenjeId != 0) { 

            UpisUOdjeljenje noviUpis = new UpisUOdjeljenje {

                UcenikId = UcenikId,
                OdjeljenjeId = OdjeljenjeId,
                BrojUDnevniku = 0
            };

            _context._UpisUOdjeljenje.Add(noviUpis);
            _context.SaveChanges();

            IEnumerable<UpisUOdjeljenje> listaUcenika = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeId).Include(x => x.Ucenik).ToList();

            listaUcenika = listaUcenika.OrderBy(x => x.Ucenik.Prezime);

            int brojac = 1;
            foreach (var ucenik in listaUcenika) {

                ucenik.BrojUDnevniku = brojac;
                brojac++;

            }

           
            _context.SaveChanges();

            }

            return RedirectToAction(nameof(Index));

        }




        public JsonResult nazivUcenika(string Prefix) {

            List<SelectListItem> ucenik = _context._Ucenik.Where(x => (x.Ime + " " + x.Prezime).StartsWith(Prefix)||( x.Prezime + " " + x.Ime).StartsWith(Prefix)).Select(x => new SelectListItem
            {

                Value = x.KorisnikId.ToString(),
                Text = x.Prezime + " " + x.Ime
            }).ToList();
            

           

            return Json(ucenik);
        }


        public JsonResult nazivRoditelja(string Prefix)
        {

            List<SelectListItem> ucenik = _context._Roditelj.Where(x => (x.Ime + " " + x.Prezime).StartsWith(Prefix) || (x.Prezime + " " + x.Ime).StartsWith(Prefix)).Select(x => new SelectListItem
            {

                Value = x.KorisnikId.ToString(),
                Text = x.Prezime + " " + x.Ime
            }).ToList();




            return Json(ucenik);
        }

        public IActionResult DodajUcenika()
        {
            UcenikDodajVM vm = new UcenikDodajVM();
            vm.DatumRodjenja= new DateTime(DateTime.Now.Year - 14, DateTime.Now.Month, DateTime.Now.Day);



            return PartialView(GetDefaultVM(vm));
        }

        public IActionResult DodajFile()
        {
            Ucenik ucenik = _context._Ucenik.FirstOrDefault();

            return View(ucenik);
        }

        
        [HttpPost]
        public async Task<IActionResult> SnimiUcenika(UcenikDodajVM vm)
        {




            if (!ModelState.IsValid)
            {
               
                return View("DodajUcenika", GetDefaultVM(vm));
            }


            Roditelj roditelj = _context._Roditelj.FirstOrDefault(x => x.Prezime + " " + x.Ime == vm.Roditelj);

            string passwordGen = Guid.NewGuid().ToString("N").Substring(0, 10);

            KorisnickiNalog korisnicki = new KorisnickiNalog
            {
                Username = vm.Username,
                Password = passwordGen,
                Zapamti = false
            };

            _context.korisnickiNalogs.Add(korisnicki);
            _context.SaveChanges();

            Ucenik noviUcenik = new Ucenik
            {

                Ime = vm.Ime,
                Prezime = vm.Prezime,
                Telefon = vm.Telefon,
                Email = vm.Email,
                GradId = vm.GradID,
                KorisnickoIme = vm.Username,
                Lozinka = passwordGen,
                KorisnickiNalogID = _context.korisnickiNalogs.FirstOrDefault(x => x.Username == vm.Username && x.Password == passwordGen).KorisnickiNalogID,
                DatumRodenja = vm.DatumRodjenja,
                Vladanje = "Primjerno",
                RoditeljId=roditelj?.KorisnikId
                

            };

           

            if (vm.KorinickaSlika != null)
            {
                using (var memoryStream = new MemoryStream())
                {

                    await vm.KorinickaSlika.CopyToAsync(memoryStream);
                    noviUcenik.KorisnickaSlika = memoryStream.ToArray();
                }
            }





            _context._Ucenik.Add(noviUcenik);
            _context.SaveChanges();

          
            return RedirectToAction("Index");

        }




        #region glupeFunkcije

        public UcenikDodajVM GetDefaultVM(UcenikDodajVM vm)
        {

         

            if (vm.Gradovi==null)
            {
                vm.Gradovi = _context._Grad.Select(x => new SelectListItem
                {
                    Value = x.GradId.ToString(),
                    Text=x.Naziv
                }).ToList();
            }

            if (vm.Roditleji == null)
            {
                vm.Roditleji = _context._Roditelj.Select(x => new SelectListItem
                {
                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }).ToList();
            }

            

            return vm;
        }

        public OdjeljenjeUcenikVm DefaultVM(OdjeljenjeUcenikVm vm)
        {



            vm.Ucenici = _context._Ucenik.Where(x => !_context._UpisUOdjeljenje.Any(s => s.UcenikId == x.KorisnikId)).Select(x => new SelectListItem
            {

                Value = x.KorisnikId.ToString(),
                Text = x.Prezime + " " + x.Ime

            }).ToList();

            var godinaStudija = _context._GodinaStudija.FirstOrDefault(x => x.TrenutnaGodina == true);
            vm.GodinaStudijaId = godinaStudija.GodinaStudijaId;
            vm.GodineStudija = godinaStudija.Godina;

            vm.Razredi = _context.Razred.Select(x => new SelectListItem
            {

                Value = x.RazredId.ToString(),
                Text = x.OpisRazreda

            }).ToList();




            return vm;
        }

        public UcenikUrediVM Popuni(UcenikUrediVM vm)
        {



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
        #endregion

        public IActionResult Obrisi(int UcenikID)
        {


            Ucenik izbrisi = _context._Ucenik.FirstOrDefault(x => x.KorisnikId == UcenikID);
            KorisnickiNalog korisnicki = _context.korisnickiNalogs.FirstOrDefault(x => x.KorisnickiNalogID == izbrisi.KorisnickiNalogID);
            var Odjeljenje = _context._Odjeljenje.FirstOrDefault(x => x.UcenikID == UcenikID);
            var upisanaOdjeljenja = _context._UpisUOdjeljenje.Where(x => x.UcenikId == UcenikID).ToList();

            foreach (var upis in upisanaOdjeljenja)
            {
                IEnumerable<UpisUOdjeljenje> obj = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == upis.OdjeljenjeId).Include(s=>s.Ucenik).ToArray();

                _context._UpisUOdjeljenje.Remove(upis);
                              
            }

            if (Odjeljenje != null)
            {
                Odjeljenje.UcenikID = null;
                _context.Update(Odjeljenje);
            }

            _context.korisnickiNalogs.Remove(korisnicki);

            _context._Ucenik.Remove(izbrisi);
            _context.SaveChanges();
            return RedirectToAction(nameof(_tabela));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            

            

            Ucenik urediUcenik = _context._Ucenik.Include(x=>x.Roditelj).FirstOrDefault(x => x.KorisnikId == id);

            UcenikUrediVM novi = new UcenikUrediVM
            {

                Ime = urediUcenik.Ime,
                Prezime = urediUcenik.Prezime,
                DatumRodjenja = urediUcenik.DatumRodenja,
                Email = urediUcenik.Email,
                Telefon = urediUcenik.Telefon,
                GradID = urediUcenik.GradId,
                Username = urediUcenik.KorisnickoIme,
                Password = urediUcenik.Lozinka,
                Roditelji = (urediUcenik.Roditelj != null) ? urediUcenik.Roditelj.Prezime + " " + urediUcenik.Roditelj.Ime:"",
                UcenikID=urediUcenik.KorisnikId
                

            };

                
            



            return PartialView(Popuni(novi));

        }

        public async Task<IActionResult> UrediUcenika(UcenikUrediVM vm)
        {
            if (!ModelState.IsValid)
            {

                return PartialView("Uredi", Popuni(vm));
            }
           
               

                Ucenik noviUcenik = _context._Ucenik.FirstOrDefault(X => X.KorisnikId == vm.UcenikID);

                KorisnickiNalog korisnik = _context.korisnickiNalogs.Where(x => x.KorisnickiNalogID == noviUcenik.KorisnickiNalogID).FirstOrDefault();

                if (korisnik.Username != vm.Username || korisnik.Password != vm.Password)
                {
                    korisnik.Password = vm.Password;
                    korisnik.Username = vm.Username;

                    _context.Update(korisnik);
                }


                noviUcenik.Ime = vm.Ime;
                noviUcenik.Prezime = vm.Prezime;
                noviUcenik.Telefon = vm.Telefon;
                noviUcenik.Email = vm.Email;
                noviUcenik.GradId = vm.GradID;
                noviUcenik.KorisnickoIme = vm.Username;
                noviUcenik.Lozinka = vm.Password;
                noviUcenik.DatumRodenja = vm.DatumRodjenja;
               

            try
            {
                Roditelj roditelj = _context._Roditelj.FirstOrDefault(x => x.Prezime + " " + x.Ime == vm.Roditelji);
                noviUcenik.RoditeljId = roditelj?.KorisnikId;

                
            }

            catch (Exception e)
            {
                var nesto = e.Message;
                var temp = e.Data;

                var st = new StackTrace(e, true);
           
                var frame = st.GetFrame(0);
              
                var line = frame.GetFileLineNumber();
            }

            if (vm.KorinickaSlika != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {

                        await vm.KorinickaSlika.CopyToAsync(memoryStream);
                        noviUcenik.KorisnickaSlika = memoryStream.ToArray();
                    }
                }

           
                _context._Ucenik.Update(noviUcenik);
                _context.SaveChanges();
       



            return RedirectToAction("_tabela");

        }

        public IActionResult Detalji(int id) {


            Ucenik ucenik = _context._Ucenik.Where(x => x.KorisnikId == id).Include(x => x.Roditelj).Include(x => x.MjestoRodenja).FirstOrDefault();
            

            UcenikDetalj detalji = new UcenikDetalj {

                Slika = ucenik.KorisnickaSlika,
                ImeIprezime = ucenik.Ime + " " + ucenik.Prezime,
                DatumRodjenja = ucenik.DatumRodenja.ToShortDateString(),
                Roditelj = ucenik.Roditelj.Ime + " " + ucenik.Roditelj.Prezime,
                MjestoPrebivalista = ucenik.MjestoRodenja.Naziv,
                Odjeljenja=_context._UpisUOdjeljenje.Select(x=>new UcenikDetalj.Row {

                    Oznaka=x.Odjeljenje.Oznaka,
                    GodinaStudija=x.Odjeljenje.GodinaStudija.Godina
                })
               
                
            };

            

            return View(detalji);
        }
        
        

       

        #region Provjera view modela za dodavanje ucenika
        public IActionResult ProvjeriRoditelj(string Roditelj)
        {
            Roditelj roditelj = _context._Roditelj.FirstOrDefault(x => x.Prezime + " " + x.Ime == Roditelj);
            if (roditelj==null)
            {
                return Json("Pogresno prezime i ime roditelja!");
            }

            return Json(true);
        }

        public IActionResult ProvjeriUsername(string Username,int UcenikID)
        {
            var provjera = _context.korisnickiNalogs.FirstOrDefault(x => x.Username == Username);

            if (provjera != null) {
                return Json("Username je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult ProvjeriEmail(string Email)
        {
            if (Email == null)
                return Json(true);

            Korisnik roditelj = _context._Korisnik.FirstOrDefault(x => x.Email == Email);
            if (roditelj != null)
            {
                return Json("Email je vec zauzet!");
            }

            return Json(true);
        }


        public IActionResult ProvjeriTelefon(string Telefon)
        {
            

            Korisnik roditelj = _context._Korisnik.FirstOrDefault(x => x.Telefon == Telefon);
            if (roditelj != null)
            {
                return Json("Broj telefona je vec zauzet!");
            }

            return Json(true);
        }
        #endregion

        #region Provjera za uredjivanje ucenika
        public IActionResult UsernameCheck(string Username, int UcenikID)
        {

            Korisnik prviValue = _context._Korisnik.First(x => x.KorisnikId == UcenikID);
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.KorisnickoIme == Username && Username != prviValue.KorisnickoIme);

            if (provjera != null)
            {
                return Json("Username je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult EmailCheck(string Email, int UcenikID)
        {

            Korisnik prviValue = _context._Korisnik.First(x => x.KorisnikId == UcenikID);
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.Email == Email && Email != prviValue.Email);

            if (provjera != null)
            {
                return Json("Email je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult TelefonCheck(string Telefon, int UcenikID)
        {

            Korisnik prviValue = _context._Korisnik.First(x => x.KorisnikId == UcenikID);
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.Telefon == Telefon && Telefon != prviValue.Telefon);

            if (provjera != null)
            {
                return Json("Broj telefona je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult VerifyFile(string KorinickaSlika) {

            var path =Path.GetExtension(KorinickaSlika);
            bool jeliTacna = new[] { ".jpg", ".jpeg", ".png" }.Contains(path);

            if (jeliTacna)
                return Json(true);

            return Json("Pogresan format fila!Primaju se samo slike(.jpg,.jpeg,.png)!");
        }
        #endregion


    }








}