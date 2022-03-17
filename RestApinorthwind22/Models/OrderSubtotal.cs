using System;
using System.Collections.Generic;

namespace RestApinorthwind22.Models
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
