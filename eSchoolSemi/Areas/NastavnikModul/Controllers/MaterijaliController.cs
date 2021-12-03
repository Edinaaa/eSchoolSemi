using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.NastavnikModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.NastavnikModul.Controllers
{
    [Area("NastavnikModul")]
    [Autorizacija(false, true, false, false)]
    public class MaterijaliController : Controller
    {
        private MojContext _db;
        private IHostingEnvironment _he;
        public MaterijaliController(MojContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from M in _db._Materijal
                        join N in _db._Nastavnik on M.NastavnikId equals N.KorisnikId
                        join A in _db._Angazovan on N.KorisnikId equals A.NastavnikId
                        join K in _db._Korisnik on N.KorisnikId equals K.KorisnikId
                        let FullName = K.Ime + " " + K.Prezime
                        where N.KorisnikId == Nastavnik.KorisnikId
                        orderby M.DatumObjave descending
                        select new { FullName, M.MaterijalId, M.Naziv, M.DatumObjave, M.FileURL, M.FilePath };

            List<MaterijaliIndexVM> VM = query.Distinct().Select(x => new MaterijaliIndexVM
            {
                Autor = x.FullName,
                Naslov = x.Naziv,
                DatumObjave = x.DatumObjave,
                MaterijalID = x.MaterijalId,
                FileURL = x.FileURL,
                FilePath = x.FilePath,
                Extension = Path.GetExtension(x.FilePath)
            }).ToList();

            return View(VM);
        }

        public IActionResult Detalji(int MaterijaliID)
        {
            Materijal M = _db._Materijal.Find(MaterijaliID);

            MaterijalDetaljVM VM = new MaterijalDetaljVM
            {
                DatumObjave = M.DatumObjave,
                MaterijalID = M.MaterijalId,
                Sadrzaj = M.Sadrzaj,
                Naziv = M.Naziv
            };

            return View(VM);
        }

        public IActionResult Dodaj()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            var Nastavnik = _db._Nastavnik.First(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            var query = from NNP in _db._NastavniPlanPredmet
                        join A in _db._Angazovan on NNP.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                        join K in _db._Korisnik on A.NastavnikId equals K.KorisnikId
                        join P in _db._Predmet on NNP.PredmetId equals P.PredmetId
                        where A.NastavnikId == Nastavnik.KorisnikId
                        select new { P.PredmetId, P.Naziv };

            MaterijaliDodajVM VM = new MaterijaliDodajVM
            {
                NastavnikID = Nastavnik.KorisnikId,
                Predmeti = query.Distinct().Select(x => new SelectListItem { Value = x.PredmetId.ToString(), Text = x.Naziv }).ToList()
            };
            return View(VM);
        }

        public IActionResult Snimi(MaterijaliDodajVM VM, IFormFile FileURL)
        {
            var query = from NNP in _db._NastavniPlanPredmet
                        join A in _db._Angazovan on NNP.NastavniPlanPredmetId equals A.NastavniPlanPredmetId
                        where A.NastavnikId == VM.NastavnikID && NNP.PredmetId == VM.PredmetID
                        select NNP.NastavniPlanPredmetId;

            var filePath = Path.Combine(_he.WebRootPath + "\\Images", FileURL.FileName);
            FileURL.CopyTo(new FileStream(filePath, FileMode.Create));

            Materijal M = new Materijal
            {
                NastavnikId = VM.NastavnikID,
                DatumObjave = DateTime.Now,
                Naziv = VM.Naslov,
                Sadrzaj = VM.Sadrzaj,
                PredmetId = VM.PredmetID,
                FileURL = FileURL.FileName,
                FilePath = Path.Combine("\\Images", FileURL.FileName)
            };


            _db.Add(M);
            _db.SaveChanges();

            return RedirectToAction("Index", "Materijali");
        }

        public IActionResult Brisi(int MaterijaliID)
        {

            Materijal M = _db._Materijal.Find(MaterijaliID);
            _db.Remove(M);
            _db.SaveChanges();

            return RedirectToAction("Index", "Materijali");
        }

    }
}