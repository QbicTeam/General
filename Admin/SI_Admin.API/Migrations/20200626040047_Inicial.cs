using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SI_Admin.API.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aplicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Abr = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicaciones", x => x.Id);
                });

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
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    NumUsuarios = table.Column<int>(nullable: false),
                    NumNegocios = table.Column<int>(nullable: false),
                    Costo = table.Column<decimal>(nullable: false),
                    ClaseIcono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquetes", x => x.Id);
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
                    PadreId = table.Column<int>(nullable: true),
                    ClaseIcono = table.Column<string>(nullable: true),
                    RutaNavegacion = table.Column<string>(nullable: true),
                    Notas = table.Column<string>(nullable: true),
                    NotasTipo = table.Column<string>(nullable: true),
                    NotasPie = table.Column<string>(nullable: true),
                    Componente = table.Column<string>(nullable: true),
                    PermisosEspeciales = table.Column<string>(nullable: true),
                    Estatus = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
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
                    IsDBControl = table.Column<bool>(nullable: false),
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
                    CostoInicial = table.Column<decimal>(nullable: false),
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
                name: "PaqueteApps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppId = table.Column<int>(nullable: false),
                    PaqueteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaqueteApps_Aplicaciones_AppId",
                        column: x => x.AppId,
                        principalTable: "Aplicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaqueteApps_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteActualizacionApps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppId = table.Column<int>(nullable: false),
                    ClienteActualizacionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteActualizacionApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteActualizacionApps_Aplicaciones_AppId",
                        column: x => x.AppId,
                        principalTable: "Aplicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteActualizacionApps_ClienteActualizaciones_ClienteActualizacionId",
                        column: x => x.ClienteActualizacionId,
                        principalTable: "ClienteActualizaciones",
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
                name: "LicenciaApps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppId = table.Column<int>(nullable: false),
                    LicenciaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenciaApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenciaApps_Aplicaciones_AppId",
                        column: x => x.AppId,
                        principalTable: "Aplicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenciaApps_Licencias_LicenciaId",
                        column: x => x.LicenciaId,
                        principalTable: "Licencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteActualizacionApps_AppId",
                table: "ClienteActualizacionApps",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteActualizacionApps_ClienteActualizacionId",
                table: "ClienteActualizacionApps",
                column: "ClienteActualizacionId");

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
                name: "IX_LicenciaApps_AppId",
                table: "LicenciaApps",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenciaApps_LicenciaId",
                table: "LicenciaApps",
                column: "LicenciaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteApps_AppId",
                table: "PaqueteApps",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteApps_PaqueteId",
                table: "PaqueteApps",
                column: "PaqueteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteActualizacionApps");

            migrationBuilder.DropTable(
                name: "ClienteActualizacionNegocios");

            migrationBuilder.DropTable(
                name: "ClienteNegocios");

            migrationBuilder.DropTable(
                name: "LicenciaApps");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "PaqueteApps");

            migrationBuilder.DropTable(
                name: "ClienteActualizaciones");

            migrationBuilder.DropTable(
                name: "Licencias");

            migrationBuilder.DropTable(
                name: "Aplicaciones");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Paquetes");
        }
    }
}
