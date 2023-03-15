using Microsoft.EntityFrameworkCore.Migrations;

namespace SelectAndRepair.Data.Migrations
{
    public partial class fixvotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Organizations_OrganizationId1",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_OrganizationId1",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "Votes");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Organizations",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "BlogPosts",
                type: "nvarchar(3500)",
                maxLength: 3500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)",
                oldMaxLength: 3000);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_OrganizationId",
                table: "Votes",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Organizations_OrganizationId",
                table: "Votes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Organizations_OrganizationId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_OrganizationId",
                table: "Votes");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationId1",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Organizations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "BlogPosts",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3500)",
                oldMaxLength: 3500);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_OrganizationId1",
                table: "Votes",
                column: "OrganizationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Organizations_OrganizationId1",
                table: "Votes",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
