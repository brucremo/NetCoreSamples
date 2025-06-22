using System;
using System.Collections.Generic;

namespace NetCoreSamples.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;
}
