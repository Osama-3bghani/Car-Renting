using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRenting.Migrations
{
    /// <inheritdoc />
    public partial class altertableCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarGallery_CarGalleryId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CarGalleryId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarGallery_CarGalleryId",
                table: "Cars",
                column: "CarGalleryId",
                principalTable: "CarGallery",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarGallery_CarGalleryId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CarGalleryId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarGallery_CarGalleryId",
                table: "Cars",
                column: "CarGalleryId",
                principalTable: "CarGallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
