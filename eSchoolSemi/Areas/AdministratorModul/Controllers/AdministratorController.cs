using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using eSchoolSemi.Web.Helper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class AdministratorController : Controller
    {
        private MojContext _context;


        public AdministratorController(MojContext context) => _context = context;

        public IActionResult Index()
        {

            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var AdMin = _context.Administrators.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);


            AdministratorIndexVM admini = new AdministratorIndexVM
            {

                KorisnikId = AdMin.KorisnikId,
                Administratori = _context.Administrators.Select(x => new AdministratorIndexVM.Row
                {

                    AdministratorId = x.KorisnikId,
                    ImePrezime = x.Ime + " " + x.Prezime,
                    Username = x.KorisnickoIme,
                    Password = x.Lozinka
                }).ToList()
            };

            return View(admini);
        }

        public IActionResult Dodaj()
        {

            AdministratorDodajVM dodajVM = new AdministratorDodajVM();


            return PartialView(dodajVM);
        }

        public IActionResult Snimi(AdministratorDodajVM obj)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", obj);
            }


            KorisnickiNalog korisnik = new KorisnickiNalog
            {
                Username = obj.Username,
                Password = obj.Password,
                Zapamti = false
            };

            _context.korisnickiNalogs.Add(korisnik);
            _context.SaveChanges();

            Administrator noviAdmin = new Administrator
            {
                Ime = obj.Ime,
                Prezime = obj.Prezime,
                DatumRodenja = DateTime.Today,
                Email = "ImeSkole@mail.com",
                Telefon = "+387(035)-555-999",
                KorisnickoIme = obj.Username,
                Lozinka = obj.Password,
                GradId = null,
                KorisnickiNalogID = _context.korisnickiNalogs.Where(x => x.Username == obj.Username).First().KorisnickiNalogID
            };

            _context.Administrators.Add(noviAdmin);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int korisnikId)
        {


            var administrator = _context.Administrators.FirstOrDefault(x => x.KorisnikId == korisnikId);
            var nalozi = _context.korisnickiNalogs.FirstOrDefault(x => x.KorisnickiNalogID == administrator.KorisnickiNalogID);


            _context.korisnickiNalogs.Remove(nalozi);



            _context.Administrators.Remove(administrator);


            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult GodinaStudijaPromjena()
        {

            var tretnutnaGodina = _context._GodinaStudija.First(x => x.TrenutnaGodina == true);

            GodinaStudijaVM godina = new GodinaStudijaVM
            {

                TretnutnaGodinaID = tretnutnaGodina.GodinaStudijaId,
                Opis = tretnutnaGodina.Godina,
                GodinaStudija = _context._GodinaStudija.Select(x => new SelectListItem
                {
                    Value = x.GodinaStudijaId.ToString(),
                    Text = x.Godina

                }).ToList()
            };


            return PartialView(godina);
        }

        public IActionResult SnimiPromjenu(GodinaStudijaVM vM)
        {

            var godinaTren = _context._GodinaStudija.First(x => x.GodinaStudijaId == vM.TretnutnaGodinaID);
            godinaTren.TrenutnaGodina = false;
            _context.Update(godinaTren);

            var godinaIzab = _context._GodinaStudija.First(x => x.GodinaStudijaId == vM.GodinaStudijaId);
            godinaIzab.TrenutnaGodina = true;
            _context.Update(godinaIzab);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PromjeniPassword()
        {

            PromjeniPasswordVM novaLozinka = new PromjeniPasswordVM
            {

                KorisnickiNalogId = HttpContext.GetLogiraniKorisnik().KorisnickiNalogID
            };


            return PartialView(novaLozinka);

        }

        public IActionResult SnimiPassword(PromjeniPasswordVM vm)
        {
            KorisnickiNalog novi = _context.korisnickiNalogs.First(x => x.KorisnickiNalogID == vm.KorisnickiNalogId);
            var korisnik = _context._Korisnik.First(x => x.KorisnickiNalogID == vm.KorisnickiNalogId);

            korisnik.Lozinka = vm.NoviPassword;
            novi.Password = vm.NoviPassword;

            _context.Update(korisnik);
            _context.Update(novi);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #region PasswordProvjera

        public IActionResult ProvjeriStari(string StariPassword, int KorisnickiNalogId) {

            var nalog = _context.korisnickiNalogs.First(x => x.KorisnickiNalogID == KorisnickiNalogId);

            if (nalog.Password.Equals(StariPassword))
            {
                return Json(true);
            }

            return Json("Pogresan stari password!");

        }


        public IActionResult ProvjeriNovi(string NoviPassword,string StariPassword)
        {
            bool provjera = NoviPassword.Equals(NoviPassword);

            if (!NoviPassword.Equals(StariPassword))
            {
                return Json(true);
            }

            return Json("Novi i stari password su isti!");

        }


        public IActionResult ProvjeriPromjeni(string PromjeniPassword, string NoviPassword)
        {


            if (NoviPassword.Equals(PromjeniPassword))
            {
                return Json(true);
            }

            return Json("Passwordi se ne podudaraju!");

        }



        #endregion
    }
}