using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.RoditeljModul.ViewModels
{
    public class SastanakIndexVm
    {
        public int RoditeljId { get; set; }
        public List<Ucenik> Ucenici { get; set; }
        public class Ucenik
        {
            public int UcenikID { get; set; }
            public string UcenikIme { get; set; }
           
            public List<Row> Rows{ get; set; }
            public class Row
            {
                public int SastanakId { get; set; }
                public string  Naziv { get; set; }
                public string SastanakTip { get; set; }
                public DateTime DatumObjave { get; set; }
                public DateTime DatumSastanka { get; set; }
                 public int OdjeljenjeID { get; set; }

               public int OrganizatorId { get; set; }
                public string Organizator { get; set; }
                
              
            }
        }
      
    }
}
