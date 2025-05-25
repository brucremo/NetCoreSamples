using System;
using System.Collections.Generic;

namespace NetCoreSamples.Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
