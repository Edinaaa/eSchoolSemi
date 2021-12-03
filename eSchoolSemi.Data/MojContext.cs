using eSchool.Data.Models;
using eSchoolSemi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace eSchoolSemi.Data
{
    public class MojContext:DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Odjeljenje>().HasOne(p => p.Raspored).WithOne(i => i.Odjeljenje).HasForeignKey<Raspored>(b => b.OdjeljenjeID);
           
        }



        public DbSet<Angazovan> _Angazovan { get; set; }
        public DbSet<GodinaStudija> _GodinaStudija { get; set; }
        public DbSet<Grad> _Grad { get; set; }
        public DbSet<Korisnik> _Korisnik { get; set; }
        public DbSet<Materijal> _Materijal { get; set; }
        public DbSet<Nastavnik> _Nastavnik { get; set; }
        public DbSet<NastavniPlan> _NastavniPlan { get; set; }
        public DbSet<NastavniPlanPredmet> _NastavniPlanPredmet { get; set; }
        public DbSet<Obavjestenje> _Obavjestenje { get; set; }
        public DbSet<Odjeljenje> _Odjeljenje { get; set; }
        public DbSet<OdrzanCas> _OdrzanCas { get; set; }
        public DbSet<OdrzanCasDetalji> _OdrzanCasDetalji { get; set; }
        public DbSet<Predmet> _Predmet { get; set; }
        public DbSet<Roditelj> _Roditelj { get; set; }
        public DbSet<Sastanak> _Sastanak { get; set; }
        public DbSet<SastanakRoditelj> _SastanakRoditelj { get; set; }


        public DbSet<SastanakTip> _SastanakTip { get; set; }
        public DbSet<TipObavijesti> _TipObavijesti { get; set; }
        public DbSet<Ucenik> _Ucenik { get; set; }
        public DbSet<TipOcjene> _TipOcjene { get; set; }
        public DbSet<UpisUOdjeljenje> _UpisUOdjeljenje { get; set; }

        public DbSet<KorisnickiNalog> korisnickiNalogs { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<AutorizacijskiToken> AutorizacijskiToken { get; set; }

        public DbSet<ObavjestenjeOdjeljenje> ObavjestenjeOdjeljenje { get; set; }

        public DbSet<ZakljucnaOcjena> ZakljucnaOcjena { get; set; }

        public DbSet<Raspored> Raspored { get; set; }

        public DbSet<RasporedDetalj> RasporedDetalj { get; set; }

        public DbSet<Dan> Dan { get; set; }

        public DbSet<PocetakCasa> PocetakCasa { get; set; }

        public DbSet<Razred> Razred { get; set; }




    }
}
