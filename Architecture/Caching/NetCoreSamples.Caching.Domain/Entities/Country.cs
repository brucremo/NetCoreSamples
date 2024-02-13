using System;
using System.Collections.Generic;

namespace NetCoreSamples.Caching.Domain.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}
