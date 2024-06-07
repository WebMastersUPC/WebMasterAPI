using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebmasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AutoidforProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nameProject",
                table: "Projects",
                newName: "NameProject");

            migrationBuilder.RenameColumn(
                name: "methodologies",
                table: "Projects",
                newName: "Methodologies");

            migrationBuilder.RenameColumn(
                name: "languages",
                table: "Projects",
                newName: "Languages");

            migrationBuilder.RenameColumn(
                name: "frameworks",
                table: "Projects",
                newName: "Frameworks");

            migrationBuilder.RenameColumn(
                name: "developer_id",
                table: "Projects",
                newName: "Developer_id");

            migrationBuilder.RenameColumn(
                name: "descriptionProject",
                table: "Projects",
                newName: "DescriptionProject");

            migrationBuilder.RenameColumn(
                name: "budget",
                table: "Projects",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "projectID",
                table: "Projects",
                newName: "ProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameProject",
                table: "Projects",
                newName: "nameProject");

            migrationBuilder.RenameColumn(
                name: "Methodologies",
                table: "Projects",
                newName: "methodologies");

            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "Projects",
                newName: "languages");

            migrationBuilder.RenameColumn(
                name: "Frameworks",
                table: "Projects",
                newName: "frameworks");

            migrationBuilder.RenameColumn(
                name: "Developer_id",
                table: "Projects",
                newName: "developer_id");

            migrationBuilder.RenameColumn(
                name: "DescriptionProject",
                table: "Projects",
                newName: "descriptionProject");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Projects",
                newName: "budget");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Projects",
                newName: "projectID");
        }
    }
}
