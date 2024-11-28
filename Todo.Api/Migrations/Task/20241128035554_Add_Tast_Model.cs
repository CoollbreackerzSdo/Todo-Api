using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Api.Migrations.Task
{
    /// <inheritdoc />
    public partial class Add_Tast_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    register_date = table.Column<DateOnly>(type: "date", nullable: false),
                    register_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    creator_key = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_creator_key",
                table: "Tasks",
                column: "creator_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
