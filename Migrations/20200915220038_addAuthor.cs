using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class addAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Birthday = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LanguageId = table.Column<short>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    GenreId = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Author_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId",
                table: "Author",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_LanguageId",
                table: "Author",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
