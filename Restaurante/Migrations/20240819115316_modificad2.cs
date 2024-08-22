using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations
{
    public partial class modificad2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<int>(type: "int", nullable: false),
                    nombre_u = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    eliminado_rol = table.Column<bool>(type: "bit", nullable: false),
                    Usuarioid_usuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.id_rol);
                    table.ForeignKey(
                        name: "FK_Rol_Usuarios_Usuarioid_usuario",
                        column: x => x.Usuarioid_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Usuarioid_usuario",
                table: "Rol",
                column: "Usuarioid_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
