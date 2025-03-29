using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class orderitem
{
    public long order_id { get; set; }

    public long product_id { get; set; }

    public double amount { get; set; }

    public double? price { get; set; }

    public double? total { get; private set; }

    public virtual order order { get; set; } = null!;

    public virtual product product { get; set; } = null!;
}
