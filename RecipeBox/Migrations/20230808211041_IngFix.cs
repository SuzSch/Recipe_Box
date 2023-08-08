using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBox.Migrations
{
    public partial class IngFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ing_AspNetUsers_UserId",
                table: "Ing");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngs_Ing_IngId",
                table: "RecipeIngs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ing",
                table: "Ing");

            migrationBuilder.DropIndex(
                name: "IX_Ing_UserId",
                table: "Ing");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ing");

            migrationBuilder.RenameTable(
                name: "Ing",
                newName: "Ings");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeName",
                keyValue: null,
                column: "RecipeName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeName",
                table: "Recipes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Directions",
                keyValue: null,
                column: "Directions",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Directions",
                table: "Recipes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ings",
                table: "Ings",
                column: "IngId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngs_Ings_IngId",
                table: "RecipeIngs",
                column: "IngId",
                principalTable: "Ings",
                principalColumn: "IngId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngs_Ings_IngId",
                table: "RecipeIngs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ings",
                table: "Ings");

            migrationBuilder.RenameTable(
                name: "Ings",
                newName: "Ing");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeName",
                table: "Recipes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Directions",
                table: "Recipes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Ing",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ing",
                table: "Ing",
                column: "IngId");

            migrationBuilder.CreateIndex(
                name: "IX_Ing_UserId",
                table: "Ing",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ing_AspNetUsers_UserId",
                table: "Ing",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngs_Ing_IngId",
                table: "RecipeIngs",
                column: "IngId",
                principalTable: "Ing",
                principalColumn: "IngId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
