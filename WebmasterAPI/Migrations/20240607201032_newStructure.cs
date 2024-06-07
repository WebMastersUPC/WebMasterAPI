using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebmasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class newStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "nameProject",
                table: "Projects",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "methodologies",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "descriptionProject",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "projectID",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "NameProject",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Methodologies",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionProject",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectID",
                table: "Projects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
