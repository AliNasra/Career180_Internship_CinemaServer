using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1.Configurations
{
    public class SeatEntityTypeConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");
            builder.HasKey(x=>x.seatID);
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.HasOne(x => x.hall).WithMany(x => x.seats).HasForeignKey(x => x.hallID).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
