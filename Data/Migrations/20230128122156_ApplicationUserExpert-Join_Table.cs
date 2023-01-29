using Microsoft.EntityFrameworkCore.Migrations;

namespace Apps.Data.Migrations
{
    public partial class ApplicationUserExpertJoin_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserExperts",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    ExpertId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserExperts", x => new { x.ApplicationUserId, x.ExpertId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserExperts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserExperts_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserExperts_ExpertId",
                table: "ApplicationUserExperts",
                column: "ExpertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserExperts");
        }
    }
}
