using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class BookMultipleGenresAndAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Book");

            migrationBuilder.AddColumn<short>(
                name: "AuthorId01",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "AuthorId02",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "AuthorId03",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "AuthorId04",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "AuthorId05",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GenreId01",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GenreId02",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GenreId03",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GenreId04",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GenreId05",
                table: "Book",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId01",
                table: "Book",
                column: "AuthorId01");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId02",
                table: "Book",
                column: "AuthorId02");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId03",
                table: "Book",
                column: "AuthorId03");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId04",
                table: "Book",
                column: "AuthorId04");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId05",
                table: "Book",
                column: "AuthorId05");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId01",
                table: "Book",
                column: "GenreId01");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId02",
                table: "Book",
                column: "GenreId02");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId03",
                table: "Book",
                column: "GenreId03");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId04",
                table: "Book",
                column: "GenreId04");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId05",
                table: "Book",
                column: "GenreId05");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId01",
                table: "Book",
                column: "AuthorId01",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId02",
                table: "Book",
                column: "AuthorId02",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId03",
                table: "Book",
                column: "AuthorId03",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId04",
                table: "Book",
                column: "AuthorId04",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId05",
                table: "Book",
                column: "AuthorId05",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId01",
                table: "Book",
                column: "GenreId01",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId02",
                table: "Book",
                column: "GenreId02",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId03",
                table: "Book",
                column: "GenreId03",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId04",
                table: "Book",
                column: "GenreId04",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId05",
                table: "Book",
                column: "GenreId05",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId01",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId02",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId03",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId04",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId05",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId01",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId02",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId03",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId04",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId05",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId01",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId02",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId03",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId04",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId05",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId01",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId02",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId03",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId04",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId05",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId01",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId02",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId03",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId04",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId05",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId01",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId02",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId03",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId04",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId05",
                table: "Book");

            migrationBuilder.AddColumn<short>(
                name: "AuthorId",
                table: "Book",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GenreId",
                table: "Book",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId",
                table: "Book",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
