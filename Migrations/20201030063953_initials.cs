using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiSign.Migrations
{
    public partial class initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "signer_history",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    files = table.Column<string>(nullable: true),
                    activity = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    time_ = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "signer_logs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activity = table.Column<string>(nullable: true),
                    time_ = table.Column<DateTime>(nullable: false),
                    ip_address = table.Column<string>(nullable: true),
                    users = table.Column<int>(nullable: true),
                    uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "signer_m_docs_category",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_m_docs_category", x => x.category_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "signer_history");

            migrationBuilder.DropTable(
                name: "signer_logs");

            migrationBuilder.DropTable(
                name: "signer_m_docs_category");
        }
    }
}
