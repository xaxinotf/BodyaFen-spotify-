using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodyaFen_spotify_.Migrations
{
    /// <inheritdoc />
    public partial class nig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Photos_PhotoId",
                table: "Artists");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoId",
                table: "Artists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Photos_PhotoId",
                table: "Artists",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Photos_PhotoId",
                table: "Artists");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoId",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Photos_PhotoId",
                table: "Artists",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
