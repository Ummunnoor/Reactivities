using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId1",
                table: "ActivityAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_AspNetUsers_ActivityId",
                table: "ActivityAttendees");

            migrationBuilder.DropIndex(
                name: "IX_ActivityAttendees_ActivityId1",
                table: "ActivityAttendees");

            migrationBuilder.DropColumn(
                name: "ActivityId1",
                table: "ActivityAttendees");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAttendees_UserId",
                table: "ActivityAttendees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId",
                table: "ActivityAttendees",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_AspNetUsers_UserId",
                table: "ActivityAttendees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId",
                table: "ActivityAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_AspNetUsers_UserId",
                table: "ActivityAttendees");

            migrationBuilder.DropIndex(
                name: "IX_ActivityAttendees_UserId",
                table: "ActivityAttendees");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId1",
                table: "ActivityAttendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAttendees_ActivityId1",
                table: "ActivityAttendees",
                column: "ActivityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId1",
                table: "ActivityAttendees",
                column: "ActivityId1",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_AspNetUsers_ActivityId",
                table: "ActivityAttendees",
                column: "ActivityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
