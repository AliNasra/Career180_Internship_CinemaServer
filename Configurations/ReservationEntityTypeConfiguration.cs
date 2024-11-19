using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp1.Configurations
{
    public class ReservationEntityTypeConfiguration:IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(p => p.reservationID);
            builder.HasOne(p => p.reservingUser).WithMany(p => p.reservations).HasForeignKey(p => p.userID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.HasOne(p => p.seat).WithMany(p => p.reservations).HasForeignKey(p => p.seatID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.Property(p => p.reservationDate).IsRequired();
            builder.Property(x => x.isActive).HasDefaultValue(true);

        }
    }
}
