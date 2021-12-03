using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_GodinaStudija",
                columns: table => new
                {
                    GodinaStudijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Godina = table.Column<string>(nullable: true),
                    TrenutnaGodina = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GodinaStudija", x => x.GodinaStudijaId);
                });

            migrationBuilder.CreateTable(
                name: "_Grad",
                columns: table => new
                {
                    GradId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grad", x => x.GradId);
                });

            migrationBuilder.CreateTable(
                name: "_Predmet",
                columns: table => new
                {
                    PredmetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Oznaka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Predmet", x => x.PredmetId);
                });

            migrationBuilder.CreateTable(
                name: "_SastanakTip",
                columns: table => new
                {
                    SastanakTipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SastanakTip", x => x.SastanakTipId);
                });

            migrationBuilder.CreateTable(
                name: "_TipObavijesti",
                columns: table => new
                {
                    TipObavijestiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipObavijesti", x => x.TipObavijestiId);
                });

            migrationBuilder.CreateTable(
                name: "_TipOcjene",
                columns: table => new
                {
                    TipOcjeneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipOcjene", x => x.TipOcjeneId);
                });

            migrationBuilder.CreateTable(
                name: "Dan",
                columns: table => new
                {
                    DanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dan", x => x.DanID);
                });

            migrationBuilder.CreateTable(
                name: "korisnickiNalogs",
                columns: table => new
                {
                    KorisnickiNalogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Zapamti = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korisnickiNalogs", x => x.KorisnickiNalogID);
                });

            migrationBuilder.CreateTable(
                name: "PocetakCasa",
                columns: table => new
                {
                    PocetakCasaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pocetak = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocetakCasa", x => x.PocetakCasaID);
                });

            migrationBuilder.CreateTable(
                name: "Razred",
                columns: table => new
                {
                    RazredId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojRazreda = table.Column<int>(nullable: false),
                    OpisRazreda = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razred", x => x.RazredId);
                });

            migrationBuilder.CreateTable(
                name: "_Korisnik",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumRodenja = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    GradId = table.Column<int>(nullable: true),
                    Ime = table.Column<string>(nullable: false),
                    KorisnickiNalogID = table.Column<int>(nullable: false),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    DatumZaposlenja = table.Column<DateTime>(nullable: true),
                    Titula = table.Column<string>(nullable: true),
                    Zvanje = table.Column<string>(nullable: true),
                    KorisnickaSlika = table.Column<byte[]>(nullable: true),
                    RoditeljId = table.Column<int>(nullable: true),
                    Vladanje = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Korisnik", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK__Korisnik__Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "_Grad",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Korisnik_korisnickiNalogs_KorisnickiNalogID",
                        column: x => x.KorisnickiNalogID,
                        principalTable: "korisnickiNalogs",
                        principalColumn: "KorisnickiNalogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Korisnik__Korisnik_RoditeljId",
                        column: x => x.RoditeljId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutorizacijskiToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickiNalogID = table.Column<int>(nullable: false),
                    Vrijednost = table.Column<string>(nullable: true),
                    VrijemeEvidentiranja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorizacijskiToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutorizacijskiToken_korisnickiNalogs_KorisnickiNalogID",
                        column: x => x.KorisnickiNalogID,
                        principalTable: "korisnickiNalogs",
                        principalColumn: "KorisnickiNalogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_NastavniPlan",
                columns: table => new
                {
                    NastavniPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GodinaStudijaId = table.Column<int>(nullable: true),
                    GodinaStudiranjaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Prebacen = table.Column<bool>(nullable: false),
                    RazredId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NastavniPlan", x => x.NastavniPlanId);
                    table.ForeignKey(
                        name: "FK__NastavniPlan__GodinaStudija_GodinaStudijaId",
                        column: x => x.GodinaStudijaId,
                        principalTable: "_GodinaStudija",
                        principalColumn: "GodinaStudijaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NastavniPlan_Razred_RazredId",
                        column: x => x.RazredId,
                        principalTable: "Razred",
                        principalColumn: "RazredId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Obavjestenje",
                columns: table => new
                {
                    ObavjestenjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministatorID = table.Column<int>(nullable: true),
                    AdministratorKorisnikId = table.Column<int>(nullable: true),
                    DatumObavjestenja = table.Column<DateTime>(nullable: false),
                    Naslov = table.Column<string>(nullable: true),
                    NastavnikID = table.Column<int>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    TipObavijestiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Obavjestenje", x => x.ObavjestenjeId);
                    table.ForeignKey(
                        name: "FK__Obavjestenje__Korisnik_AdministratorKorisnikId",
                        column: x => x.AdministratorKorisnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Obavjestenje__Korisnik_NastavnikID",
                        column: x => x.NastavnikID,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Obavjestenje__TipObavijesti_TipObavijestiId",
                        column: x => x.TipObavijestiId,
                        principalTable: "_TipObavijesti",
                        principalColumn: "TipObavijestiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_NastavniPlanPredmet",
                columns: table => new
                {
                    NastavniPlanPredmetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojCasova = table.Column<int>(nullable: false),
                    NastavniPlanId = table.Column<int>(nullable: true),
                    PredmetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NastavniPlanPredmet", x => x.NastavniPlanPredmetId);
                    table.ForeignKey(
                        name: "FK__NastavniPlanPredmet__NastavniPlan_NastavniPlanId",
                        column: x => x.NastavniPlanId,
                        principalTable: "_NastavniPlan",
                        principalColumn: "NastavniPlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NastavniPlanPredmet__Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Odjeljenje",
                columns: table => new
                {
                    OdjeljenjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GodinaStudijaId = table.Column<int>(nullable: true),
                    Kapacitet = table.Column<int>(nullable: false),
                    NastavniPlanId = table.Column<int>(nullable: true),
                    Oznaka = table.Column<string>(nullable: true),
                    Prebacen = table.Column<bool>(nullable: false),
                    RazredID = table.Column<int>(nullable: true),
                    RazrednikId = table.Column<int>(nullable: true),
                    UcenikID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Odjeljenje", x => x.OdjeljenjeId);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__GodinaStudija_GodinaStudijaId",
                        column: x => x.GodinaStudijaId,
                        principalTable: "_GodinaStudija",
                        principalColumn: "GodinaStudijaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__NastavniPlan_NastavniPlanId",
                        column: x => x.NastavniPlanId,
                        principalTable: "_NastavniPlan",
                        principalColumn: "NastavniPlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje_Razred_RazredID",
                        column: x => x.RazredID,
                        principalTable: "Razred",
                        principalColumn: "RazredId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__Korisnik_RazrednikId",
                        column: x => x.RazrednikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__Korisnik_UcenikID",
                        column: x => x.UcenikID,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Angazovan",
                columns: table => new
                {
                    AngazovanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NastavniPlanPredmetId = table.Column<int>(nullable: true),
                    NastavnikId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Angazovan", x => x.AngazovanId);
                    table.ForeignKey(
                        name: "FK__Angazovan__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Angazovan__Korisnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Materijal",
                columns: table => new
                {
                    MaterijalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumObjave = table.Column<DateTime>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileURL = table.Column<string>(nullable: true),
                    NastavniPlanPredmetId = table.Column<int>(nullable: true),
                    NastavnikId = table.Column<int>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    PredmetId = table.Column<int>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Materijal", x => x.MaterijalId);
                    table.ForeignKey(
                        name: "FK__Materijal__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Materijal__Korisnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Materijal__Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_OdrzanCas",
                columns: table => new
                {
                    OdrzanCasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumOdrzavanja = table.Column<DateTime>(nullable: false),
                    NastavniPlanPredmetId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OdrzanCas", x => x.OdrzanCasId);
                    table.ForeignKey(
                        name: "FK__OdrzanCas__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OdrzanCas__Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Sastanak",
                columns: table => new
                {
                    SastanakId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumObavijest = table.Column<DateTime>(nullable: false),
                    DatumSastanka = table.Column<DateTime>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    OdjeljenjeId = table.Column<int>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    OrganizatorId = table.Column<int>(nullable: true),
                    SastanakTipId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sastanak", x => x.SastanakId);
                    table.ForeignKey(
                        name: "FK__Sastanak__Korisnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Sastanak__Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Sastanak__Korisnik_OrganizatorId",
                        column: x => x.OrganizatorId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Sastanak__SastanakTip_SastanakTipId",
                        column: x => x.SastanakTipId,
                        principalTable: "_SastanakTip",
                        principalColumn: "SastanakTipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_UpisUOdjeljenje",
                columns: table => new
                {
                    UpisUOdjeljenjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUDnevniku = table.Column<int>(nullable: false),
                    OdjeljenjeId = table.Column<int>(nullable: false),
                    UcenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UpisUOdjeljenje", x => x.UpisUOdjeljenjeId);
                    table.ForeignKey(
                        name: "FK__UpisUOdjeljenje__Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UpisUOdjeljenje__Korisnik_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObavjestenjeOdjeljenje",
                columns: table => new
                {
                    ObavjestenjeOdjeljenjeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObavjestenjeID = table.Column<int>(nullable: false),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObavjestenjeOdjeljenje", x => x.ObavjestenjeOdjeljenjeID);
                    table.ForeignKey(
                        name: "FK_ObavjestenjeOdjeljenje__Obavjestenje_ObavjestenjeID",
                        column: x => x.ObavjestenjeID,
                        principalTable: "_Obavjestenje",
                        principalColumn: "ObavjestenjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObavjestenjeOdjeljenje__Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Raspored",
                columns: table => new
                {
                    RasporedID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raspored", x => x.RasporedID);
                    table.ForeignKey(
                        name: "FK_Raspored__Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_SastanakRoditelj",
                columns: table => new
                {
                    SastanakRoditeljId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Komentar = table.Column<string>(nullable: true),
                    RoditeljId = table.Column<int>(nullable: true),
                    SastanakId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SastanakRoditelj", x => x.SastanakRoditeljId);
                    table.ForeignKey(
                        name: "FK__SastanakRoditelj__Korisnik_RoditeljId",
                        column: x => x.RoditeljId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__SastanakRoditelj__Sastanak_SastanakId",
                        column: x => x.SastanakId,
                        principalTable: "_Sastanak",
                        principalColumn: "SastanakId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_OdrzanCasDetalji",
                columns: table => new
                {
                    OdrzanCasDetaljiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: true),
                    OdrzanCasId = table.Column<int>(nullable: true),
                    Odsutan = table.Column<bool>(nullable: false),
                    Opravdano = table.Column<bool>(nullable: false),
                    UpisUOdjeljenjeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OdrzanCasDetalji", x => x.OdrzanCasDetaljiId);
                    table.ForeignKey(
                        name: "FK__OdrzanCasDetalji__OdrzanCas_OdrzanCasId",
                        column: x => x.OdrzanCasId,
                        principalTable: "_OdrzanCas",
                        principalColumn: "OdrzanCasId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OdrzanCasDetalji__UpisUOdjeljenje_UpisUOdjeljenjeId",
                        column: x => x.UpisUOdjeljenjeId,
                        principalTable: "_UpisUOdjeljenje",
                        principalColumn: "UpisUOdjeljenjeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZakljucnaOcjena",
                columns: table => new
                {
                    ZakljucnaOcjenaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumZakljucivanja = table.Column<DateTime>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    UpisUOdjeljenjeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZakljucnaOcjena", x => x.ZakljucnaOcjenaID);
                    table.ForeignKey(
                        name: "FK_ZakljucnaOcjena__Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZakljucnaOcjena__UpisUOdjeljenje_UpisUOdjeljenjeId",
                        column: x => x.UpisUOdjeljenjeId,
                        principalTable: "_UpisUOdjeljenje",
                        principalColumn: "UpisUOdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RasporedDetalj",
                columns: table => new
                {
                    RasporedDetaljID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DanID = table.Column<int>(nullable: false),
                    PocetakCasaId = table.Column<int>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    RasporedID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RasporedDetalj", x => x.RasporedDetaljID);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj_Dan_DanID",
                        column: x => x.DanID,
                        principalTable: "Dan",
                        principalColumn: "DanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaId",
                        column: x => x.PocetakCasaId,
                        principalTable: "PocetakCasa",
                        principalColumn: "PocetakCasaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj__Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj_Raspored_RasporedID",
                        column: x => x.RasporedID,
                        principalTable: "Raspored",
                        principalColumn: "RasporedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Angazovan_NastavniPlanPredmetId",
                table: "_Angazovan",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Angazovan_NastavnikId",
                table: "_Angazovan",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__Korisnik_GradId",
                table: "_Korisnik",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX__Korisnik_KorisnickiNalogID",
                table: "_Korisnik",
                column: "KorisnickiNalogID");

            migrationBuilder.CreateIndex(
                name: "IX__Korisnik_RoditeljId",
                table: "_Korisnik",
                column: "RoditeljId");

            migrationBuilder.CreateIndex(
                name: "IX__Materijal_NastavniPlanPredmetId",
                table: "_Materijal",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Materijal_NastavnikId",
                table: "_Materijal",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__Materijal_PredmetId",
                table: "_Materijal",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__NastavniPlan_GodinaStudijaId",
                table: "_NastavniPlan",
                column: "GodinaStudijaId");

            migrationBuilder.CreateIndex(
                name: "IX__NastavniPlan_RazredId",
                table: "_NastavniPlan",
                column: "RazredId");

            migrationBuilder.CreateIndex(
                name: "IX__NastavniPlanPredmet_NastavniPlanId",
                table: "_NastavniPlanPredmet",
                column: "NastavniPlanId");

            migrationBuilder.CreateIndex(
                name: "IX__NastavniPlanPredmet_PredmetId",
                table: "_NastavniPlanPredmet",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Obavjestenje_AdministratorKorisnikId",
                table: "_Obavjestenje",
                column: "AdministratorKorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX__Obavjestenje_NastavnikID",
                table: "_Obavjestenje",
                column: "NastavnikID");

            migrationBuilder.CreateIndex(
                name: "IX__Obavjestenje_TipObavijestiId",
                table: "_Obavjestenje",
                column: "TipObavijestiId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_GodinaStudijaId",
                table: "_Odjeljenje",
                column: "GodinaStudijaId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_NastavniPlanId",
                table: "_Odjeljenje",
                column: "NastavniPlanId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_RazredID",
                table: "_Odjeljenje",
                column: "RazredID");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_RazrednikId",
                table: "_Odjeljenje",
                column: "RazrednikId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_UcenikID",
                table: "_Odjeljenje",
                column: "UcenikID");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCas_NastavniPlanPredmetId",
                table: "_OdrzanCas",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCas_OdjeljenjeID",
                table: "_OdrzanCas",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCasDetalji_OdrzanCasId",
                table: "_OdrzanCasDetalji",
                column: "OdrzanCasId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCasDetalji_UpisUOdjeljenjeId",
                table: "_OdrzanCasDetalji",
                column: "UpisUOdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__Sastanak_NastavnikId",
                table: "_Sastanak",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__Sastanak_OdjeljenjeId",
                table: "_Sastanak",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__Sastanak_OrganizatorId",
                table: "_Sastanak",
                column: "OrganizatorId");

            migrationBuilder.CreateIndex(
                name: "IX__Sastanak_SastanakTipId",
                table: "_Sastanak",
                column: "SastanakTipId");

            migrationBuilder.CreateIndex(
                name: "IX__SastanakRoditelj_RoditeljId",
                table: "_SastanakRoditelj",
                column: "RoditeljId");

            migrationBuilder.CreateIndex(
                name: "IX__SastanakRoditelj_SastanakId",
                table: "_SastanakRoditelj",
                column: "SastanakId");

            migrationBuilder.CreateIndex(
                name: "IX__UpisUOdjeljenje_OdjeljenjeId",
                table: "_UpisUOdjeljenje",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__UpisUOdjeljenje_UcenikId",
                table: "_UpisUOdjeljenje",
                column: "UcenikId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacijskiToken_KorisnickiNalogID",
                table: "AutorizacijskiToken",
                column: "KorisnickiNalogID");

            migrationBuilder.CreateIndex(
                name: "IX_ObavjestenjeOdjeljenje_ObavjestenjeID",
                table: "ObavjestenjeOdjeljenje",
                column: "ObavjestenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_ObavjestenjeOdjeljenje_OdjeljenjeID",
                table: "ObavjestenjeOdjeljenje",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Raspored_OdjeljenjeID",
                table: "Raspored",
                column: "OdjeljenjeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_DanID",
                table: "RasporedDetalj",
                column: "DanID");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_PocetakCasaId",
                table: "RasporedDetalj",
                column: "PocetakCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_PredmetID",
                table: "RasporedDetalj",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_RasporedID",
                table: "RasporedDetalj",
                column: "RasporedID");

            migrationBuilder.CreateIndex(
                name: "IX_ZakljucnaOcjena_PredmetID",
                table: "ZakljucnaOcjena",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_ZakljucnaOcjena_UpisUOdjeljenjeId",
                table: "ZakljucnaOcjena",
                column: "UpisUOdjeljenjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Angazovan");

            migrationBuilder.DropTable(
                name: "_Materijal");

            migrationBuilder.DropTable(
                name: "_OdrzanCasDetalji");

            migrationBuilder.DropTable(
                name: "_SastanakRoditelj");

            migrationBuilder.DropTable(
                name: "_TipOcjene");

            migrationBuilder.DropTable(
                name: "AutorizacijskiToken");

            migrationBuilder.DropTable(
                name: "ObavjestenjeOdjeljenje");

            migrationBuilder.DropTable(
                name: "RasporedDetalj");

            migrationBuilder.DropTable(
                name: "ZakljucnaOcjena");

            migrationBuilder.DropTable(
                name: "_OdrzanCas");

            migrationBuilder.DropTable(
                name: "_Sastanak");

            migrationBuilder.DropTable(
                name: "_Obavjestenje");

            migrationBuilder.DropTable(
                name: "Dan");

            migrationBuilder.DropTable(
                name: "PocetakCasa");

            migrationBuilder.DropTable(
                name: "Raspored");

            migrationBuilder.DropTable(
                name: "_UpisUOdjeljenje");

            migrationBuilder.DropTable(
                name: "_NastavniPlanPredmet");

            migrationBuilder.DropTable(
                name: "_SastanakTip");

            migrationBuilder.DropTable(
                name: "_TipObavijesti");

            migrationBuilder.DropTable(
                name: "_Odjeljenje");

            migrationBuilder.DropTable(
                name: "_Predmet");

            migrationBuilder.DropTable(
                name: "_NastavniPlan");

            migrationBuilder.DropTable(
                name: "_Korisnik");

            migrationBuilder.DropTable(
                name: "_GodinaStudija");

            migrationBuilder.DropTable(
                name: "Razred");

            migrationBuilder.DropTable(
                name: "_Grad");

            migrationBuilder.DropTable(
                name: "korisnickiNalogs");
        }
    }
}
