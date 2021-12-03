using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Areas.RoditeljModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.RoditeljModul.Controllers
{
    [Area("RoditeljModul")]
        [Autorizacija(false, false, true, false)]
    public class ObavijestController : Controller
    {
        
        
            private MojContext db;
            public ObavijestController(MojContext _db) { db = _db; }
            public IActionResult Index()
            {
                ObavijestIndexVM vm = new ObavijestIndexVM()
                {
                    Rows = db._Obavjestenje.Where(t=>t.TipObavijesti.Tip!="za ucenike" && t.TipObavijesti.Tip != "za nastavnike").Include(n => n.Nastavnik).Include(a => a.Administrator).Include(t => t.TipObavijesti).Select(x => new ObavijestIndexVM.Row()
                    {
                        ObavjestenjeId = x.ObavjestenjeId,
                        TipObavijesti = x.TipObavijestiId == null ? " " :x.TipObavijesti.Tip,
                        DatumObavjestenja = x.DatumObavjestenja,
                        Naslov = x.Naslov,
                        Autor = (x.NastavnikID == null) ? "Administracija" : x.Nastavnik.Ime + ' ' + x.Nastavnik.Prezime
                    }).ToList()
                };
                return View(vm);

            }
            public IActionResult Detalji(int ObavjestenjeId)
            {
                Obavjestenje x = db._Obavjestenje
                    .Include(n => n.Nastavnik).Include(a => a.Administrator).Include(t => t.TipObavijesti)
                    .Where(o => o.ObavjestenjeId == ObavjestenjeId).FirstOrDefault();
                ObavijestDetaljiVM vm = new ObavijestDetaljiVM()
                {
                    ObavjestenjeId = x.ObavjestenjeId,
                    TipObavijesti = x.TipObavijestiId == null ? " " : x.TipObavijesti.Tip,
                    Sadrzaj = x.Sadrzaj,
                    DatumObavjestenja = x.DatumObavjestenja,
                    Naslov = x.Naslov,
                    Autor = (x.NastavnikID == null) ? "Administracija" : x.Nastavnik.Ime + ' ' + x.Nastavnik.Prezime
                };

                return View(vm);

            }
        
    }
}