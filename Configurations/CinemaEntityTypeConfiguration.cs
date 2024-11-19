using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConsoleApp1.Models;


namespace ConsoleApp1.Configurations
{
    public class CinemaEntityTypeConfiguration:IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.ToTable("Cinemas");
            builder.HasKey(x=>x.cinemaID).HasName("cinemaID");
            builder.Property(x => x.cinemaName).IsRequired();
            builder.Property(x => x.isActive).HasDefaultValue(true);
        }
    }
}
