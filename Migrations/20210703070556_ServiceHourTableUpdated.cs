using Microsoft.EntityFrameworkCore.Migrations;

namespace shangrila.Migrations
{
    public partial class ServiceHourTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "ServiceHour");

            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "ServiceHour",
                newName: "IsOpen");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "ServiceHour",
                newName: "ServiceHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceHours",
                table: "ServiceHour",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "IsOpen",
                table: "ServiceHour",
                newName: "Visible");

            migrationBuilder.AddColumn<string>(
                name: "CloseTime",
                table: "ServiceHour",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
