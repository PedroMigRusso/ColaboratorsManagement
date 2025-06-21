using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColaboratorsManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PossibleError",
                table: "Collaborators");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PossibleError",
                table: "Collaborators",
                type: "TEXT",
                nullable: true);
        }
    }
}
