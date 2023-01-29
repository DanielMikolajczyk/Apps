using Microsoft.EntityFrameworkCore.Migrations;

namespace Apps.Data.Migrations
{
    public partial class ApplicationUserExpertChangeRequestExpertJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserExpertChangeRequestExperts",
                columns: table => new
                {
                    ApplicationUserExpertChangeRequestId = table.Column<int>(nullable: false),
                    ExpertId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserExpertChangeRequestExperts", x => new { x.ApplicationUserExpertChangeRequestId, x.ExpertId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserExpertChangeRequestExperts_ApplicationUserExpertChangeRequests_ApplicationUserExpertChangeRequestId",
                        column: x => x.ApplicationUserExpertChangeRequestId,
                        principalTable: "ApplicationUserExpertChangeRequests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserExpertChangeRequestExperts_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserExpertChangeRequestExperts_ExpertId",
                table: "ApplicationUserExpertChangeRequestExperts",
                column: "ExpertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserExpertChangeRequestExperts");
        }
    }
}
