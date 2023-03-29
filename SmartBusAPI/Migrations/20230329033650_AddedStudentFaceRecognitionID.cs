using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudentFaceRecognitionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaceRecognitionID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceRecognitionID",
                table: "Students");
        }
    }
}