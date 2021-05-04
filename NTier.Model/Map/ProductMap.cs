using NTier.Model.Entity;

namespace NTier.Model.Map
{
    public class ProductMap : BaseMap<Product>
    {
        public ProductMap()
        {
            ToTable("dbo.Products");

            Property(x => x.Price).HasColumnOrder(3).IsRequired();
            Property(x => x.UnitInStock).HasColumnOrder(4).IsRequired();
            Property(x => x.QuantityPerUnit).HasColumnOrder(5).IsRequired();
            Property(x => x.CategoryId).HasColumnOrder(6).IsRequired();
        }
    }
}
