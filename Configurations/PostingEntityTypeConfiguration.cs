using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConsoleApp1.Models;


namespace ConsoleApp1.Configurations
{
    public class PostingEntityTypeConfiguration:IEntityTypeConfiguration<Posting>
    {
        public void Configure(EntityTypeBuilder<Posting> builder)
        {
            builder.ToTable("Postings");
            builder.HasKey(x=>x.postingID).HasName("postingID");
            builder.HasOne(p => p.cinema).WithMany(p => p.postings).HasForeignKey(p => p.cinemaID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.HasOne(p => p.hall).WithMany(p => p.postings).HasForeignKey(p => p.hallID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.HasOne(p => p.film).WithMany(p => p.postings).HasForeignKey(p => p.filmID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.HasOne(p => p.postingUser).WithMany(p => p.postings).HasForeignKey(p => p.postingUserID).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.Property(p => p.operationDate).IsRequired();
            builder.Property(p => p.operationFee).IsRequired();
            builder.Property(x => x.isActive).HasDefaultValue(true);
        }
    }
}
