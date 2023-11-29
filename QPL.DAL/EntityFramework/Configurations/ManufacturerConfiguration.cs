using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPL.DAL.Entities.Concrete;

namespace QPL.DAL.EntityFramework.Configurations
{
    public class ManufacturerConfiguration : BaseConfiguration<Manufacturer>
    {
        public override void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(250).IsRequired();

            builder.HasData(
                new Manufacturer()
                {
                    Id = 1,
                    Name = "ARDUINO",
                    Code = "UCH010",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new Manufacturer()
                {
                    Id = 2,
                    Name = "RADIAN HEATSINKS",
                    Code = "RAD090",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                }
                );
        }
    }
}
