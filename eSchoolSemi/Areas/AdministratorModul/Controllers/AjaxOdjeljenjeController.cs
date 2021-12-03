using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class AjaxOdjeljenjeController : Controller
    {
        private MojContext _context;
        public AjaxOdjeljenjeController(MojContext context) => _context = context;

        public IActionResult Index(int odjeljenjeId)
        {
            Odjeljenje prikazOdljenje = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == odjeljenjeId);

            List<UpisUOdjeljenje> upisi = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == prikazOdljenje.OdjeljenjeId).Include(x => x.Ucenik).ToList();

            AjaxPrikazVM uceniciOdljenja = new AjaxPrikazVM
            {
                OdjeljenjeID= odjeljenjeId,

                Ucenici = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == prikazOdljenje.OdjeljenjeId).Select(x => new AjaxPrikazVM.Row
                {

                    BrojUDnevniku = x.BrojUDnevniku,
                    ImePrezimeUcenika = x.Ucenik.Ime + " " + x.Ucenik.Prezime,
                    UcenikID = x.UcenikId
                }).ToList()
            };

            uceniciOdljenja.Ucenici = uceniciOdljenja.Ucenici.OrderBy(x => x.BrojUDnevniku);


            return PartialView(uceniciOdljenja);
        }

        [HttpGet]
        public IActionResult DetaljiUcenik(int UcenikId) {

            Ucenik nekiUcenik= _context._Ucenik.Where(x => x.KorisnikId == UcenikId).Include(x => x.Roditelj).Include(x => x.MjestoRodenja).FirstOrDefault();
            string ImePrezimRoditelja = "Prazno";
            if (nekiUcenik.RoditeljId!=null)
            {
                ImePrezimRoditelja = nekiUcenik.Roditelj.Ime + " " + nekiUcenik.Roditelj.Prezime;
            }

            var trenutnaGodina = _context._GodinaStudija.First(x => x.TrenutnaGodina == true);

            var treutnoOdjeljenje = from x in _context._Odjeljenje
                                    join s in _context._UpisUOdjeljenje on x.OdjeljenjeId equals s.OdjeljenjeId
                                    where s.UcenikId == nekiUcenik.KorisnikId && x.GodinaStudijaId == trenutnaGodina.GodinaStudijaId
                                    select x.Oznaka;

            string Odjeljenje = treutnoOdjeljenje.FirstOrDefault();

            UcenikDetalj ucenik = new UcenikDetalj {

                Slika=nekiUcenik.KorisnickaSlika,
                ImeIprezime=nekiUcenik.Ime+" "+nekiUcenik.Prezime,
                DatumRodjenja=nekiUcenik.DatumRodenja.ToShortDateString(),
                Roditelj=ImePrezimRoditelja,
                MjestoPrebivalista=nekiUcenik.MjestoRodenja.Naziv,
                TrenutnoUpisanoOdjeljenje = (!String.IsNullOrEmpty(Odjeljenje)) ? Odjeljenje : "Nije upisan u trenutnu akademsku godinu!",
                 Odjeljenja = _context._UpisUOdjeljenje.Where(x=>x.UcenikId==UcenikId).Select(x => new UcenikDetalj.Row
                 {

                     Oznaka = x.Odjeljenje.Oznaka,
                     GodinaStudija = x.Odjeljenje.GodinaStudija.Godina
                 })
            };

            return PartialView(ucenik);
        }



        public IActionResult PromjeniOdjeljenje(int OdjeljenjeId,int UcenikId) {


            var odjeljenje = _context._Odjeljenje.First(x => x.OdjeljenjeId == OdjeljenjeId);
            var ucenik = _context._Ucenik.First(x => x.KorisnikId == UcenikId);

            var Odjejlenja = _context._Odjeljenje.Where(x =>x.GodinaStudijaId==odjeljenje.GodinaStudijaId && x.OdjeljenjeId!=odjeljenje.OdjeljenjeId).ToList();

            Odjejlenja = Odjejlenja.OrderBy(x => x.Oznaka).ToList();

            AjaxUcenikOdjeljenjeVM spisakOdjeljenja = new AjaxUcenikOdjeljenjeVM {
                OdjeljenjeID = odjeljenje.OdjeljenjeId,
                UcenikId=ucenik.KorisnikId,
                Naziv=ucenik.Prezime+" "+ucenik.Ime,
                Odjeljenja= Odjejlenja.Select(x=>new SelectListItem {

                    Value=x.OdjeljenjeId.ToString(),
                    Text=x.Oznaka

                }).ToList()

            };




            return PartialView(spisakOdjeljenja);
        }

        public IActionResult SnimiPrebaceno(AjaxUcenikOdjeljenjeVM ucenik) {

            if (ModelState.IsValid)
            {
                var odjeljenje = _context._Odjeljenje.FirstOrDefault(x => x.UcenikID == ucenik.UcenikId);
                if (odjeljenje != null) {

                    odjeljenje.UcenikID = null;
                    _context.Update(odjeljenje);
                }
                    

                var upisUcenik = _context._UpisUOdjeljenje.First(x => x.UcenikId == ucenik.UcenikId && x.OdjeljenjeId == ucenik.OdjeljenjeID);


                var staroOdjeljenje = upisUcenik.OdjeljenjeId;

                upisUcenik.OdjeljenjeId = ucenik.OdjeljenjePrebacenoId;
                _context.Update(upisUcenik);
                _context.SaveChanges();

                var lista1 = Broj_U_Dnevniku(ucenik.OdjeljenjePrebacenoId);
                var list2 = Broj_U_Dnevniku(staroOdjeljenje);

                _context.SaveChanges();
            }


            return RedirectToAction(nameof(Index),new { odjeljenjeId=ucenik.OdjeljenjeID });

        }

       
        public IEnumerable<UpisUOdjeljenje> Broj_U_Dnevniku(int odjeljenjeId) {


            IEnumerable<UpisUOdjeljenje> listaUcenika = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == odjeljenjeId).Include(x=>x.Ucenik).ToList();
            listaUcenika = listaUcenika.OrderBy(x => x.Ucenik.Prezime);

            int brojac = 1;

            foreach (var temp in listaUcenika)
            {
                temp.BrojUDnevniku = brojac;
                ++brojac;
            }

           

            return listaUcenika;
        }

        public IActionResult ProvjeriKapacitet(int OdjeljenjePrebacenoId) {

            if (OdjeljenjePrebacenoId != 0) {
                var odjeljenje = _context._Odjeljenje.First(x => x.OdjeljenjeId == OdjeljenjePrebacenoId);
                var brojUpisani =_context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == odjeljenje.OdjeljenjeId).Count();
                if (brojUpisani >= odjeljenje.Kapacitet)
                    return Json("Kapacitet tog odjeljenja je dosegnut!");
                return Json(true);

             }


            return Json(true);
        }
    }
}