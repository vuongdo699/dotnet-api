using ApplicationCore.Domain.Entities.ChemincalAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class IChemicalTypeConfiguration : IEntityTypeConfiguration<ChemicalType>
    {
        public void Configure(EntityTypeBuilder<ChemicalType> builder)
        {
            // table
            builder.ToTable(nameof(ChemicalType), DatabaseContext.SCHEMA);
            builder.ConfigureByConvention();

            // props
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
