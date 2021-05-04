using System.ComponentModel.DataAnnotations.Schema;

namespace NTier.Model.Entity
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public string QuantityPerUnit { get; set; }
        public short UnitInStock { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
