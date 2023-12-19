using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddComment : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainVideo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommenterUserId = table.Column<int>(type: "int", nullable: false),
                    CommentedUserId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentedUserId",
                        column: x => x.CommentedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommenterUserId",
                        column: x => x.CommenterUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentedUserId",
                table: "Comments",
                column: "CommentedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommenterUserId",
                table: "Comments",
                column: "CommenterUserId");

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
                name: "Comments");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MainVideo",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PaymentDetailsId",
                table: "UserPaymentMethods",
                newName: "PaymentInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPaymentMethods_PaymentDetailsId",
                table: "UserPaymentMethods",
                newName: "IX_UserPaymentMethods_PaymentInfoId");

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
