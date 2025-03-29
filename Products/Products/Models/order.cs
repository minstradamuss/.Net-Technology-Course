using System;
using System.Collections.Generic;

namespace Products.Models;

public partial class order
{
    public long o_id { get; set; }

    public byte[]? order_date { get; set; }

    public virtual ICollection<orderitem> orderitems { get; set; } = new List<orderitem>();
}
