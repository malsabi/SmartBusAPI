using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedExtraDataInSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parents",
                columns: new[] { "ID", "Address", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[] { 2, "Fujairah - Al Faseel - United Arab Emirates", "Shammasaif@gmail.com", "Shamma", "Saif", "4a4ad7dfa1aa5fae154f4e741d4dda3ce6bc190397f13b722dbac00420c468f7", "+971502345678" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "Address", "BelongsToBusID", "BusID", "FirstName", "Gender", "GradeLevel", "Image", "IsAtHome", "IsAtSchool", "IsOnBus", "LastName", "LastSeen", "ParentID" },
                values: new object[,]
                {
                    { 2, "Fujairah - Al Faseel - United Arab Emirates", 1, null, "Shaikha", "Female", 6, "", true, false, false, "Yousuf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Fujairah - Al Faseel - United Arab Emirates", 1, null, "Khadeja", "Female", 6, "", true, false, false, "Mohammad", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Parents",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
