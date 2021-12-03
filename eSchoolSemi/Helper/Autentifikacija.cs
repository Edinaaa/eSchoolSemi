using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;

using eSchoolSemi.Web.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace eSchoolSemi.Web.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalog korisnik, bool snimiUCookie = false)
        {

          
            MojContext baza = context.RequestServices.GetService<MojContext>();


            string stariToken = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (stariToken != null)
            {
                AutorizacijskiToken obrisati = baza.AutorizacijskiToken.FirstOrDefault(x => x.Vrijednost == stariToken);
                if (obrisati != null)
                {
                    baza.AutorizacijskiToken.Remove(obrisati);
                    baza.SaveChanges();
                }
            }

            if (korisnik != null)
            {

                string token = Guid.NewGuid().ToString();
                baza.AutorizacijskiToken.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnickiNalogID = korisnik.KorisnickiNalogID,
                    VrijemeEvidentiranja = DateTime.Now
                });
                baza.SaveChanges();
                context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
        }

        public static string GetTrenutniToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<string>(LogiraniKorisnik);
        }

        public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
        {

            MojContext baza = context.RequestServices.GetService<MojContext>();

            string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (token == null)
                return null;

            return baza.AutorizacijskiToken
                .Where(x => x.Vrijednost == token)
                .Select(s => s.KorisnickiNalog)
                .SingleOrDefault();


        }

    }
}