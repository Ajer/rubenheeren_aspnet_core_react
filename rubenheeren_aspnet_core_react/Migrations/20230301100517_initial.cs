using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rubenheeren_aspnet_core_react.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 100000, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Content", "CreatedTime", "Title" },
                values: new object[,]
                {
                    { 1, "This is Content 1", new DateTime(2023, 3, 1, 11, 5, 17, 370, DateTimeKind.Local).AddTicks(6621), "Title for post 1" },
                    { 2, "This is Content 2", new DateTime(2023, 3, 1, 11, 5, 17, 370, DateTimeKind.Local).AddTicks(6668), "Title for post 2" },
                    { 3, "This is Content 3", new DateTime(2023, 3, 1, 11, 5, 17, 370, DateTimeKind.Local).AddTicks(6671), "Title for post 3" },
                    { 4, "This is Content 4", new DateTime(2023, 3, 1, 11, 5, 17, 370, DateTimeKind.Local).AddTicks(6673), "Title for post 4" },
                    { 5, "This is Content 5", new DateTime(2023, 3, 1, 11, 5, 17, 370, DateTimeKind.Local).AddTicks(6675), "Title for post 5" },
                    { 6, "This is Content 6", new DateTime(2023, 3, 1, 11, 5, 17, 370, DateTimeKind.Local).AddTicks(6677), "Title for post 6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
