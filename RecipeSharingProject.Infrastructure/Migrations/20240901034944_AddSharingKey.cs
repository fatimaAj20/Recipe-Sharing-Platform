using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSharingProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSharingKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SharingKey",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharingKey",
                table: "Recipes");
        }
    }
}
