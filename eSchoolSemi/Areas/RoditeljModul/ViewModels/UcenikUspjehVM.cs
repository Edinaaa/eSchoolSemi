using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class UcenikUspjehVM
    {
        public int UcenikID { get; set; }
        public string ImePrezime { get; set; }
        public double? UkupnoProsjek { get; set; }
        public int UkupnoBrojIzostanaka { get; set; }
        public int UkupnoBrojOpravdanih { get; set; }
        public int UkupnoBrojNeOpravdanih { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
           
            public string Predmet { get; set; }
            public int PredmetID { get; set; }

            public double? Prosjek { get; set; }
            public int BrojIzostanaka { get; set; }

            public int BrojOpravdanih { get; set; }
            public int BrojNeOpravdanih { get; set; }


        }
    }
}
