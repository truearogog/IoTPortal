using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTPortal.Data.EF.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceApiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Devices");
        }
    }
}
