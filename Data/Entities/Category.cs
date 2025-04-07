using System;
using System.Collections.Generic;

namespace imsapi.Data.Entities
{
    public class Category: EntityBase
    {
        public int storeId { get; set; }
        public virtual Store? Store { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}