using NTier.Model.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NTier.Model.Map
{
    public class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .HasMaxLength(50)
                .HasColumnOrder(2)
                .IsRequired();

            Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnOrder(97)
                .IsRequired();

            Property(x => x.CreatedDate)
                .HasColumnName("Created_Date")
                .HasColumnType("datetime2")
                .HasColumnOrder(98)
                .IsRequired();

            Property(x => x.UpdatedDate)
              .HasColumnName("Updated_Date")
              .HasColumnType("datetime2")
              .HasColumnOrder(99)
              .IsRequired();
        }
    }
}
