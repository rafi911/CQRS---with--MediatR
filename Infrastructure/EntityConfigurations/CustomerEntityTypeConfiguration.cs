using Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration:  BaseEntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.FirstName)
                .IsRequired();

            builder.Property(p => p.LastName)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .IsRequired();
        }
    }
}
