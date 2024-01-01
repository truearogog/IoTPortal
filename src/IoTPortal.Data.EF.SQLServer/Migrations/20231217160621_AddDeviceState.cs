using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTPortal.Data.EF.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Devices");
        }
    }
}
