using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAdminName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Peer", "Shah" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Admin", "Admin" });
        }
    }
}
