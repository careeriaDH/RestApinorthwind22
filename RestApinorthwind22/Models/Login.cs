using System;
using System.Collections.Generic;

namespace RestApinorthwind22.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
    }
}
