using System.Collections.Generic;

namespace NTier.Model.Entity
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
