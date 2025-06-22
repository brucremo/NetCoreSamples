using System;
using System.Collections.Generic;

namespace NetCoreSamples.Domain.Entities;

public partial class OrderProduct
{
    public Guid OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
