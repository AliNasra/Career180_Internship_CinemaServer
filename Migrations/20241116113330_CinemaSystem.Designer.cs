﻿// <auto-generated />
using System;
using ConsoleApp1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleApp1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241116113330_CinemaSystem")]
    partial class CinemaSystem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConsoleApp1.Models.Cinema", b =>
                {
                    b.Property<int>("cinemaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cinemaID"));

                    b.Property<string>("cinemaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("cinemaID")
                        .HasName("cinemaID");

                    b.ToTable("Cinemas", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Film", b =>
                {
                    b.Property<int>("filmID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("filmID"));

                    b.Property<string>("description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("filmTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filmType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("insertingAdminID")
                        .HasColumnType("int");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("slotCount")
                        .HasColumnType("int");

                    b.HasKey("filmID")
                        .HasName("filmID");

                    b.HasIndex("insertingAdminID");

                    b.ToTable("Films", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Hall", b =>
                {
                    b.Property<int>("hallID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("hallID"));

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.Property<int>("cinemaID")
                        .HasColumnType("int");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("hallID")
                        .HasName("hallID");

                    b.HasIndex("cinemaID");

                    b.ToTable("Halls", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Operation", b =>
                {
                    b.Property<int>("operationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("operationID"));

                    b.Property<double>("moneyAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("operationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("operationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("successStatus")
                        .HasColumnType("bit");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("operationID")
                        .HasName("operationID");

                    b.HasIndex("userID");

                    b.ToTable("Operations", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Posting", b =>
                {
                    b.Property<int>("postingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("postingID"));

                    b.Property<int>("cinemaID")
                        .HasColumnType("int");

                    b.Property<int>("filmID")
                        .HasColumnType("int");

                    b.Property<int>("hallID")
                        .HasColumnType("int");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("operationDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("operationFee")
                        .HasColumnType("float");

                    b.Property<int>("postingUserID")
                        .HasColumnType("int");

                    b.HasKey("postingID")
                        .HasName("postingID");

                    b.HasIndex("cinemaID");

                    b.HasIndex("filmID");

                    b.HasIndex("hallID");

                    b.HasIndex("postingUserID");

                    b.ToTable("Postings", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Reservation", b =>
                {
                    b.Property<int>("reservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reservationID"));

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("postingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("reservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("seatID")
                        .HasColumnType("int");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("reservationID");

                    b.HasIndex("postingID");

                    b.HasIndex("seatID");

                    b.HasIndex("userID");

                    b.ToTable("Reservations", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Seat", b =>
                {
                    b.Property<int>("seatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("seatID"));

                    b.Property<int>("hallID")
                        .HasColumnType("int");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("seatID");

                    b.HasIndex("hallID");

                    b.ToTable("Seats", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userID"));

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("deposit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userID");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ConsoleApp1.Models.Film", b =>
                {
                    b.HasOne("ConsoleApp1.Models.User", "insertingAdmin")
                        .WithMany("insertedFilms")
                        .HasForeignKey("insertingAdminID")
                        .IsRequired();

                    b.Navigation("insertingAdmin");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Hall", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Cinema", "cinema")
                        .WithMany("halls")
                        .HasForeignKey("cinemaID")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("cinema");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Operation", b =>
                {
                    b.HasOne("ConsoleApp1.Models.User", "user")
                        .WithMany("operations")
                        .HasForeignKey("userID")
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Posting", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Cinema", "cinema")
                        .WithMany("postings")
                        .HasForeignKey("cinemaID")
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Film", "film")
                        .WithMany("postings")
                        .HasForeignKey("filmID")
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Hall", "hall")
                        .WithMany("postings")
                        .HasForeignKey("hallID")
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.User", "postingUser")
                        .WithMany("postings")
                        .HasForeignKey("postingUserID")
                        .IsRequired();

                    b.Navigation("cinema");

                    b.Navigation("film");

                    b.Navigation("hall");

                    b.Navigation("postingUser");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Reservation", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Posting", "posting")
                        .WithMany()
                        .HasForeignKey("postingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Seat", "seat")
                        .WithMany("reservations")
                        .HasForeignKey("seatID")
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.User", "reservingUser")
                        .WithMany("reservations")
                        .HasForeignKey("userID")
                        .IsRequired();

                    b.Navigation("posting");

                    b.Navigation("reservingUser");

                    b.Navigation("seat");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Seat", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Hall", "hall")
                        .WithMany("seats")
                        .HasForeignKey("hallID")
                        .IsRequired();

                    b.Navigation("hall");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Cinema", b =>
                {
                    b.Navigation("halls");

                    b.Navigation("postings");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Film", b =>
                {
                    b.Navigation("postings");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Hall", b =>
                {
                    b.Navigation("postings");

                    b.Navigation("seats");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Seat", b =>
                {
                    b.Navigation("reservations");
                });

            modelBuilder.Entity("ConsoleApp1.Models.User", b =>
                {
                    b.Navigation("insertedFilms");

                    b.Navigation("operations");

                    b.Navigation("postings");

                    b.Navigation("reservations");
                });
#pragma warning restore 612, 618
        }
    }
}