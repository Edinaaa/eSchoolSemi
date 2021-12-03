using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class ObavjestiController : Controller
    {
        private MojContext _context;


        public ObavjestiController(MojContext context) => _context = context;

        public IActionResult Index()
        {
            ObavjestFilterVm filter = new ObavjestFilterVm();

            filter.TipObavjesti = _context._TipObavijesti.Select(x => new SelectListItem
            {

                Value = x.TipObavijestiId.ToString(),
                Text = x.Tip
            }).ToList();

            filter.Nastavnik= _context._Nastavnik.Select(x => new SelectListItem
            {

                Value = x.KorisnikId.ToString(),
                Text = x.Ime+" "+x.Prezime
            }).ToList();

            filter.DatumDo = DateTime.Now;
            filter.DatumOd = DateTime.Now;






            return View(filter);
        }

        public async Task<IActionResult> ObavjestiAjax(string Naslov,int? NastavnikID, int? Tip, DateTime? DatumOd, DateTime? DatumDo, int? page) {



            ObavjestiIndexVM obavjestiIndexVM = new ObavjestiIndexVM
            {


                Obavjesti = from x in _context._Obavjestenje
                            join s in _context._TipObavijesti on x.TipObavijestiId equals s.TipObavijestiId
                            join p in _context._Nastavnik on x.NastavnikID equals p.KorisnikId into ps
                            from p in ps.DefaultIfEmpty()
                            select new ObavjestiIndexVM.Row
                            {
                                ObavjestID = x.ObavjestenjeId,
                                Naslov = x.Naslov,
                                Sadrzaj = x.Sadrzaj,
                                TipObavjesti = s.Tip,
                                DatumPostavljanja = x.DatumObavjestenja.ToLongDateString(),
                                Autor = (x.NastavnikID == null) ? "Administracija" :p.Ime+' '+p.Prezime

                            }
            };


            if (!String.IsNullOrEmpty(Naslov))
            {

                obavjestiIndexVM.Obavjesti = obavjestiIndexVM.Obavjesti.Where(x => x.Naslov.Contains(Naslov));
            }


            if (NastavnikID != null)
            {

                var nastavnikSearch = _context._Nastavnik.First(x => x.KorisnikId == NastavnikID);

                string Naziv = nastavnikSearch.Ime + " " + nastavnikSearch.Prezime;

                obavjestiIndexVM.Obavjesti = obavjestiIndexVM.Obavjesti.Where(x => x.Autor == Naziv);

            }

            if (Tip != null)
            {
                TipObavijesti temp = _context._TipObavijesti.First(x => x.TipObavijestiId == Tip);

                obavjestiIndexVM.Obavjesti = obavjestiIndexVM.Obavjesti.Where(x => x.TipObavjesti == temp.Tip);

            }

            if (DatumOd!=null) {

               
                
                obavjestiIndexVM.Obavjesti = obavjestiIndexVM.Obavjesti.Where(x =>DateTime.Parse(x.DatumPostavljanja)>= DatumOd);
            }

            if (DatumDo != null)
            {

               

                obavjestiIndexVM.Obavjesti = obavjestiIndexVM.Obavjesti.Where(x => DateTime.Parse(x.DatumPostavljanja) >= DatumDo);
            }

            obavjestiIndexVM.Obavjesti = obavjestiIndexVM.Obavjesti.OrderByDescending(x => DateTime.Parse(x.DatumPostavljanja));

            int pageSize = 8;
            return PartialView(await PaginatedList<ObavjestiIndexVM.Row>.CreateAsync(obavjestiIndexVM.Obavjesti.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult DodajObavjest()
        {
            DodajObavjestVM novaObavijest = new DodajObavjestVM
            {
                //promjeni ovde
                Razred = _context.Razred.Select(x => new SelectListItem
                {

                    Value = x.RazredId.ToString(),
                    Text = x.OpisRazreda
                }).ToList(),

                TipObavjesti = _context._TipObavijesti.Select(x => new SelectListItem
                {


                    Value = x.TipObavijestiId.ToString(),
                    Text = x.Tip

                }).ToList()
            };



            return View(novaObavijest);
        }

        public IActionResult Snimi(DodajObavjestVM obj)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();

            Administrator admin = _context.Administrators.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            Obavjestenje obavjest = new Obavjestenje {

                Naslov = obj.Naslov,
                Sadrzaj = obj.Sadrzaj,
                TipObavijestiId = obj.TipObavjestiID,
                AdministatorID = admin.KorisnikId,
                DatumObavjestenja = DateTime.Today
            };

            
            _context._Obavjestenje.Add(obavjest);
            _context.SaveChanges();


            Obavjestenje temp = _context._Obavjestenje.FirstOrDefault(x => x.Sadrzaj == obj.Sadrzaj && x.Naslov == obj.Naslov);
            var trenutnaGodina = _context._GodinaStudija.First(x => x.TrenutnaGodina == true).GodinaStudijaId;

            List<Odjeljenje> odjeljenjea = new List<Odjeljenje>();

            if (obj.razredID == null) {

                odjeljenjea = _context._Odjeljenje.Where(x=>x.GodinaStudijaId==trenutnaGodina).ToList();
            }

            else
            {
                //promjeni ovdje
                odjeljenjea = _context._Odjeljenje.Where(x => x.RazredID == obj.razredID&& x.GodinaStudijaId == trenutnaGodina).ToList();
            }

            if (odjeljenjea != null) {

                foreach (var item in odjeljenjea)
                {
                    ObavjestenjeOdjeljenje novo = new ObavjestenjeOdjeljenje
                    {

                        OdjeljenjeID = item.OdjeljenjeId,
                        ObavjestenjeID = temp.ObavjestenjeId
                    };

                    _context.ObavjestenjeOdjeljenje.Add(novo);
                }

                _context.SaveChanges();
            }
            

            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int ObavjestID) {

            Obavjestenje obavjestenje = _context._Obavjestenje.FirstOrDefault(x => x.ObavjestenjeId == ObavjestID);

            Nastavnik nastavnik = _context._Nastavnik.FirstOrDefault(x => x.KorisnikId == obavjestenje.NastavnikID);

            var odljenjenja = _context.ObavjestenjeOdjeljenje.Where(x => x.ObavjestenjeID == ObavjestID);

            var temp = from x in _context._Odjeljenje
                       join s in odljenjenja on x.OdjeljenjeId equals s.OdjeljenjeID
                       select x.Oznaka;






            ObavjestiIndexVM.Row obavjest = new ObavjestiIndexVM.Row {

                Naslov = obavjestenje.Naslov,
                Sadrzaj = obavjestenje.Sadrzaj,
                TipObavjesti = _context._TipObavijesti.First(x => x.TipObavijestiId == obavjestenje.TipObavijestiId).Tip,
                Autor = (nastavnik != null) ? nastavnik.Ime + " " + nastavnik.Prezime : "Administracija",
                DatumPostavljanja = obavjestenje.DatumObavjestenja.ToLongDateString()

            };

            foreach (var item in temp)
            {
                obavjest.ZaKoga+=item+" ";
            }

            return View(obavjest);
        }


        public IActionResult Uredi(int ObavjestID)
        {

            Obavjestenje Obavjest = _context._Obavjestenje.First(x => x.ObavjestenjeId == ObavjestID);

            

            if (Obavjest == null)
            {
                return NotFound();
            }           



            DodajObavjestVM urediObavjest = new DodajObavjestVM
            {
                ObavjestID=Obavjest.ObavjestenjeId,
                Naslov = Obavjest.Naslov,
                Sadrzaj = Obavjest.Sadrzaj,
                TipObavjestiID=Obavjest.TipObavijestiId,             
                
                

                TipObavjesti = _context._TipObavijesti.Select(x => new SelectListItem
                {


                    Value = x.TipObavijestiId.ToString(),
                    Text = x.Tip

                }).ToList()
            };

            return View(urediObavjest);
        }


        public IActionResult SnimiUredjeno(DodajObavjestVM obj) {

            Obavjestenje obavjest = _context._Obavjestenje.First(x => x.ObavjestenjeId == obj.ObavjestID);

            obavjest.Naslov = obj.Naslov;
            obavjest.TipObavijestiId = obj.TipObavjestiID;
            obavjest.Sadrzaj = obj.Sadrzaj;

            _context._Obavjestenje.Update(obavjest);
            _context.SaveChanges();




            return RedirectToAction(nameof(Index));
        }

        public IActionResult Obrisi(int ObavjestId) {

            List<ObavjestenjeOdjeljenje> obavjestOdljenjenja = _context.ObavjestenjeOdjeljenje.Where(x => x.ObavjestenjeID == ObavjestId).ToList();

            if (obavjestOdljenjenja!=null)
            {
                foreach (var obavjest in obavjestOdljenjenja)
                {
                    _context.ObavjestenjeOdjeljenje.Remove(obavjest);
                }

                _context.SaveChanges();
            }


            var obavjestZaIzbrisat = _context._Obavjestenje.First(x => x.ObavjestenjeId == ObavjestId);

            _context._Obavjestenje.Remove(obavjestZaIzbrisat);
            _context.SaveChanges();

            return RedirectToAction(nameof(ObavjestiAjax));
        }

    }
}