using System;
using System.Collections.Generic;

namespace E_Ticaret_Uygulaması.Models
{
    public partial class Order
    {
        public int SiparişId { get; set; }

        public int? KullanıcıId { get; set; }

        public decimal? ToplamTutar { get; set; }

        public DateTime? SiparişTarihi { get; set; }
    }
}
