using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Configurations
{
    public class FilmEntityTypeConfiguration:IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films");
            builder.HasKey(x => x.filmID).HasName("filmID");
            builder.Property(x => x.filmTitle).IsRequired();
            builder.Property(x => x.filmType).IsRequired();
            builder.Property(x => x.description).HasDefaultValue("");
            builder.Property(x => x.slotCount).IsRequired();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.HasOne(x=>x.insertingAdmin).WithMany(x=>x.insertedFilms).HasForeignKey(x=>x.insertingAdminID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();

        }
    }
}
