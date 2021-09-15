using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class Add_ProductImage_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 14, 43, 37, 599, DateTimeKind.Local).AddTicks(2732));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82"),
                column: "ConcurrencyStamp",
                value: "c88600f2-7f9e-4e93-918c-bd123205e34b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0700e70b-f821-487f-8e73-51652b150080", "AQAAAAEAACcQAAAAEMnbZjnH/JuLHFwsT3t1WJV9U5YQA+llbLUvmxY6FdlGxdQ6hcrCtJSs0LWUjfFm3g==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 17, 53, 26, 778, DateTimeKind.Local).AddTicks(4544));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 14, 43, 37, 599, DateTimeKind.Local).AddTicks(2732),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82"),
                column: "ConcurrencyStamp",
                value: "c98967b3-bb82-4464-ab76-ecea94d0dc9c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fdfed35e-9e65-4136-80ab-8d75817b7c38", "AQAAAAEAACcQAAAAECo3JEx9UHrgj/6fj7Nzyx8UmS+oilIHspwK5iV9YnDfQpP3S9ddlpZgTzP0S11ghw==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 14, 43, 37, 612, DateTimeKind.Local).AddTicks(4387));
        }
    }
}
