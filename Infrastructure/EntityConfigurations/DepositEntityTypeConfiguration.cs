using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Service.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Infrastructure;

namespace WalletService.Infrastructure.EntityConfigurations
{
    public class DepositEntityTypeConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> configuration)
        {
            configuration.ToTable("Deposits", WalletContext.DEFAULT_SCHEMA);

            configuration.HasKey(b => b.Id);

            configuration.Ignore(b => b.DomainEvents);
            configuration.Ignore(b => b.Amount);

            configuration.Property<Guid>("WalletId").IsRequired();


            configuration.Property(b => b.Id).HasColumnName("DepositsId");

            configuration.Property(b => b.Date).IsRequired();

            //Address value object persisted as owned entity type supported since EF Core 2.0
            configuration.Property(b => b.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(8,2)");
        }
    }
}
