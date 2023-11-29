using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPL.DAL.Entities.Concrete;

namespace QPL.DAL.EntityFramework.Configurations
{
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedById);
            builder.Property(e => e.UpdatedById);
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.UpdatedDate).IsRequired();

            builder.HasOne(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.UpdatedBy).WithMany().HasForeignKey(e => e.UpdatedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
