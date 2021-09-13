using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationScheduling.Infrastructure.Migrations
{
    public partial class update_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Schedule_ScheduleId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_CompanyId",
                table: "Schedule");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CompanyId",
                table: "Schedule",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Schedule_ScheduleId",
                table: "Notifications",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Schedule_ScheduleId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_CompanyId",
                table: "Schedule");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CompanyId",
                table: "Schedule",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Schedule_ScheduleId",
                table: "Notifications",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
