using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Sessions");
        }
    }
}
