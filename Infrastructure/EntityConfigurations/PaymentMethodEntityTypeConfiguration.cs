using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Infrastructure;

namespace WalletService.Infrastructure.EntityConfigurations
{
    public class PaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> configuration)
        {
            configuration.ToTable("PaymentMethod", WalletContext.DEFAULT_SCHEMA);

            configuration.HasKey(b => b.Id);

            configuration.Property(ct => ct.Id)
                .HasColumnName("PaymentMethodId")
              .ValueGeneratedNever()
              .IsRequired();

            configuration.Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .HasMaxLength(25)
                .IsRequired();

        }
    }
}
