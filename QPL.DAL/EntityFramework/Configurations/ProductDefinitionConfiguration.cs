using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPL.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.DAL.EntityFramework.Configurations
{
    public class ProductDefinitionConfiguration : BaseConfiguration<ProductDefinition>
    {
        public override void Configure(EntityTypeBuilder<ProductDefinition> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CPC).HasMaxLength(8).IsRequired();
            builder.Property(x => x.NortelCpc).HasMaxLength(8);
            builder.Property(x => x.EngineeringCode).HasMaxLength(250);
            builder.Property(x => x.SpecNo).HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Comments).HasMaxLength(500);
            builder.Property(x => x.Concept).HasMaxLength(250);
            builder.Property(x => x.PackageType).HasMaxLength(250);
            builder.Property(x => x.RoshStatus).HasMaxLength(250);
            builder.Property(x => x.FlammabilityRating).HasMaxLength(250);
            builder.Property(x => x.RadiassionRating).HasMaxLength(250);
            builder.Property(x => x.DisassembledOrReusable);

            builder.HasData(
                new ProductDefinition()
                {
                    Id = 1,
                    CPC = "A9904473",
                    NortelCpc = "A0609386",
                    EngineeringCode = "CHR01F2212",
                    SpecNo = "NPS25161-20",
                    Description = "1/4W 200 OHM 1% SM-RES.",
                    Comments = "NT : A0609386",
                    Concept = "RS1F2212",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new ProductDefinition()
                {
                    Id = 2,
                    CPC = "A9904474",
                    NortelCpc = "A0609369",
                    EngineeringCode = "CHR01F3322",
                    SpecNo = "NPS25161-20",
                    Description = "1/16W 33,2KOHM 1%SM-RES ",
                    Comments = "NT : A0609369  ",
                    Concept = "RS1F2212",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
        }
    }
}
