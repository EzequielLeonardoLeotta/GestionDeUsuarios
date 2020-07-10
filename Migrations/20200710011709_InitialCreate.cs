using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionDeUsuarios.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumento = table.Column<string>(nullable: true),
                    Documento = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    Edad = table.Column<int>(nullable: false),
                    Contacto = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
