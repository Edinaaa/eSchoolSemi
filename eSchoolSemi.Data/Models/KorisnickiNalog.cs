using System;
using System.Collections.Generic;
using System.Text;

namespace eSchoolSemi.Data.Models
{
    public class KorisnickiNalog
    {

        public int KorisnickiNalogID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime TokenVaziDo { get; set; }
        public bool Zapamti { get; set; }
    }
}
