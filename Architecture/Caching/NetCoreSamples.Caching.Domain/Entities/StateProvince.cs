using System;
using System.Collections.Generic;

namespace NetCoreSamples.Caching.Domain.Entities;

public partial class StateProvince
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
