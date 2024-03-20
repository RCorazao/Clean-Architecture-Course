
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Persistence.Configuration
{
    public class BookingConfiguration
    {
        public BookingConfiguration(EntityTypeBuilder<BookingEntity> entityBuilder)
        {
            entityBuilder.HasKey(e => e.BookingId);
            entityBuilder.Property(e => e.RegisterDate).IsRequired();
            entityBuilder.Property(e => e.Code).IsRequired();
            entityBuilder.Property(e => e.Type).IsRequired();
            entityBuilder.Property(e => e.CustomerId).IsRequired();
            entityBuilder.Property(e => e.UserId).IsRequired();

            entityBuilder.HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId);

            entityBuilder.HasOne(x => x.Customer)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
