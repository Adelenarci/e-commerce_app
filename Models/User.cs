using System;
using System.Collections.Generic;

namespace E_Ticaret_Uygulaması.Models
{

public partial class User
{
    public int KullanıcıId { get; set; }

    public string? KullanıcıAdı { get; set; }

    public string? Şifre { get; set; }

    public string? Email { get; set; }

    public string? Rol { get; set; }
}
}