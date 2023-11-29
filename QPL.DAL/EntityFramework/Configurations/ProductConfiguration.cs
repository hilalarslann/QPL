using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPL.DAL.Entities.Concrete;


namespace QPL.DAL.EntityFramework.Configurations
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.EngineeringStatus).HasMaxLength(250);
            builder.Property(x => x.PrevEngineeringStatus).HasMaxLength(250);
            builder.HasOne(e => e.ProductDefinition).WithMany(x => x.Products).HasForeignKey(x => x.ProductDefinitionId);
            builder.HasOne(e => e.Manufacturer).WithMany(x => x.Products).HasForeignKey(x => x.ManufacturerId);
            
            builder.HasData(
               new Product()
               {
                   Id = 1,
                   EngineeringStatus = "DHR01F2212",
                   PrevEngineeringStatus = "RS1F2212",
                   ManufacturerId = 1,
                   ProductDefinitionId = 1,
                   CreatedDate = DateTime.Now,
                   UpdatedDate = DateTime.Now,
               },
               new Product()
               {
                   Id = 2,
                   EngineeringStatus = "DDCFEEE",
                   PrevEngineeringStatus = "RE21232",
                   ManufacturerId = 1,
                   ProductDefinitionId = 1,
                   CreatedDate = DateTime.Now,
                   UpdatedDate = DateTime.Now,
               }
               );
        }
    }
}
