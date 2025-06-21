using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColaboratorsManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelChanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BetterThecnologie",
                table: "Collaborators",
                newName: "BetterTechnology");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BetterTechnology",
                table: "Collaborators",
                newName: "BetterThecnologie");
        }
    }
}
