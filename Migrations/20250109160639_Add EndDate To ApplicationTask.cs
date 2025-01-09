using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_task_manager.Migrations
{
    /// <inheritdoc />
    public partial class AddEndDateToApplicationTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "Identity",
                table: "Tasks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "Identity",
                table: "Tasks");
        }
    }
}
