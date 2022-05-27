using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSoftware.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.EmployeeTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypes_Name",
                table: "EmployeeTypes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeId");
        }
    }
}
