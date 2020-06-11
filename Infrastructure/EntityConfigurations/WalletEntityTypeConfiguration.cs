using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletService.Domain.AggregatesModel.WalletAggregate;

namespace WalletService.Service.Infrastructure.EntityConfigurations
{
    public class WalletEntityTypeConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> configuration)
        {
            configuration.ToTable("Wallets", WalletContext.DEFAULT_SCHEMA);

            configuration.HasKey(b => b.Id);

            configuration.Ignore(b => b.DomainEvents);

            configuration.Property(b => b.Id).HasColumnName("WalletId");

            configuration.Property(b => b.UserId).HasMaxLength(36).IsRequired();

            //Address value object persisted as owned entity type supported since EF Core 2.0
            configuration.Property(b => b.Balance)
                .HasColumnName("Balance")
                .HasColumnType("decimal(8,2)");


        }
    }
}
