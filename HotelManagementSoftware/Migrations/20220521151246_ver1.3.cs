using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSoftware.Migrations
{
    public partial class ver13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDayRate",
                table: "RoomTypes");

            migrationBuilder.RenameColumn(
                name: "HalfDayRate",
                table: "RoomTypes",
                newName: "Rate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "RoomTypes",
                newName: "HalfDayRate");

            migrationBuilder.AddColumn<decimal>(
                name: "FullDayRate",
                table: "RoomTypes",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
