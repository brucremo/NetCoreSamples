﻿using System;
using System.Collections.Generic;

namespace NetCoreSamples.Domain.Entities;

public partial class StateProvince
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country Country { get; set; } = null!;
}
