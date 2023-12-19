using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class PackageUpload1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPaymentMethods_AccountInfos_PaymentInfoId",
                table: "UserPaymentMethods");

            migrationBuilder.RenameColumn(
                name: "PaymentInfoId",
                table: "UserPaymentMethods",
                newName: "PaymentDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPaymentMethods_PaymentInfoId",
                table: "UserPaymentMethods",
                newName: "IX_UserPaymentMethods_PaymentDetailsId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ItemSoftware",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    PluginId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSoftware", x => new { x.ItemId, x.SoftwareId });
                    table.ForeignKey(
                        name: "FK_ItemSoftware_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSoftware_Plugins_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemSoftware_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSoftware_PluginId",
                table: "ItemSoftware",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSoftware_SoftwareId",
                table: "ItemSoftware",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPaymentMethods_AccountInfos_PaymentDetailsId",
                table: "UserPaymentMethods",
                column: "PaymentDetailsId",
                principalTable: "AccountInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPaymentMethods_AccountInfos_PaymentDetailsId",
                table: "UserPaymentMethods");

            migrationBuilder.DropTable(
                name: "ItemSoftware");

            migrationBuilder.RenameColumn(
                name: "PaymentDetailsId",
                table: "UserPaymentMethods",
                newName: "PaymentInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPaymentMethods_PaymentDetailsId",
                table: "UserPaymentMethods",
                newName: "IX_UserPaymentMethods_PaymentInfoId");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPaymentMethods_AccountInfos_PaymentInfoId",
                table: "UserPaymentMethods",
                column: "PaymentInfoId",
                principalTable: "AccountInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
