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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.RoditeljModul.Controllers
{
    [Area("RoditeljModul")]
    [Autorizacija(false, false, true, false)]
    public class SastanakController : Controller
    {
       private MojContext db;

        public SastanakController(MojContext _db) { db = _db; }
        public IActionResult Index()
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Roditelj roditelj = db._Roditelj.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).Include(r => r.KorisnickiNalog).FirstOrDefault();
            List<Ucenik> ucenici = db._Ucenik.Where(u => u.RoditeljId == roditelj.KorisnikId).ToList();
            SastanakIndexVm vm = new SastanakIndexVm() {
                RoditeljId =roditelj.KorisnikId,
                Ucenici = ucenici.Select(x => new SastanakIndexVm.Ucenik() {
                    UcenikID = x.KorisnikId,
                    UcenikIme = x.Ime + " " + x.Prezime,

                    Rows = db._Sastanak.Where(s => s.OdjeljenjeId ==
                    db._UpisUOdjeljenje.Where(o => o.UcenikId == x.KorisnikId).FirstOrDefault().OdjeljenjeId).Include(t => t.SastanakTip)
                     .Select(z => new SastanakIndexVm.Ucenik.Row() {
                         Naziv = z.Naziv,
                         OdjeljenjeID = db._UpisUOdjeljenje.Where(o => o.UcenikId == x.KorisnikId).FirstOrDefault().OdjeljenjeId,
                         DatumObjave = z.DatumObavijest,
                         DatumSastanka=z.DatumSastanka,
                         OrganizatorId= (z.OrganizatorId == null) ? 0 : int.Parse(z.OrganizatorId.ToString()) ,
                         
                         SastanakId = z.SastanakId,
                         SastanakTip =(z.SastanakTipId==null)? "n/a ":z.SastanakTip.Naziv,
                       Organizator = (z.OrganizatorId == null)  ? "n/a " :  z.Organizator.Ime
                       

                   }).ToList()


                }).ToList()
            };
            return View(vm);
        }
        public IActionResult Dodaj(int RoditeljId)
        {
           
            Roditelj roditelj = db._Roditelj.Where(x => x.KorisnikId == RoditeljId).Include(r => r.KorisnickiNalog).FirstOrDefault();
            List<Ucenik> ucenici = db._Ucenik.Where(u => u.RoditeljId == roditelj.KorisnikId).ToList();
            SastanakDodajVM vm = new SastanakDodajVM() {
                DatumObjave=DateTime.Now,
             RoditeljID=roditelj.KorisnikId,
             DatumSastanka= DateTime.Now,
             TipSastanka=db._SastanakTip.Select(x=> new SelectListItem() { Value=x.SastanakTipId.ToString(), Text=x.Naziv}).ToList(),
            Komentar=" ",
            Naziv=" ",
            Opis=" ",
           
             Ucenici =ucenici.Select(x=> new SelectListItem() {  Value = x.KorisnikId.ToString(), Text = x.Ime + " " + x.Prezime}).ToList()
             
            };
           
            return View(vm);
        }
        public IActionResult Snimi(SastanakDodajVM vm)
        {

            SastanakTip st = db._SastanakTip.Where(t=>t.Naziv=="Informacije").FirstOrDefault();
            Sastanak s = new Sastanak() {
                DatumObavijest=vm.DatumObjave,
                Naziv=vm.Naziv,
                Opis=vm.Opis,
                OrganizatorId=vm.RoditeljID,
                DatumSastanka = vm.DatumSastanka,
                NastavnikId = db._UpisUOdjeljenje.Include(o => o.Odjeljenje).Where(o => o.UcenikId == vm.UcenikId).FirstOrDefault().Odjeljenje.RazrednikId,
               SastanakTipId=st.SastanakTipId,
                OdjeljenjeId= db._UpisUOdjeljenje.Where(o => o.UcenikId == vm.UcenikId ).FirstOrDefault().OdjeljenjeId,
                
                
            };  db._Sastanak.Add(s);
            SastanakRoditelj sr = new SastanakRoditelj()
            {
                
                Komentar = vm.Komentar,
                RoditeljId = vm.RoditeljID,
               
                SastanakId = s.SastanakId


            };


          
          db._SastanakRoditelj.Add(sr);
            db.SaveChanges();
            return RedirectToAction("Index", "Sastanak");
        }

        public IActionResult Detalji(int SastanakId)
        {

            Sastanak s = db._Sastanak
                .Include(t=>t.SastanakTip).Include(o=>o.Odjeljenje).Include(n => n.Nastavnik).Include(a => a.Organizator)
                .Where(x => x.SastanakId == SastanakId).FirstOrDefault();
            SastanakRoditelj sr = db._SastanakRoditelj.Where(x => x.SastanakId == SastanakId).FirstOrDefault();
            SastanakDetaljiVM vm = new SastanakDetaljiVM()
            {
                DatumObjave = s.DatumObavijest,
              SastanakId=s.SastanakId,
                Nastavnik=  s.Nastavnik.Ime+ " "+s.Nastavnik.Prezime,
                Organizator= s.Organizator.Ime + " " + s.Organizator.Prezime,
                TipSastanka =s.SastanakTip.Naziv,
                Komentar = sr==null?" ":sr.Komentar,
                DatumSastanka = s==null? DateTime.Now: s.DatumSastanka,
                Naziv =s.Naziv,
                Opis = s.Opis,
               Odjeljenje= s.Odjeljenje.Oznaka
               
                

            };

            return View(vm);
        }

        public IActionResult Uredi(int SastanakId)
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Roditelj roditelj = db._Roditelj.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).Include(r => r.KorisnickiNalog).FirstOrDefault();
            Sastanak s = db._Sastanak.Include(t => t.SastanakTip).Include(o => o.Odjeljenje)
                .Where(x => x.SastanakId == SastanakId && x.OrganizatorId==roditelj.KorisnikId).FirstOrDefault();// sastanak kojeg je kreirao logirani korisnik
            if (s==null)
            {
                return RedirectToAction("Index", "Sastanak");
            }
            SastanakRoditelj sr= db._SastanakRoditelj
                .Where(x => x.SastanakId == SastanakId && x.RoditeljId==roditelj.KorisnikId).FirstOrDefault();// sastanak kojeg je kreirao logirani korisnik
            if (sr == null)
            {
                return RedirectToAction("Index", "Sastanak");
            }
            List<Ucenik> ucenici = db._Ucenik.Where(u => u.RoditeljId == roditelj.KorisnikId).ToList();
            SastanakUrediVM vm = new SastanakUrediVM()
            {
                DatumObjave = s.DatumObavijest,
                SastanakId = s.SastanakId,
                RoditeljID=roditelj.KorisnikId,
                SastankTip = s.SastanakTip.Naziv,
                Komentar =sr==null?" ":sr.Komentar ,
                DatumSastanka = s == null ? DateTime.Now : s.DatumSastanka ,
                Naziv = s.Naziv,
                Opis = s.Opis,
                Ucenici = ucenici.Select(x => new SelectListItem() { Value = x.KorisnikId.ToString(), Text = x.Ime + " " + x.Prezime }).ToList()





            };

            return View(vm);
        }
        public IActionResult SnimiEdit(SastanakUrediVM vm)
        {

            SastanakTip st = db._SastanakTip.Where(t => t.Naziv == "Informacije").FirstOrDefault();

            Sastanak s = db._Sastanak.Where(x => x.SastanakId == vm.SastanakId).FirstOrDefault();

            s.DatumObavijest = vm.DatumObjave;
            s.Naziv = vm.Naziv;
            s.Opis = vm.Opis;
            s.OrganizatorId = vm.RoditeljID;
            s.DatumSastanka = vm.DatumSastanka;
            s.NastavnikId = db._UpisUOdjeljenje.Include(o => o.Odjeljenje).Where(o => o.UcenikId == vm.UcenikId).FirstOrDefault().Odjeljenje.RazrednikId;
            s.SastanakTipId = st.SastanakTipId;
            s.OdjeljenjeId = db._UpisUOdjeljenje.Where(o => o.UcenikId == vm.UcenikId).FirstOrDefault().OdjeljenjeId;


             db._Sastanak.Update(s);
            SastanakRoditelj sr = db._SastanakRoditelj.Where(x => x.SastanakId == vm.SastanakId).FirstOrDefault();
            sr.Komentar = vm.Komentar;
            sr.RoditeljId = vm.RoditeljID;

            sr.SastanakId = s.SastanakId;

            



            db._SastanakRoditelj.Update(sr);
            db.SaveChanges();
            return RedirectToAction("Index", "Sastanak");
        }

        public IActionResult Obrisi(int SastanakId)
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            Roditelj roditelj = db._Roditelj.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).Include(r => r.KorisnickiNalog).FirstOrDefault();
          
            SastanakRoditelj sr = db._SastanakRoditelj.Where(x => x.SastanakId == SastanakId && x.RoditeljId == roditelj.KorisnikId).FirstOrDefault();
            if (sr == null)
            {
                return RedirectToAction("Index", "Sastanak");
            }
            db._SastanakRoditelj.Remove(sr);
            Sastanak s = db._Sastanak.Include(t => t.SastanakTip).Include(o => o.Odjeljenje).Where(x => x.SastanakId == SastanakId ).FirstOrDefault();
            db._Sastanak.Remove(s);
            db.SaveChanges();

            return RedirectToAction("Index","Sastanak");
        }
    }
}