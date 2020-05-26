using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SI_Admin.API.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Contacto = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    NomEmpresa = table.Column<string>(nullable: true),
                    NomCorto = table.Column<string>(nullable: true),
                    RFC = table.Column<string>(nullable: true),
                    Domicilio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paquetes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true),
                    NumUsuarios = table.Column<int>(nullable: false),
                    NumNegocios = table.Column<int>(nullable: false),
                    Costo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquetes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteActualizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteActualizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteActualizaciones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteNegocios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Server = table.Column<string>(nullable: true),
                    DataBase = table.Column<string>(nullable: true),
                    UserDB = table.Column<string>(nullable: true),
                    PWD = table.Column<string>(nullable: true),
                    Puerto = table.Column<string>(nullable: true),
                    NombreNegocio = table.Column<string>(nullable: true),
                    NegocioID = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteNegocios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteNegocios_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Licencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(nullable: false),
                    PaqueteInicialId = table.Column<int>(nullable: true),
                    CostoIncial = table.Column<decimal>(nullable: false),
                    NumUsuariosTotal = table.Column<int>(nullable: false),
                    NumNegociosTotal = table.Column<int>(nullable: false),
                    CostoTotalActual = table.Column<decimal>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    UltActualizacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licencias_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licencias_Paquetes_PaqueteInicialId",
                        column: x => x.PaqueteInicialId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteActualizacionNegocios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreNegocio = table.Column<string>(nullable: true),
                    NegocioId = table.Column<int>(nullable: false),
                    ClienteActualizacionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteActualizacionNegocios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteActualizacionNegocios_ClienteActualizaciones_ClienteActualizacionId",
                        column: x => x.ClienteActualizacionId,
                        principalTable: "ClienteActualizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aplicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Abr = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    ClienteActualizacionId = table.Column<int>(nullable: true),
                    LicenciaId = table.Column<int>(nullable: true),
                    PaqueteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aplicaciones_ClienteActualizaciones_ClienteActualizacionId",
                        column: x => x.ClienteActualizacionId,
                        principalTable: "ClienteActualizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aplicaciones_Licencias_LicenciaId",
                        column: x => x.LicenciaId,
                        principalTable: "Licencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aplicaciones_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AplicacionId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    PadreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Aplicaciones_AplicacionId",
                        column: x => x.AplicacionId,
                        principalTable: "Aplicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_PadreId",
                        column: x => x.PadreId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aplicaciones_ClienteActualizacionId",
                table: "Aplicaciones",
                column: "ClienteActualizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicaciones_LicenciaId",
                table: "Aplicaciones",
                column: "LicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicaciones_PaqueteId",
                table: "Aplicaciones",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteActualizaciones_ClienteId",
                table: "ClienteActualizaciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteActualizacionNegocios_ClienteActualizacionId",
                table: "ClienteActualizacionNegocios",
                column: "ClienteActualizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteNegocios_ClienteId",
                table: "ClienteNegocios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Licencias_ClienteId",
                table: "Licencias",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Licencias_PaqueteInicialId",
                table: "Licencias",
                column: "PaqueteInicialId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_AplicacionId",
                table: "Menus",
                column: "AplicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_PadreId",
                table: "Menus",
                column: "PadreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteActualizacionNegocios");

            migrationBuilder.DropTable(
                name: "ClienteNegocios");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Aplicaciones");

            migrationBuilder.DropTable(
                name: "ClienteActualizaciones");

            migrationBuilder.DropTable(
                name: "Licencias");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Paquetes");
        }
    }
}
