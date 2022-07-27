using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RVA_Projekat.Migrations
{
    public partial class migracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrutoHonorars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrenutnaPlata = table.Column<int>(type: "int", nullable: false),
                    valuta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrutoHonorars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NetoHonorars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPorez = table.Column<int>(type: "int", nullable: false),
                    umanjenje = table.Column<int>(type: "int", nullable: false),
                    uvecanje = table.Column<int>(type: "int", nullable: false),
                    BrutoHonorarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetoHonorars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetoHonorars_BrutoHonorars_BrutoHonorarId",
                        column: x => x.BrutoHonorarId,
                        principalTable: "BrutoHonorars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GodineIskustva = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BrutoHonorarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zaposlenis_BrutoHonorars_BrutoHonorarId",
                        column: x => x.BrutoHonorarId,
                        principalTable: "BrutoHonorars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porez",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    NetoHonorarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porez", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porez_NetoHonorars_NetoHonorarId",
                        column: x => x.NetoHonorarId,
                        principalTable: "NetoHonorars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetoHonorars_BrutoHonorarId",
                table: "NetoHonorars",
                column: "BrutoHonorarId");

            migrationBuilder.CreateIndex(
                name: "IX_Porez_NetoHonorarId",
                table: "Porez",
                column: "NetoHonorarId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenis_BrutoHonorarId",
                table: "Zaposlenis",
                column: "BrutoHonorarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Porez");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Zaposlenis");

            migrationBuilder.DropTable(
                name: "NetoHonorars");

            migrationBuilder.DropTable(
                name: "BrutoHonorars");
        }
    }
}
