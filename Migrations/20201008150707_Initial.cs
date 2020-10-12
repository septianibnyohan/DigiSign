using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiSign.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "signer_employee",
                columns: table => new
                {
                    employee_id = table.Column<string>(nullable: false),
                    employee_name = table.Column<string>(nullable: true),
                    employee_email = table.Column<string>(nullable: true),
                    superior_id = table.Column<string>(nullable: true),
                    lan_id = table.Column<string>(nullable: true),
                    department_id = table.Column<string>(nullable: true),
                    lastnotification = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_employee", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "signer_file",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    filename = table.Column<string>(nullable: true),
                    extension = table.Column<string>(nullable: true),
                    size = table.Column<int>(nullable: true),
                    document_key = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    editted = table.Column<string>(nullable: true),
                    is_template = table.Column<string>(nullable: true),
                    template_fields = table.Column<string>(nullable: true),
                    sign_reason = table.Column<string>(nullable: true),
                    accessibility = table.Column<string>(nullable: true),
                    public_permissions = table.Column<string>(nullable: true),
                    company = table.Column<int>(nullable: true),
                    uploaded_by = table.Column<int>(nullable: true),
                    uploaded_on = table.Column<DateTime>(nullable: true),
                    guid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "signer_requests",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company = table.Column<int>(nullable: true),
                    sender = table.Column<string>(nullable: true),
                    sender_note = table.Column<string>(nullable: true),
                    send_time = table.Column<DateTime>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    workflow_method = table.Column<string>(nullable: true),
                    next_order = table.Column<int>(nullable: true),
                    requestor_email = table.Column<string>(nullable: true),
                    department_id = table.Column<string>(nullable: true),
                    classification_id = table.Column<int>(nullable: true),
                    created_time = table.Column<DateTime>(nullable: true),
                    pdf_certified = table.Column<string>(nullable: true),
                    sender_old = table.Column<int>(nullable: true),
                    reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "signer_workflow",
                columns: table => new
                {
                    workflow_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<int>(nullable: true),
                    employee_id = table.Column<int>(nullable: true),
                    document = table.Column<string>(nullable: true),
                    signing_key = table.Column<string>(nullable: true),
                    positions = table.Column<string>(nullable: true),
                    chain_positions = table.Column<string>(nullable: true),
                    chain_emails = table.Column<string>(nullable: true),
                    sender_note = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    notes = table.Column<string>(nullable: true),
                    updated_by = table.Column<int>(nullable: true),
                    order_by = table.Column<int>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: true),
                    face_auth_id = table.Column<string>(nullable: true),
                    face_auth_status = table.Column<string>(nullable: true),
                    face_auth_payload = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signer_workflow", x => x.workflow_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "signer_employee");

            migrationBuilder.DropTable(
                name: "signer_file");

            migrationBuilder.DropTable(
                name: "signer_requests");

            migrationBuilder.DropTable(
                name: "signer_workflow");
        }
    }
}
