using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationScheduling.Infrastructure.Migrations
{
    public partial class updated_naming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentOnDays",
                table: "ScheduleRequirements",
                newName: "SendOnDays");

            migrationBuilder.RenameColumn(
                name: "CompanyTypesAllowed",
                table: "ScheduleRequirements",
                newName: "AllowedCompanyTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendOnDays",
                table: "ScheduleRequirements",
                newName: "SentOnDays");

            migrationBuilder.RenameColumn(
                name: "AllowedCompanyTypes",
                table: "ScheduleRequirements",
                newName: "CompanyTypesAllowed");
        }
    }
}
