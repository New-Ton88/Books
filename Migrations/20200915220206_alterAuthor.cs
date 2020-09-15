using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class alterAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Author");

            migrationBuilder.AddColumn<short>(
                name: "GenreId01",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "GenreId02",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "GenreId03",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "GenreId04",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "GenreId05",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "GenreId06",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "GenreId07",
                table: "Author",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId01",
                table: "Author",
                column: "GenreId01");

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId02",
                table: "Author",
                column: "GenreId02");

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId03",
                table: "Author",
                column: "GenreId03");

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId04",
                table: "Author",
                column: "GenreId04");

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId05",
                table: "Author",
                column: "GenreId05");

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId06",
                table: "Author",
                column: "GenreId06");

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId07",
                table: "Author",
                column: "GenreId07");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId01",
                table: "Author",
                column: "GenreId01",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId02",
                table: "Author",
                column: "GenreId02",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId03",
                table: "Author",
                column: "GenreId03",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId04",
                table: "Author",
                column: "GenreId04",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId05",
                table: "Author",
                column: "GenreId05",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId06",
                table: "Author",
                column: "GenreId06",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId07",
                table: "Author",
                column: "GenreId07",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId01",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId02",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId03",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId04",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId05",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId06",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Genre_GenreId07",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId01",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId02",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId03",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId04",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId05",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId06",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_GenreId07",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId01",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId02",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId03",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId04",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId05",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId06",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "GenreId07",
                table: "Author");

            migrationBuilder.AddColumn<short>(
                name: "GenreId",
                table: "Author",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_GenreId",
                table: "Author",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Genre_GenreId",
                table: "Author",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
