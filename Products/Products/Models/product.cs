using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class product
{
    public long p_id { get; set; }

    public string? p_name { get; set; }

    public double? price { get; set; }

    public virtual ICollection<orderitem> orderitems { get; set; } = new List<orderitem>();
}
