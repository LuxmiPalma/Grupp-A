using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionUser",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    BookingsId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionUser", x => new { x.BookingsId, x.BookingsId1 });
                    table.ForeignKey(
                        name: "FK_SessionUser_AspNetUsers_BookingsId1",
                        column: x => x.BookingsId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionUser_Sessions_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionUser_BookingsId1",
                table: "SessionUser",
                column: "BookingsId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionUser");
        }
    }
}
