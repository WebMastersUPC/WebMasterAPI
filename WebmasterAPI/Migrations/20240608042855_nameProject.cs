using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebmasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class nameProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliverables_Projects_project_id",
                table: "Deliverables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "Projects",
                newName: "enterprise_id");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "Deliverables",
                newName: "projectID");

            migrationBuilder.RenameIndex(
                name: "IX_Deliverables_project_id",
                table: "Deliverables",
                newName: "IX_Deliverables_projectID");

            migrationBuilder.AlterColumn<long>(
                name: "enterprise_id",
                table: "Projects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "projectID",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "budget",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "descriptionProject",
                table: "Projects",
                type: "TEXT",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "developer_id",
                table: "Projects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "frameworks",
                table: "Projects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "languages",
                table: "Projects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "methodologies",
                table: "Projects",
                type: "TEXT",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "nameProject",
                table: "Projects",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_enterprise_id",
                table: "Projects",
                column: "enterprise_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverables_Projects_projectID",
                table: "Deliverables",
                column: "projectID",
                principalTable: "Projects",
                principalColumn: "projectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Enterprises_enterprise_id",
                table: "Projects",
                column: "enterprise_id",
                principalTable: "Enterprises",
                principalColumn: "enterprise_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliverables_Projects_projectID",
                table: "Deliverables");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Enterprises_enterprise_id",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_enterprise_id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "projectID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "budget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "descriptionProject",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "developer_id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "frameworks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "languages",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "methodologies",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "nameProject",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "enterprise_id",
                table: "Projects",
                newName: "project_id");

            migrationBuilder.RenameColumn(
                name: "projectID",
                table: "Deliverables",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "IX_Deliverables_projectID",
                table: "Deliverables",
                newName: "IX_Deliverables_project_id");

            migrationBuilder.AlterColumn<long>(
                name: "project_id",
                table: "Projects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverables_Projects_project_id",
                table: "Deliverables",
                column: "project_id",
                principalTable: "Projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
