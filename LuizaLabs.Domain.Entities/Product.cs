using LuizaLabs.Infra.Cross;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public void ValidateProduct()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ValidationException("Nome requerido");
        }
    }
}
