using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSoftware.Migrations
{
    public partial class ver12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationsCancelFee",
                table: "ReservationsCancelFee");

            migrationBuilder.DropColumn(
                name: "DayBeforeArrival",
                table: "ReservationsCancelFee");

            migrationBuilder.RenameTable(
                name: "ReservationsCancelFee",
                newName: "ReservationCancelFeePercents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "PercentOfTotal",
                table: "ReservationCancelFeePercents",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "DayNumberBeforeArrival",
                table: "ReservationCancelFeePercents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationCancelFeePercents",
                table: "ReservationCancelFeePercents",
                column: "DayNumberBeforeArrival");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationCancelFeePercents",
                table: "ReservationCancelFeePercents");

            migrationBuilder.DropColumn(
                name: "DayNumberBeforeArrival",
                table: "ReservationCancelFeePercents");

            migrationBuilder.RenameTable(
                name: "ReservationCancelFeePercents",
                newName: "ReservationsCancelFee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PercentOfTotal",
                table: "ReservationsCancelFee",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayBeforeArrival",
                table: "ReservationsCancelFee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationsCancelFee",
                table: "ReservationsCancelFee",
                column: "DayBeforeArrival");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders",
                column: "ReservationId",
                unique: true,
                filter: "[ReservationId] IS NOT NULL");
        }
    }
}
