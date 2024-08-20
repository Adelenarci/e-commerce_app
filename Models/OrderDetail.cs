using System;
using System.Collections.Generic;

namespace E_Ticaret_Uygulaması.Models
{
    public partial class OrderDetail
    {
    public int SiparişDetayId { get; set; }

    public int? SiparişId { get; set; }

    public int? ÜrünId { get; set; }

    public int? Miktar { get; set; }

    public decimal? Fiyat { get; set; }
    }
}


