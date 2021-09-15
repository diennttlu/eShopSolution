using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 14, 43, 37, 599, DateTimeKind.Local).AddTicks(2732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 14, 26, 28, 377, DateTimeKind.Local).AddTicks(7425));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82"), "c98967b3-bb82-4464-ab76-ecea94d0dc9c", "Administrator role", "ADMIN", "admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"), 0, "fdfed35e-9e65-4136-80ab-8d75817b7c38", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "diennttlu@gmail.com", true, "Tu", "Dien", false, null, "diennttlu@gmail.com", "admin", "AQAAAAEAACcQAAAAECo3JEx9UHrgj/6fj7Nzyx8UmS+oilIHspwK5iV9YnDfQpP3S9ddlpZgTzP0S11ghw==", null, false, "", false, "admin" });

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

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"), new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"), new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82") });

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fb72392-62d8-41ce-b809-f78bd9de2e82"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("6a3c2b02-7427-4ffe-8df7-a635a5a58758"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 14, 26, 28, 377, DateTimeKind.Local).AddTicks(7425),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 9, 15, 14, 43, 37, 599, DateTimeKind.Local).AddTicks(2732));

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
                value: new DateTime(2021, 9, 15, 14, 26, 28, 395, DateTimeKind.Local).AddTicks(6903));
        }
    }
}
