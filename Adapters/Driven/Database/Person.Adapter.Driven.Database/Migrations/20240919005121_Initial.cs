using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Person.Adapter.Driven.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cadastro");

            migrationBuilder.CreateTable(
                name: "Pes_Pessoa",
                schema: "Cadastro",
                columns: table => new
                {
                    Pes_PessoaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Pes_Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Pes_Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Pes_Tipo = table.Column<int>(type: "integer", nullable: false),
                    Pes_Documento = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Pes_Status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pes_Pessoa", x => x.Pes_PessoaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pes_Pessoa",
                schema: "Cadastro");
        }
    }
}
