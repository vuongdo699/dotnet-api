using ApplicationCore.Domain.Entities.ChemincalAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class IChemicalConfiguration : IEntityTypeConfiguration<Chemical>
    {
        public void Configure(EntityTypeBuilder<Chemical> builder)
        {
            // table
            builder.ToTable(nameof(Chemical), DatabaseContext.SCHEMA);
            builder.ConfigureByConvention();

            // props
            builder.Property(x => x.Name).IsRequired();

            // FK
            builder.HasOne<ChemicalType>()
                .WithMany()
                .HasForeignKey(p => p.TypeId);
        }
    }
}
