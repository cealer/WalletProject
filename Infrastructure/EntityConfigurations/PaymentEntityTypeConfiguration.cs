using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Infrastructure;

namespace WalletService.Infrastructure.EntityConfigurations
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> configuration)
        {
            configuration.ToTable("Payments", WalletContext.DEFAULT_SCHEMA);

            configuration.HasKey(b => b.Id);

            configuration.Ignore(b => b.DomainEvents);

            configuration.Property(b => b.Id).HasColumnName("PaymentId");

            //Address value object persisted as owned entity type supported since EF Core 2.0
            configuration.Property(b => b.Amount).HasColumnType("decimal(8,2)");

            configuration
                        .Property(x => x.PaymentMethodId)
                        .UsePropertyAccessMode(PropertyAccessMode.Field)
                        .HasColumnName("PaymentMethodId")
                        .IsRequired();

            configuration.HasOne(p => p.PaymentMethod)
                .WithMany()
                .HasForeignKey("PaymentMethodId");

            configuration.HasOne<Wallet>()
               .WithMany()
               .IsRequired()
               .HasForeignKey("_walletId");

        }

    }
}
