using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Helper;
using eSchoolSemi.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;

namespace eSchoolSemi.Web.Controllers
{
    public class DvofaktorskaAutentitfikacijaController : Controller
    {
        private readonly MojContext _context;

        public DvofaktorskaAutentitfikacijaController(MojContext context) => _context = context;

        public IActionResult Index()
        {

            return View(new LoginVM()
            {
                ZapamtiPassword = true,
            });
        }
        

        public IActionResult Login(LoginVM input)
        {
            KorisnickiNalog korisnik = _context.korisnickiNalogs
                .SingleOrDefault(x => x.Username == input.username && x.Password == input.password);

            

            if (korisnik == null)
            {
                TempData["error_poruka"] = "Pogrešan username ili password";
                return View("Index", input);
            }
            // odvdje dodati token
           Korisnik k = _context._Korisnik.Where(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID).FirstOrDefault();
            string telefon = String.Join("", k.Telefon.Where(Char.IsDigit));// izdvaja samo brojeve u string
            if (korisnik != null)
            {
                double min =4;
              // korisnik ima dvije minute da unese token
                Random random = new Random();
                string str = random.Next().ToString();
                string token = str.Substring(0,4);//samo prva 4 karaktera
                korisnik.Token = token;
                korisnik.TokenVaziDo = DateTime.Now.AddMinutes(min);

                //send 2fa token to sms
                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "1979e701",
                    ApiSecret = "p8DCtNVOpPVHkpHE"
                });
                var results = client.SMS.Send(request: new SMS.SMSRequest
                {
                    from = "eSchool",
                    to = telefon,
                    text = "Vas token za login: " + token
                });

                _context.SaveChanges();
               
            }
            LoginWithTokenVM vm = new LoginWithTokenVM() {
                password=input.password,
                Token="unesite token",
                username=input.username,
                ZapamtiPassword=input.ZapamtiPassword
            };
            
           
            return View(vm);
        }
        public IActionResult LoginWithToken(string username, string password, string Token, bool ZapamtiPassword)
        {
            KorisnickiNalog korisnik = _context.korisnickiNalogs
              .SingleOrDefault(x => x.Username == username && x.Password == password);
            // ako token odg ide prijava
            if (Token!=korisnik.Token || korisnik.TokenVaziDo<DateTime.Now)
            {
                TempData["error_poruka"] = "Pogrešan token ili je isteklo vrijeme";
               
                return RedirectToAction("Index", "DvofaktorskaAutentitfikacija");
            }

              Administrator prijavaA = _context.Administrators.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);


            Nastavnik prijavaN = _context._Nastavnik.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);
            Roditelj prijavaR = _context._Roditelj.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);


            Ucenik prijavaU = _context._Ucenik.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            if (prijavaA != null)
            {

                korisnik.Zapamti = ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik, ZapamtiPassword);
                return RedirectToAction("Index", "Administrator", new { area = "AdministratorModul" });
            }

            if (prijavaN != null)
            {
                korisnik.Zapamti = ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik,ZapamtiPassword);
                //Ovde stavis gdje hoces da ti redirecta Login na koju stranicu
                return RedirectToAction("Index", "Nastavnik", new { area = "NastavnikModul" });
            }
            if (prijavaR != null)
            {
                korisnik.Zapamti = ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik, ZapamtiPassword);
                //Ovde stavis gdje hoces da ti redirecta Login na koju stranicu
                return RedirectToAction("Index", "Roditelj", new { area = "RoditeljModul" });
            }

            if (prijavaU != null)
            {
                korisnik.Zapamti = ZapamtiPassword;
                HttpContext.SetLogiraniKorisnik(korisnik, ZapamtiPassword);
                return RedirectToAction("Index", "Ucenik", new { area = "UcenikModul" });
            }
            HttpContext.SetLogiraniKorisnik(korisnik, ZapamtiPassword);
            return RedirectToAction("Index", "Home");


        }
      
        public IActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);

            return RedirectToAction("Index");
        }
    }
}