using NTier.Model.Entity;

namespace NTier.Model.Map
{
    public class CategoryMap : BaseMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");

            Property(x => x.Description)
                .HasMaxLength(255)
                .HasColumnOrder(3)
                .IsRequired();
        }
    }
}
