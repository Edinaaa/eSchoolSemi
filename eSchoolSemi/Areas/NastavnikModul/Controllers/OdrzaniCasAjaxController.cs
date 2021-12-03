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
    public class OdrzaniCasAjaxController : Controller
    {
        private MojContext _db;
        public OdrzaniCasAjaxController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index(int OdrzaniCasDetaljiID)
        {
            OdrzanCasDetalji O = _db._OdrzanCasDetalji.First(x => x.OdrzanCasDetaljiId == OdrzaniCasDetaljiID);
            var query = from K in _db._Korisnik
                        join UO in _db._UpisUOdjeljenje on K.KorisnikId equals UO.UcenikId
                        join OO in _db._OdrzanCasDetalji on UO.UpisUOdjeljenjeId equals OO.UpisUOdjeljenjeId
                        let FullName = K.Ime + " " + K.Prezime
                        where OO.OdrzanCasDetaljiId == O.OdrzanCasDetaljiId
                        select new { FullName };
            OdrzaniCasDetaljiEditVM VM = new OdrzaniCasDetaljiEditVM
            {
                Ocjena = O.Ocjena ?? 0,
                OdrzanCasDetaljiId = O.OdrzanCasDetaljiId,
                Odsutan = O.Odsutan,
                Opravdano = O.Opravdano,
                Ucenik = query.Select(x=> x.FullName).First()
            };

            return PartialView(VM);
        }
        public IActionResult Snimi(OdrzanCasDetalji O)
        {
            OdrzanCasDetalji N = _db._OdrzanCasDetalji.Find(O.OdrzanCasDetaljiId);
            N.Ocjena = O.Ocjena;
            N.Odsutan = O.Odsutan;

            int _OdjeljnjeID = _db._OdrzanCas.Where(x => x.OdrzanCasId == N.OdrzanCasId).First().OdjeljenjeID;

            _db._OdrzanCasDetalji.Update(N);
            _db.SaveChanges();

            return RedirectToAction("Edit","OdrzaniCas", new { OdrzaniCasID = N.OdrzanCasId, OdjeljenjeID = _OdjeljnjeID });
        }
    }
}