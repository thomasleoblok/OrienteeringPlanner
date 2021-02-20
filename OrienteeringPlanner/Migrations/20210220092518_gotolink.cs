using Microsoft.EntityFrameworkCore.Migrations;

namespace OrienteeringPlanner.Migrations
{
    public partial class gotolink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GotoLink",
                table: "Run",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GotoLink",
                table: "Run");
        }
    }
}
