using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Controllers;
using eSchoolSemi.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using eSchoolSemi.Web.Helper;

namespace eSchoolSemi.Web.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool ucenici, bool nastavnici, bool roditelji, bool administrator)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { ucenici, nastavnici, roditelji, administrator };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool ucenici, bool nastavnici,bool roditelji,bool administrator)
        {
            _ucenici = ucenici;
            _nastavnici = nastavnici;
            _roditelji = roditelji;
            _administrator = administrator;
        }
        private readonly bool _ucenici;
        private readonly bool _nastavnici;
        private readonly bool _roditelji;
        private readonly bool _administrator;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalog k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Index", "DvofaktorskaAutentitfikacija", new { @area = "" });
                return;
            }


            MojContext _context = filterContext.HttpContext.RequestServices.GetService<MojContext>();


            if (_ucenici && _context._Ucenik.Any(s => s.KorisnickiNalogID == k.KorisnickiNalogID))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }


            if (_nastavnici && _context._Nastavnik.Any(s => s.KorisnickiNalogID == k.KorisnickiNalogID))
            {
                await next();//ok - ima pravo pristupa
                return;
            }
            
            if (_roditelji && _context._Roditelj.Any(s => s.KorisnickiNalogID == k.KorisnickiNalogID))
            {
                await next();//ok - ima pravo pristupa
                return;
            }

            
            if (_administrator && _context.Administrators.Any(s => s.KorisnickiNalogID == k.KorisnickiNalogID))
            {
                await next();//ok - ima pravo pristupa                
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}