using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class Change_file_lenght_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82"),
                column: "ConcurrencyStamp",
                value: "9c9ea4ef-cb45-4516-973c-0b46379bceba");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ccbbca4a-dcc1-4119-92fb-2c16b8a4a739", "AQAAAAEAACcQAAAAEKk9X1GIV5VHfCgB6GgrKMgJVuiA7/kDWwDthSNfxeyswHnI/gPCiZGllWUj9gLNdQ==" });

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
                value: new DateTime(2021, 9, 15, 18, 48, 43, 586, DateTimeKind.Local).AddTicks(5479));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

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
        }
    }
}
