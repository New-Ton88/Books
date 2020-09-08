using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class addCategoryGenreFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Genre_CategoryId",
                table: "Genre",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Category_CategoryId",
                table: "Genre",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Category_CategoryId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_CategoryId",
                table: "Genre");
        }
    }
}
