using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActivityAttendeeSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendee_Activities_ActivityId",
                table: "ActivityAttendee");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendee_AspNetUsers_UserId",
                table: "ActivityAttendee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityAttendee",
                table: "ActivityAttendee");

            migrationBuilder.DropIndex(
                name: "IX_ActivityAttendee_UserId",
                table: "ActivityAttendee");

            migrationBuilder.RenameTable(
                name: "ActivityAttendee",
                newName: "ActivityAttendees");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId1",
                table: "ActivityAttendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityAttendees",
                table: "ActivityAttendees",
                columns: new[] { "ActivityId", "UserId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId1",
                table: "ActivityAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_AspNetUsers_ActivityId",
                table: "ActivityAttendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityAttendees",
                table: "ActivityAttendees");

            migrationBuilder.DropIndex(
                name: "IX_ActivityAttendees_ActivityId1",
                table: "ActivityAttendees");

            migrationBuilder.DropColumn(
                name: "ActivityId1",
                table: "ActivityAttendees");

            migrationBuilder.RenameTable(
                name: "ActivityAttendees",
                newName: "ActivityAttendee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityAttendee",
                table: "ActivityAttendee",
                columns: new[] { "ActivityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAttendee_UserId",
                table: "ActivityAttendee",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendee_Activities_ActivityId",
                table: "ActivityAttendee",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendee_AspNetUsers_UserId",
                table: "ActivityAttendee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
