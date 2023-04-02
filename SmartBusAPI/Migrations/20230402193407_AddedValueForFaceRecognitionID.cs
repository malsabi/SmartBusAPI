using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedValueForFaceRecognitionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1,
                column: "FaceRecognitionID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2,
                column: "FaceRecognitionID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 3,
                column: "FaceRecognitionID",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1,
                column: "FaceRecognitionID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2,
                column: "FaceRecognitionID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 3,
                column: "FaceRecognitionID",
                value: 0);
        }
    }
}
