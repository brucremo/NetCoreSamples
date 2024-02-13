using System;
using System.Collections.Generic;

namespace NetCoreSamples.Caching.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public int StateProvinceId { get; set; }

    public virtual StateProvince StateProvince { get; set; } = null!;
}
