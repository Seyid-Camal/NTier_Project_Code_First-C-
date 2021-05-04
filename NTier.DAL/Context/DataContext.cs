using NTier.Model.Entity;
using NTier.Model.Map;
using System.Data.Entity;

namespace NTier.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Name=Conn")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
