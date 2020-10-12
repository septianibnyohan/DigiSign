using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiSign.Migrations
{
    public partial class signer_file_share : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "signer_files_share",
                columns: table => new
                {
                    share_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<int>(nullable: false),
                    employee_id = table.Column<string>(nullable: true),
                    employee_email = table.Column<string>(nullable: true),
                    rules = table.Column<string>(nullable: true),
                    updated_by = table.Column<string>(nullable: true),
                    updated_time = table.Column<DateTime>(nullable: true),
                    shared_key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_files_share", x => x.share_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "signer_files_share");
        }
    }
}
