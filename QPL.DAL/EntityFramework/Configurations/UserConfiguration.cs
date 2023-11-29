using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPL.DAL.Entities.Concrete;

namespace QPL.DAL.EntityFramework.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.UserName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.ActiveQPLSearch).HasMaxLength(int.MaxValue);

            builder.HasData(
                new User()
                {
                    Id = 1,
                    UserName = "hiarslan",
                    RoleId = 1,
                    ActiveQPLSearch = "",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                }
            ) ;
        }
    }
}
