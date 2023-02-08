using Microsoft.EntityFrameworkCore.Migrations;

namespace Apps.Data.Migrations
{
    public partial class Act_Expert_Join_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActExperts",
                columns: table => new
                {
                    ActId = table.Column<int>(nullable: false),
                    ExpertId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActExperts", x => new { x.ActId, x.ExpertId });
                    table.ForeignKey(
                        name: "FK_ActExperts_Act_ActId",
                        column: x => x.ActId,
                        principalTable: "Act",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActExperts_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActExperts_ExpertId",
                table: "ActExperts",
                column: "ExpertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActExperts");
        }
    }
}
