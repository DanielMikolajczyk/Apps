using Microsoft.EntityFrameworkCore.Migrations;

namespace Apps.Data.Migrations
{
    public partial class PdfInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ActId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pdf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pdf_Act_ActId",
                        column: x => x.ActId,
                        principalTable: "Act",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pdf_ActId",
                table: "Pdf",
                column: "ActId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pdf");
        }
    }
}
