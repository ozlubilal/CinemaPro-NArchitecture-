using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Timeadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilmSessionDateTime",
                table: "FilmSessions",
                newName: "FilmSessionDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "FilmSessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "FilmSessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "FilmSessions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "FilmSessions");

            migrationBuilder.RenameColumn(
                name: "FilmSessionDate",
                table: "FilmSessions",
                newName: "FilmSessionDateTime");
        }
    }
}
