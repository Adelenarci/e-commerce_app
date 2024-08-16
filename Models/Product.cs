using System;
using System.Collections.Generic;

namespace E_Ticaret_Uygulaması.Models;

public partial class Product
{
    public int ÜrünId { get; set; }

    public string? İsim { get; set; }

    public string? Açıklama { get; set; }

    public decimal? Fiyat { get; set; }

    public int? Stok { get; set; }
}
