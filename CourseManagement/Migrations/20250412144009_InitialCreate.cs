using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KhoaHocs",
                table: "KhoaHocs");

            migrationBuilder.RenameTable(
                name: "KhoaHocs",
                newName: "KhoaHoc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhoaHoc",
                table: "KhoaHoc",
                column: "MaKhoaHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KhoaHoc",
                table: "KhoaHoc");

            migrationBuilder.RenameTable(
                name: "KhoaHoc",
                newName: "KhoaHocs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhoaHocs",
                table: "KhoaHocs",
                column: "MaKhoaHoc");
        }
    }
}
