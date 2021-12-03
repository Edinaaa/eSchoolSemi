using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.UcenikModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.UcenikModul.Controllers
{
    [Area("UcenikModul")]
    [Autorizacija(true, false, false, false)]
    public class MaterijaliController : Controller
    {
        private MojContext db;
        private IHostingEnvironment he;

        public MaterijaliController(MojContext _db, IHostingEnvironment _he) { db = _db; he = _he; }
        public IActionResult Index()
        {
            KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            UpisUOdjeljenje uuo = db._UpisUOdjeljenje
                .Include(o=>o.Odjeljenje)
                .Where(x => x.Ucenik.KorisnickiNalogID == k.KorisnickiNalogID).FirstOrDefault();

            MaterijalIndexVM vm = new MaterijalIndexVM() {

                Rows=db._Materijal
                .Include(p=>p.Predmet).Include(n=>n.Nastavnik)
                .Where(m=>m.NastavniPlanPredmet.NastavniPlanId==uuo.Odjeljenje.NastavniPlanId)
                .Select(x=> new MaterijalIndexVM.Row() {

                    DatumObjave=x.DatumObjave,
                    MaterijalID=x.MaterijalId,
                    Nastavnik=x.Nastavnik.Ime+" "+x.Nastavnik.Prezime,
                    Naziv=x.Naziv,
                    FilePath=x.FilePath,
                    Predmet=x.Predmet.Naziv


                }).ToList()

            };
            return View(vm);
        }

    
    }
}