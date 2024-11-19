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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(b => b.userID);
            builder.Property(b => b.userName).IsRequired();
            builder.Property(b => b.password).IsRequired();
            builder.Property(b => b.email).IsRequired();
            builder.Property(b => b.birthDate).IsRequired();
            builder.Property(b => b.isAdmin).IsRequired();
            builder.Property(b => b.isActive).HasDefaultValue(true);
            builder.Property(b => b.deposit).HasDefaultValue(0);
        }
    }
}
