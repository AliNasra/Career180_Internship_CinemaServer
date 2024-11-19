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
    public class HallEntityTypeConfiguration:IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.ToTable("Halls");
            builder.HasKey(x => x.hallID).HasName("hallID");
            builder.Property(x => x.capacity).IsRequired();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.HasOne(h => h.cinema).WithMany(c => c.halls).HasForeignKey(h => h.cinemaID).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
