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
    public class RasporedController : Controller
    {

        private MojContext _context;
        public RasporedController(MojContext context) => _context = context;

        public IActionResult Index()
        {
            IEnumerable<Raspored> listaRasporeda = _context.Raspored.ToList();
            return View(listaRasporeda);
        }

        public IActionResult DodajRaspored(int OdjeljenjeId)
        {
            var Odjeljenje = _context._Odjeljenje.First(x => x.OdjeljenjeId == OdjeljenjeId);

            RasporedDodajVM noviRaspored = new RasporedDodajVM {

                OdljenjeID=Odjeljenje.OdjeljenjeId,
                Oznaka=Odjeljenje.Oznaka

            };

            
           

            return PartialView(noviRaspored);
        }


        public IActionResult SnimiRaspored(RasporedDodajVM obj) {

            Raspored rasporedZaSnimit = new Raspored
            {

                Naziv = obj.RasporedNaziv,
                OdjeljenjeID = obj.OdljenjeID
            };

            _context.Raspored.Add(rasporedZaSnimit);
            _context.SaveChanges();

            return RedirectToAction("Detalji", "Odjeljenje",new { id =obj.OdljenjeID});
        }


        public IActionResult UrediRaspored(int id) {

            Raspored raspored = _context.Raspored.FirstOrDefault(x => x.RasporedID == id);

            RasporedDetaljVM viewModel = new RasporedDetaljVM
            {
                RasporedID = raspored.RasporedID,
                OdjeljenjeId=raspored.OdjeljenjeID,

                Predmet = _context._Predmet.Select(x => new SelectListItem {

                    Text = x.Naziv,
                    Value = x.PredmetId.ToString()
                }).ToList(),

                Dan = _context.Dan.Select(x => new SelectListItem
                {

                    Text = x.Naziv,
                    Value = x.DanID.ToString()
                }).ToList(),

                PocetakCasa = _context.PocetakCasa.Select(x => new SelectListItem
                {

                    Text = x.Pocetak,
                    Value = x.PocetakCasaID.ToString()
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult DodajDetalj(RasporedDetaljVM vM)
        {
            if (vM.PredmetId == 0) {
                
                RasporedDetalj zaIzabrisat = _context.RasporedDetalj.
                                            FirstOrDefault(x => x.RasporedID == vM.RasporedID &&
                                                           x.DanID == vM.DanId &&
                                                           x.PocetakCasaId == vM.PocetakCasaId);

                _context.RasporedDetalj.Remove(zaIzabrisat);
                return RedirectToAction("UrediRaspored", new { id = vM.RasporedID });
            }


            RasporedDetalj promjena = _context.RasporedDetalj.
                                            FirstOrDefault(x => x.RasporedID == vM.RasporedID &&
                                                           x.DanID == vM.DanId && 
                                                           x.PocetakCasaId == vM.PocetakCasaId);


            if (promjena != null)
            {
                promjena.PredmetID = vM.PredmetId;
                _context.RasporedDetalj.Update(promjena);
            }

            else
            {
                RasporedDetalj detalji = new RasporedDetalj
                {
                    RasporedID = vM.RasporedID,
                    PredmetID = vM.PredmetId,
                    DanID = vM.DanId,
                    PocetakCasaId = vM.PocetakCasaId
                };

                _context.RasporedDetalj.Add(detalji);
            }

            _context.SaveChanges();

            return RedirectToAction("UrediRaspored",new {id=vM.RasporedID});
        }


        public IActionResult CitavRaspored(int RasporedID) {

            PregledRasporedVM listaPredmeta = new PregledRasporedVM {

                listaPredmeta=_context.RasporedDetalj.Where(x=>x.RasporedID==RasporedID).
                Select(x=>new PregledRasporedVM.Row {
                    Predmet=x.Predmet.Naziv,
                    Dan=x.Dan.Naziv,
                    PocetakCasa=x.PocetakCasa.Pocetak
                }).ToList()
            };

            return PartialView(listaPredmeta);
        }

        public IActionResult CitavRasporedUredi(int RasporedID)
        {

            PregledRasporedVM listaPredmeta = new PregledRasporedVM
            {

                listaPredmeta = _context.RasporedDetalj.Where(x => x.RasporedID == RasporedID).
                Select(x => new PregledRasporedVM.Row
                {
                    Predmet = x.Predmet.Naziv,
                    Dan = x.Dan.Naziv,
                    PocetakCasa = x.PocetakCasa.Pocetak
                }).ToList()
            };

            return PartialView(listaPredmeta);
        }
    }
}