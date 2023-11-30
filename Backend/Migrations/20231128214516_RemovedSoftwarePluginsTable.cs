using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSoftwarePluginsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoftwarePlugins");

            migrationBuilder.AddColumn<int>(
                name: "SoftwareId",
                table: "Plugins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plugins_SoftwareId",
                table: "Plugins",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plugins_Software_SoftwareId",
                table: "Plugins",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plugins_Software_SoftwareId",
                table: "Plugins");

            migrationBuilder.DropIndex(
                name: "IX_Plugins_SoftwareId",
                table: "Plugins");

            migrationBuilder.DropColumn(
                name: "SoftwareId",
                table: "Plugins");

            migrationBuilder.CreateTable(
                name: "SoftwarePlugins",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    PluginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwarePlugins", x => new { x.SoftwareId, x.PluginId });
                    table.ForeignKey(
                        name: "FK_SoftwarePlugins_Plugins_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoftwarePlugins_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoftwarePlugins_PluginId",
                table: "SoftwarePlugins",
                column: "PluginId");
        }
    }
}
