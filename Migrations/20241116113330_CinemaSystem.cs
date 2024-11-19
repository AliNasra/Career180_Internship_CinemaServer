using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class CinemaSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    cinemaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cinemaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cinemaID", x => x.cinemaID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    deposit = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    hallID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cinemaID = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hallID", x => x.hallID);
                    table.ForeignKey(
                        name: "FK_Halls_Cinemas_cinemaID",
                        column: x => x.cinemaID,
                        principalTable: "Cinemas",
                        principalColumn: "cinemaID");
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    filmID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filmTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    filmType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slotCount = table.Column<int>(type: "int", nullable: false),
                    insertingAdminID = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("filmID", x => x.filmID);
                    table.ForeignKey(
                        name: "FK_Films_Users_insertingAdminID",
                        column: x => x.insertingAdminID,
                        principalTable: "Users",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    operationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    operationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    operationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moneyAmount = table.Column<double>(type: "float", nullable: false),
                    successStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("operationID", x => x.operationID);
                    table.ForeignKey(
                        name: "FK_Operations_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    seatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hallID = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.seatID);
                    table.ForeignKey(
                        name: "FK_Seats_Halls_hallID",
                        column: x => x.hallID,
                        principalTable: "Halls",
                        principalColumn: "hallID");
                });

            migrationBuilder.CreateTable(
                name: "Postings",
                columns: table => new
                {
                    postingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hallID = table.Column<int>(type: "int", nullable: false),
                    filmID = table.Column<int>(type: "int", nullable: false),
                    cinemaID = table.Column<int>(type: "int", nullable: false),
                    operationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    operationFee = table.Column<double>(type: "float", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    postingUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("postingID", x => x.postingID);
                    table.ForeignKey(
                        name: "FK_Postings_Cinemas_cinemaID",
                        column: x => x.cinemaID,
                        principalTable: "Cinemas",
                        principalColumn: "cinemaID");
                    table.ForeignKey(
                        name: "FK_Postings_Films_filmID",
                        column: x => x.filmID,
                        principalTable: "Films",
                        principalColumn: "filmID");
                    table.ForeignKey(
                        name: "FK_Postings_Halls_hallID",
                        column: x => x.hallID,
                        principalTable: "Halls",
                        principalColumn: "hallID");
                    table.ForeignKey(
                        name: "FK_Postings_Users_postingUserID",
                        column: x => x.postingUserID,
                        principalTable: "Users",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    reservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    postingID = table.Column<int>(type: "int", nullable: false),
                    seatID = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    reservationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.reservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Postings_postingID",
                        column: x => x.postingID,
                        principalTable: "Postings",
                        principalColumn: "postingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_seatID",
                        column: x => x.seatID,
                        principalTable: "Seats",
                        principalColumn: "seatID");
                    table.ForeignKey(
                        name: "FK_Reservations_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_insertingAdminID",
                table: "Films",
                column: "insertingAdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_cinemaID",
                table: "Halls",
                column: "cinemaID");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_userID",
                table: "Operations",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Postings_cinemaID",
                table: "Postings",
                column: "cinemaID");

            migrationBuilder.CreateIndex(
                name: "IX_Postings_filmID",
                table: "Postings",
                column: "filmID");

            migrationBuilder.CreateIndex(
                name: "IX_Postings_hallID",
                table: "Postings",
                column: "hallID");

            migrationBuilder.CreateIndex(
                name: "IX_Postings_postingUserID",
                table: "Postings",
                column: "postingUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_postingID",
                table: "Reservations",
                column: "postingID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_seatID",
                table: "Reservations",
                column: "seatID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_userID",
                table: "Reservations",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_hallID",
                table: "Seats",
                column: "hallID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Postings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
