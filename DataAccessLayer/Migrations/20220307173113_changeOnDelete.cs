using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class changeOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Articles_ArticleId",
                table: "OrderPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Articles_ArticleId",
                table: "OrderPositions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Articles_ArticleId",
                table: "OrderPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Articles_ArticleId",
                table: "OrderPositions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
