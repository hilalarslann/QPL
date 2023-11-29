using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPL.DAL.Entities.Concrete;

namespace QPL.DAL.EntityFramework.Configurations
{
    public class RoleConfiguration : BaseConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.RoleName).HasMaxLength(50).IsRequired();

            builder.HasData(
                new Role()
                {
                    Id = 1,
                    RoleName = Roles.QPL_Detayli_Arama,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
        }
    }
}
