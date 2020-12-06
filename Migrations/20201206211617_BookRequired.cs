using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class BookRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId01",
                table: "Book",
                column: "AuthorId01",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId02",
                table: "Book",
                column: "AuthorId02",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId03",
                table: "Book",
                column: "AuthorId03",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId04",
                table: "Book",
                column: "AuthorId04",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId05",
                table: "Book",
                column: "AuthorId05",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId01",
                table: "Book",
                column: "GenreId01",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId02",
                table: "Book",
                column: "GenreId02",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId03",
                table: "Book",
                column: "GenreId03",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId04",
                table: "Book",
                column: "GenreId04",
                principalTable: "Genre",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId05",
                table: "Book",
                column: "GenreId05",
                principalTable: "Genre",
                principalColumn: "Id");
        }
    }
}
