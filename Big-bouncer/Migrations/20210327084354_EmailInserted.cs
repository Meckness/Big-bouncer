using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Big_bouncer.Migrations
{
    public partial class EmailInserted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "$2a$11$MnXtrLmq2QTqMBwydx4mO.Q5QaN5qNQ..t0DDP3Y5TxPzNQgVkCg2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "$2a$11$aMz.Xerh519WbhpvYh7U1OaKlhavUJx7G19k5C3M/K8E4ZUcVUCpe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "$2a$11$Ubk7KDbbdOPZAFx7Yznkq.2DyMtL5j73kMlCu.Sq.qqnG0kTJJQou");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "$2a$11$q4siDfQvWubZbKJ2QTdCOu2vx1pZAN.HlI2Yqy4/CExExxTW5a2l6");
        }
    }
}
