using System;
using System.Collections.Generic;

namespace NetCoreSamples.Domain.Entities;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StateProvinceId { get; set; }

    public virtual StateProvince StateProvince { get; set; } = null!;
}
