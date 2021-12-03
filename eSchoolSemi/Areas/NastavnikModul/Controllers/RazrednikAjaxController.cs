using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.NastavnikModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.NastavnikModul.Controllers
{
    [Area("NastavnikModul")]
    [Autorizacija(false, true, false, false)]
    public class RazrednikAjaxController : Controller
    {
        private MojContext _db;
        public RazrednikAjaxController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index(int OdrzaniCasDetaljiID)
        {
            OdrzanCasDetalji O = _db._OdrzanCasDetalji.Find(OdrzaniCasDetaljiID);

            return PartialView(O);
        }
        public IActionResult Snimi(OdrzanCasDetalji O)
        {
            OdrzanCasDetalji N = _db._OdrzanCasDetalji.Find(O.OdrzanCasDetaljiId);
            N.Ocjena = O.Ocjena;
            N.Opravdano = O.Opravdano;
            N.Odsutan = O.Odsutan;

            _db._OdrzanCasDetalji.Update(N);
            _db.SaveChanges();

            int _OdjeljnjeID = _db._OdrzanCas.Where(x => x.OdrzanCasId == N.OdrzanCasId).First().OdjeljenjeID;


            return RedirectToAction("Edit", "Razrednik", new { OdrzaniCasID = N.OdrzanCasId, OdjeljenjeID = _OdjeljnjeID });
        }
        public IActionResult VladanjeEdit(int UcenikID, int _OdjeljenjeID)
        {
            Ucenik U = _db._Ucenik.Find(UcenikID);

            AjaxVladanjeVM VM = new AjaxVladanjeVM {
                OdjeljenjeID = _OdjeljenjeID,
                UcenikID = U.KorisnikId,
                Vladanje = U.Vladanje,
                Ime = U.Ime + " " + U.Prezime
            };

            return View(VM);
        }
        public IActionResult VladanjeSnimi(AjaxVladanjeVM VM)
        {
            Ucenik U = _db._Ucenik.Find(VM.UcenikID);
            U.Vladanje = VM.Vladanje;

            _db._Ucenik.Update(U);
            _db.SaveChanges();

            return RedirectToAction("Vladanje", "Razrednik", new { OdjeljenjeID = VM.OdjeljenjeID });
        }
    }
}