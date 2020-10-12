﻿// <auto-generated />
using System;
using DigiSign.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigiSign.Migrations
{
    [DbContext(typeof(docsdevEntities))]
    [Migration("20201012093014_signer_file_share")]
    partial class signer_file_share
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DigiSign.Models.signer_employee", b =>
                {
                    b.Property<string>("employee_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("department_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employee_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employee_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lan_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("lastnotification")
                        .HasColumnType("datetime2");

                    b.Property<string>("superior_id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("employee_id");

                    b.ToTable("signer_employee");
                });

            modelBuilder.Entity("DigiSign.Models.signer_file", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("accessibility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<int?>("company")
                        .HasColumnType("int");

                    b.Property<string>("document_key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("editted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("is_template")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("public_permissions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("request_id")
                        .HasColumnType("int");

                    b.Property<string>("sign_reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("size")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("template_fields")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("uploaded_by")
                        .HasColumnType("int");

                    b.Property<DateTime?>("uploaded_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("signer_file");
                });

            modelBuilder.Entity("DigiSign.Models.signer_files_share", b =>
                {
                    b.Property<int>("share_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("employee_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employee_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("request_id")
                        .HasColumnType("int");

                    b.Property<string>("rules")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shared_key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updated_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updated_time")
                        .HasColumnType("datetime2");

                    b.HasKey("share_id");

                    b.ToTable("signer_files_share");
                });

            modelBuilder.Entity("DigiSign.Models.signer_requests", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("classification_id")
                        .HasColumnType("int");

                    b.Property<int?>("company")
                        .HasColumnType("int");

                    b.Property<DateTime?>("created_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("department_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("next_order")
                        .HasColumnType("int");

                    b.Property<string>("pdf_certified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestor_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("send_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sender_note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sender_old")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("workflow_method")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("signer_requests");
                });

            modelBuilder.Entity("DigiSign.Models.signer_workflow", b =>
                {
                    b.Property<int>("workflow_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("chain_emails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chain_positions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("employee_id")
                        .HasColumnType("int");

                    b.Property<string>("face_auth_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("face_auth_payload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("face_auth_status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("order_by")
                        .HasColumnType("int");

                    b.Property<string>("positions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("request_id")
                        .HasColumnType("int");

                    b.Property<string>("sender_note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("signing_key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("updated_by")
                        .HasColumnType("int");

                    b.HasKey("workflow_id");

                    b.ToTable("signer_workflow");
                });
#pragma warning restore 612, 618
        }
    }
}
