using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;
using ConsoleApp1.Configurations;
using ConsoleApp1.Services;


namespace ConsoleApp1
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cinema;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CinemaEntityTypeConfiguration().Configure(modelBuilder.Entity<Cinema>());
            new FilmEntityTypeConfiguration().Configure(modelBuilder.Entity<Film>());
            new HallEntityTypeConfiguration().Configure(modelBuilder.Entity<Hall>());
            new ReservationEntityTypeConfiguration().Configure(modelBuilder.Entity<Reservation>());
            new SeatEntityTypeConfiguration().Configure(modelBuilder.Entity<Seat>());
            new PostingEntityTypeConfiguration().Configure(modelBuilder.Entity<Posting>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new OperationEntityTypeConfiguration().Configure(modelBuilder.Entity<Operation>());
        }
        public DbSet<Cinema>      Cinemas      { get; set; }
        public DbSet<Film>        Films        { get; set; }
        public DbSet<Hall>        Halls        { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat>        Seats        { get; set; }
        public DbSet<Posting>     Postings     { get; set; }
        public DbSet<User>        Users        { get; set; }
        public DbSet<Operation>   Operations   { get; set; }
    }
}
