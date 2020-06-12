using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WalletService.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wallet_service");

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                schema: "wallet_service",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                schema: "wallet_service",
                columns: table => new
                {
                    WalletId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(maxLength: 36, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                schema: "wallet_service",
                columns: table => new
                {
                    DepositsId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    WalletId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositsId);
                    table.ForeignKey(
                        name: "FK_Deposits_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "wallet_service",
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "wallet_service",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    _walletId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "wallet_service",
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Wallets__walletId",
                        column: x => x._walletId,
                        principalSchema: "wallet_service",
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_WalletId",
                schema: "wallet_service",
                table: "Deposits",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                schema: "wallet_service",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments__walletId",
                schema: "wallet_service",
                table: "Payments",
                column: "_walletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposits",
                schema: "wallet_service");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "wallet_service");

            migrationBuilder.DropTable(
                name: "PaymentMethod",
                schema: "wallet_service");

            migrationBuilder.DropTable(
                name: "Wallets",
                schema: "wallet_service");
        }
    }
}
