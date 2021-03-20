using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Big_bouncer.Migrations
{
    public partial class UsersCryptoPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Password", "Username" },
                values: new object[] { "$2a$11$Ubk7KDbbdOPZAFx7Yznkq.2DyMtL5j73kMlCu.Sq.qqnG0kTJJQou", "User1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "Password", "Username" },
                values: new object[] { "$2a$11$q4siDfQvWubZbKJ2QTdCOu2vx1pZAN.HlI2Yqy4/CExExxTW5a2l6", "User2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Password", "Username" },
                values: new object[] { "Password1", "User1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "Password", "Username" },
                values: new object[] { "Password2", "User2" });
        }
    }
}
