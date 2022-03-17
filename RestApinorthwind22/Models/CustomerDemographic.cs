using System;
using System.Collections.Generic;

namespace RestApinorthwind22.Models
{
    public partial class CustomerDemographic
    {
        public CustomerDemographic()
        {
            Customers = new HashSet<Customers>();
        }

        public string CustomerTypeId { get; set; } = null!;
        public string? CustomerDesc { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
