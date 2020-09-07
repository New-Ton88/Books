using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class addCategoryIdToGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CategoryId",
                table: "Genre",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Genre");
        }
    }
}
