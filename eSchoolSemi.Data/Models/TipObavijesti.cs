using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class TipObavijesti
    {
        [Key]
        public int TipObavijestiId { get; set; }
        public string Tip { get; set; }
    }
}
