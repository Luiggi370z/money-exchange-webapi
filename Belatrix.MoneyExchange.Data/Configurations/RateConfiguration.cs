using System.Data.Entity.ModelConfiguration;
using Belatrix.MoneyExchange.Model;

namespace Belatrix.MoneyExchange.Data.Configurations
{
    public class RateConfiguration : EntityTypeConfiguration<Rate>
    {
        public RateConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.CurrencyFrom)
                .IsRequired()
                .HasMaxLength(3)
                .HasIndex("IX_RateCurrency_From_To", 1);

            Property(e => e.CurrencyTo)
                .IsRequired()
                .HasMaxLength(3)
                .HasIndex("IX_RateCurrency_From_To", 2);

            Property(e => e.Value)
                .IsRequired();

            Property(e => e.DateTime).IsRequired();
        }
    }
}
