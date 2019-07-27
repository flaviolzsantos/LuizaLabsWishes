using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Product> Wishes { get; set; }
    }
}
