using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    id_horario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_hora = table.Column<DateTime>(type: "datetime", nullable: false),
                    disponibilidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Horario__C5836D6939308B65", x => x.id_horario);
                });

            migrationBuilder.CreateTable(
                name: "Paseador",
                columns: table => new
                {
                    id_paseador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DniPaseador = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    dirección = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TelefonoPaseador = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paseador__C0836292BE2D876F", x => x.id_paseador);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    id_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_servicio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    descripcion_servicio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Servicio__6FD07FDC242612FA", x => x.id_servicio);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DniUsuario = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    dirección = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TelefonoUsuario = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__4E3E04ADF1FABD7D", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Precios",
                columns: table => new
                {
                    id_paseador = table.Column<int>(type: "int", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Precios__167E656FF392DA88", x => new { x.id_paseador, x.id_servicio });
                    table.ForeignKey(
                        name: "FK__Precios__id_pase__5535A963",
                        column: x => x.id_paseador,
                        principalTable: "Paseador",
                        principalColumn: "id_paseador");
                    table.ForeignKey(
                        name: "FK__Precios__id_serv__5629CD9C",
                        column: x => x.id_servicio,
                        principalTable: "Servicios",
                        principalColumn: "id_servicio");
                });

            migrationBuilder.CreateTable(
                name: "Perro",
                columns: table => new
                {
                    id_perro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    raza = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    edad = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    instagram = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    tiktok = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Perro__93FAA747011F9CB2", x => x.id_perro);
                    table.ForeignKey(
                        name: "FK__Perro__id_usuari__59063A47",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Ranking",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_paseador = table.Column<int>(type: "int", nullable: false),
                    comentario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    valoracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ranking__72363284540DF759", x => new { x.id_usuario, x.id_paseador });
                    table.ForeignKey(
                        name: "FK__Ranking__id_pase__6754599E",
                        column: x => x.id_paseador,
                        principalTable: "Paseador",
                        principalColumn: "id_paseador");
                    table.ForeignKey(
                        name: "FK__Ranking__id_usua__66603565",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    id_foto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_perro = table.Column<int>(type: "int", nullable: true),
                    url_foto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fotos__620EA3A53500BEB4", x => x.id_foto);
                    table.ForeignKey(
                        name: "FK__Fotos__id_perro__6A30C649",
                        column: x => x.id_perro,
                        principalTable: "Perro",
                        principalColumn: "id_perro");
                });

            migrationBuilder.CreateTable(
                name: "Opiniones",
                columns: table => new
                {
                    id_opinion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_perro = table.Column<int>(type: "int", nullable: false),
                    id_paseador = table.Column<int>(type: "int", nullable: false),
                    puntuacion = table.Column<int>(type: "int", nullable: false),
                    comentario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Opinione__04DDBD78B95BF317", x => x.id_opinion);
                    table.ForeignKey(
                        name: "FK__Opiniones__id_pa__6EF57B66",
                        column: x => x.id_paseador,
                        principalTable: "Paseador",
                        principalColumn: "id_paseador");
                    table.ForeignKey(
                        name: "FK__Opiniones__id_pe__6E01572D",
                        column: x => x.id_perro,
                        principalTable: "Perro",
                        principalColumn: "id_perro");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    id_reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_paseador = table.Column<int>(type: "int", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    id_perro = table.Column<int>(type: "int", nullable: false),
                    id_horario = table.Column<int>(type: "int", nullable: false),
                    fecha_reserva = table.Column<DateTime>(type: "datetime", nullable: false),
                    estado_reserva = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservas__423CBE5D68BD8FFB", x => x.id_reserva);
                    table.ForeignKey(
                        name: "FK__Reservas__id_hor__628FA481",
                        column: x => x.id_horario,
                        principalTable: "Horario",
                        principalColumn: "id_horario");
                    table.ForeignKey(
                        name: "FK__Reservas__id_pas__5FB337D6",
                        column: x => x.id_paseador,
                        principalTable: "Paseador",
                        principalColumn: "id_paseador");
                    table.ForeignKey(
                        name: "FK__Reservas__id_per__619B8048",
                        column: x => x.id_perro,
                        principalTable: "Perro",
                        principalColumn: "id_perro");
                    table.ForeignKey(
                        name: "FK__Reservas__id_ser__60A75C0F",
                        column: x => x.id_servicio,
                        principalTable: "Servicios",
                        principalColumn: "id_servicio");
                    table.ForeignKey(
                        name: "FK__Reservas__id_usu__5EBF139D",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_id_perro",
                table: "Fotos",
                column: "id_perro");

            migrationBuilder.CreateIndex(
                name: "IX_Opiniones_id_paseador",
                table: "Opiniones",
                column: "id_paseador");

            migrationBuilder.CreateIndex(
                name: "IX_Opiniones_id_perro",
                table: "Opiniones",
                column: "id_perro");

            migrationBuilder.CreateIndex(
                name: "UQ__Paseador__AB6E6164FEA0799A",
                table: "Paseador",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perro_id_usuario",
                table: "Perro",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Precios_id_servicio",
                table: "Precios",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "IX_Ranking_id_paseador",
                table: "Ranking",
                column: "id_paseador");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_id_horario",
                table: "Reservas",
                column: "id_horario");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_id_paseador",
                table: "Reservas",
                column: "id_paseador");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_id_perro",
                table: "Reservas",
                column: "id_perro");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_id_servicio",
                table: "Reservas",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_id_usuario",
                table: "Reservas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__AB6E61643F74DBF3",
                table: "Usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Opiniones");

            migrationBuilder.DropTable(
                name: "Precios");

            migrationBuilder.DropTable(
                name: "Ranking");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "Paseador");

            migrationBuilder.DropTable(
                name: "Perro");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
