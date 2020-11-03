using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DigiSign.Models
{
    public partial class DigiSignContext : DbContext
    {
        public DigiSignContext()
        {
        }

        public DigiSignContext(DbContextOptions<DigiSignContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CertificatePfx> CertificatePfx { get; set; }
        public virtual DbSet<PplmapmasterDepartmentTm> PplmapmasterDepartmentTm { get; set; }
        public virtual DbSet<PplmapmasterWorkflowTm> PplmapmasterWorkflowTm { get; set; }
        public virtual DbSet<SignerApiFace> SignerApiFace { get; set; }
        public virtual DbSet<SignerDelegationHistory> SignerDelegationHistory { get; set; }
        public virtual DbSet<SignerEmployee> SignerEmployee { get; set; }
        public virtual DbSet<SignerFile> SignerFile { get; set; }
        public virtual DbSet<SignerFilesShare> SignerFilesShare { get; set; }
        public virtual DbSet<SignerHistory> SignerHistory { get; set; }
        public virtual DbSet<SignerLogs> SignerLogs { get; set; }
        public virtual DbSet<SignerMClassification> SignerMClassification { get; set; }
        public virtual DbSet<SignerMDepartment> SignerMDepartment { get; set; }
        public virtual DbSet<SignerMDocsCategory> SignerMDocsCategory { get; set; }
        public virtual DbSet<SignerMenu> SignerMenu { get; set; }
        public virtual DbSet<SignerNotifications> SignerNotifications { get; set; }
        public virtual DbSet<SignerNotificationsTest> SignerNotificationsTest { get; set; }
        public virtual DbSet<SignerPayload> SignerPayload { get; set; }
        public virtual DbSet<SignerPayloadCallback> SignerPayloadCallback { get; set; }
        public virtual DbSet<SignerRequests> SignerRequests { get; set; }
        public virtual DbSet<SignerRoles> SignerRoles { get; set; }
        public virtual DbSet<SignerWorkflow> SignerWorkflow { get; set; }
        public virtual DbSet<TempSignerFile> TempSignerFile { get; set; }
        public virtual DbSet<TempSignerRequests> TempSignerRequests { get; set; }
        public virtual DbSet<TempSignerWorkflow> TempSignerWorkflow { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<ViewWorkflow> ViewWorkflow { get; set; }
        public virtual DbSet<ViewWorkflowById> ViewWorkflowById { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=DigiSign;User Id=sa;password=RootOnline01;Trusted_Connection=False;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CertificatePfx>(entity =>
            {
                entity.HasKey(e => e.CertId)
                    .HasName("PK__certific__024B46EC58963D0A");

                entity.ToTable("certificate_pfx");

                entity.Property(e => e.CertId).HasColumnName("cert_id");

                entity.Property(e => e.CertFile)
                    .HasColumnName("cert_file")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CertPassword)
                    .HasColumnName("cert_password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employee_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiredFrom)
                    .HasColumnName("expired_from")
                    .HasColumnType("date");

                entity.Property(e => e.ExpiredTo)
                    .HasColumnName("expired_to")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<PplmapmasterDepartmentTm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PPLMAPMasterDepartment_TM");

                entity.Property(e => e.COfficeId)
                    .HasColumnName("C_OFFICE_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CRegion)
                    .HasColumnName("C_REGION")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CRegionDescr)
                    .HasColumnName("C_REGION_DESCR")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DeptDescr)
                    .HasColumnName("DEPT_DESCR")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Deptid)
                    .HasColumnName("DEPTID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Effdt)
                    .HasColumnName("EFFDT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Level00Id)
                    .HasColumnName("Level00_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level00Name)
                    .HasColumnName("Level00_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level01Id)
                    .HasColumnName("Level01_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level01Name)
                    .HasColumnName("Level01_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level02Id)
                    .HasColumnName("Level02_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level02Name)
                    .HasColumnName("Level02_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level03Id)
                    .HasColumnName("Level03_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level03Name)
                    .HasColumnName("Level03_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level04Id)
                    .HasColumnName("Level04_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level04Name)
                    .HasColumnName("Level04_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level05Id)
                    .HasColumnName("Level05_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level05Name)
                    .HasColumnName("Level05_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level06Id)
                    .HasColumnName("Level06_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level06Name)
                    .HasColumnName("Level06_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level07Id)
                    .HasColumnName("Level07_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level07Name)
                    .HasColumnName("Level07_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Level08Id)
                    .HasColumnName("Level08_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Level08Name)
                    .HasColumnName("Level08_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LevelDescr)
                    .HasColumnName("LEVEL_DESCR")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LevelId).HasColumnName("LEVEL_ID");

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LocationDescr)
                    .HasColumnName("LOCATION_DESCR")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerDescr)
                    .HasColumnName("MANAGER_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.ManagerId)
                    .HasColumnName("MANAGER_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerLoop).HasColumnName("MANAGER_LOOP");

                entity.Property(e => e.ManagerName)
                    .HasColumnName("MANAGER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerPosn)
                    .HasColumnName("MANAGER_POSN")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PplmapmasterWorkflowTm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PPLMAPMasterWorkflow_TM");

                entity.Property(e => e.ApprovalDottedLine)
                    .HasColumnName("APPROVAL_DOTTED_LINE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalDottedLineDescr)
                    .HasColumnName("APPROVAL_DOTTED_LINE_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalDottedLineId)
                    .HasColumnName("APPROVAL_DOTTED_LINE_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalDottedLineName)
                    .HasColumnName("APPROVAL_DOTTED_LINE_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CBenefitCostCen)
                    .HasColumnName("C_BENEFIT_COST_CEN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CCostCentre)
                    .HasColumnName("C_COST_CENTRE")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Deptid)
                    .HasColumnName("DEPTID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DivDescr)
                    .HasColumnName("DIV_DESCR")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DivId)
                    .HasColumnName("DIV_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Effdt)
                    .HasColumnName("EFFDT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmailAddr)
                    .HasColumnName("EMAIL_ADDR")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Emplid)
                    .HasColumnName("EMPLID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.GroupDescr)
                    .HasColumnName("GROUP_DESCR")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId)
                    .HasColumnName("GROUP_ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Jobcode)
                    .HasColumnName("JOBCODE")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.JobcodeDescr)
                    .HasColumnName("JOBCODE_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaManagerDescr)
                    .HasColumnName("MA_MANAGER_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.MaManagerId)
                    .HasColumnName("MA_MANAGER_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.MaManagerName)
                    .HasColumnName("MA_MANAGER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaManagerPosn)
                    .HasColumnName("MA_MANAGER_POSN")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerDescr)
                    .HasColumnName("MANAGER_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.ManagerId)
                    .HasColumnName("MANAGER_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerName)
                    .HasColumnName("MANAGER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerPosn)
                    .HasColumnName("MANAGER_POSN")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.MaxEmplRcd).HasColumnName("MAX_EMPL_RCD");

                entity.Property(e => e.NameDisplay)
                    .HasColumnName("NAME_DISPLAY")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameLogin)
                    .HasColumnName("NAME_LOGIN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NdmCstcenDescr)
                    .HasColumnName("NDM_CSTCEN_DESCR")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NdmDeptidDescr)
                    .HasColumnName("NDM_DEPTID_DESCR")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NdmGroupDescr)
                    .HasColumnName("NDM_GROUP_DESCR")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NdmLocatiDescr)
                    .HasColumnName("NDM_LOCATI_DESCR")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NdmSubgroupDescr)
                    .HasColumnName("NDM_SUBGROUP_DESCR")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PositionNbr)
                    .HasColumnName("POSITION_NBR")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDottedLine)
                    .HasColumnName("REPORT_DOTTED_LINE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDottedLineDescr)
                    .HasColumnName("REPORT_DOTTED_LINE_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.ReportDottedLineId)
                    .HasColumnName("REPORT_DOTTED_LINE_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDottedLineName)
                    .HasColumnName("REPORT_DOTTED_LINE_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportsTo)
                    .HasColumnName("REPORTS_TO")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ReportsToDescr)
                    .HasColumnName("REPORTS_TO_DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.ReportsToId)
                    .HasColumnName("REPORTS_TO_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ReportsToLoop).HasColumnName("REPORTS_TO_LOOP");

                entity.Property(e => e.ReportsToName)
                    .HasColumnName("REPORTS_TO_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnionCd)
                    .HasColumnName("UNION_CD")
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerApiFace>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__signer_a__85C600AFA6E19DD0");

                entity.ToTable("signer_api_face");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErrorLog)
                    .HasColumnName("error_log")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StatusFaceapp)
                    .HasColumnName("status_faceapp")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime)
                    .HasColumnName("updated_time")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SignerDelegationHistory>(entity =>
            {
                entity.ToTable("signer_delegation_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UserDelegated).HasColumnName("user_delegated");

                entity.Property(e => e.UserOrigin).HasColumnName("user_origin");
            });

            modelBuilder.Entity<SignerEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__signer_employee_id");

                entity.ToTable("signer_employee");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeEmail)
                    .HasColumnName("employee_email")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LanId)
                    .HasColumnName("lan_id")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Lastnotification)
                    .HasColumnName("lastnotification")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SuperiorId)
                    .HasColumnName("superior_id")
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerFile>(entity =>
            {
                entity.ToTable("signer_file");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Accessibility)
                    .HasColumnName("accessibility")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.DocumentKey)
                    .HasColumnName("document_key")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Editted)
                    .HasColumnName("editted")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasColumnName("extension")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsTemplate)
                    .HasColumnName("is_template")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PublicPermissions)
                    .HasColumnName("public_permissions")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.SignReason)
                    .HasColumnName("sign_reason")
                    .HasColumnType("text");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateFields)
                    .HasColumnName("template_fields")
                    .HasColumnType("text");

                entity.Property(e => e.UploadedBy).HasColumnName("uploaded_by");

                entity.Property(e => e.UploadedOn)
                    .HasColumnName("uploaded_on")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SignerFilesShare>(entity =>
            {
                entity.HasKey(e => e.ShareId)
                    .HasName("PK__signer_f__C36E8AE521979DDA");

                entity.ToTable("signer_files_share");

                entity.Property(e => e.ShareId).HasColumnName("share_id");

                entity.Property(e => e.EmployeeEmail)
                    .HasColumnName("employee_email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employee_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.Rules)
                    .HasColumnName("rules")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SharedKey)
                    .HasColumnName("shared_key")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedTime)
                    .HasColumnName("updated_time")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SignerHistory>(entity =>
            {
                entity.ToTable("signer_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activity)
                    .HasColumnName("activity")
                    .IsUnicode(false);

                entity.Property(e => e.Files)
                    .HasColumnName("files")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("time_")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerLogs>(entity =>
            {
                entity.ToTable("signer_logs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activity)
                    .HasColumnName("activity")
                    .HasColumnType("ntext");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("time_")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uri)
                    .HasColumnName("uri")
                    .HasColumnType("ntext");

                entity.Property(e => e.Users).HasColumnName("users");
            });

            modelBuilder.Entity<SignerMClassification>(entity =>
            {
                entity.HasKey(e => e.ClassificationId)
                    .HasName("PK__signer_m__C73D9994A6F4A551");

                entity.ToTable("signer_m_classification");

                entity.Property(e => e.ClassificationId).HasColumnName("classification_id");

                entity.Property(e => e.ClassificationName)
                    .HasColumnName("classification_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerMDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("signer_m_department");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerMDocsCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__signer_m__D54EE9B41BD064B2");

                entity.ToTable("signer_m_docs_category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerMenu>(entity =>
            {
                entity.ToTable("signer_menu");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.KeysRoles)
                    .HasColumnName("keys_roles")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Menu)
                    .HasColumnName("menu")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MenuRoles)
                    .HasColumnName("menu_roles")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<SignerNotifications>(entity =>
            {
                entity.ToTable("signer_notifications");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("ntext");

                entity.Property(e => e.Time)
                    .HasColumnName("time_")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Users).HasColumnName("users");
            });

            modelBuilder.Entity<SignerNotificationsTest>(entity =>
            {
                entity.ToTable("signer_notifications_test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("ntext");

                entity.Property(e => e.Time)
                    .HasColumnName("time_")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.User).HasColumnName("user");
            });

            modelBuilder.Entity<SignerPayload>(entity =>
            {
                entity.ToTable("signer_payload");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TrxBodyRequest)
                    .HasColumnName("trx_body_request")
                    .HasColumnType("text");

                entity.Property(e => e.TrxCode)
                    .IsRequired()
                    .HasColumnName("trx_code")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrxDate)
                    .HasColumnName("trx_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrxReferenceId)
                    .HasColumnName("trx_reference_id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TrxStatus)
                    .HasColumnName("trx_status")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerPayloadCallback>(entity =>
            {
                entity.ToTable("signer_payload_callback");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ActionDate)
                    .HasColumnName("actionDate")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("applicationId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RefId)
                    .HasColumnName("refId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .HasColumnName("requestId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TargetUserId)
                    .HasColumnName("targetUserId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionCode)
                    .HasColumnName("transactionCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerRequests>(entity =>
            {
                entity.ToTable("signer_requests");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassificationId).HasColumnName("classification_id");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NextOrder)
                    .HasColumnName("next_order")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PdfCertified)
                    .HasColumnName("pdf_certified")
                    .HasColumnType("ntext");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorEmail)
                    .HasColumnName("requestor_email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendTime)
                    .HasColumnName("send_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sender)
                    .HasColumnName("sender")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.SenderNote)
                    .HasColumnName("sender_note")
                    .IsUnicode(false);

                entity.Property(e => e.SenderOld).HasColumnName("sender_old");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowMethod)
                    .HasColumnName("workflow_method")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerRoles>(entity =>
            {
                entity.HasKey(e => e.Keys)
                    .HasName("PK__signer_r__2B3B21D08387D1C3");

                entity.ToTable("signer_roles");

                entity.Property(e => e.Keys)
                    .HasColumnName("keys")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignerWorkflow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId)
                    .HasName("PK__signer_w__64A76B706D6183A0");

                entity.ToTable("signer_workflow");

                entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");

                entity.Property(e => e.ChainEmails)
                    .HasColumnName("chain_emails")
                    .HasColumnType("ntext");

                entity.Property(e => e.ChainPositions)
                    .HasColumnName("chain_positions")
                    .HasColumnType("ntext");

                entity.Property(e => e.Document)
                    .HasColumnName("document")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.FaceAuthId)
                    .HasColumnName("face_auth_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FaceAuthPayload)
                    .HasColumnName("face_auth_payload")
                    .HasColumnType("text");

                entity.Property(e => e.FaceAuthStatus)
                    .HasColumnName("face_auth_status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderBy).HasColumnName("order_by");

                entity.Property(e => e.Positions)
                    .HasColumnName("positions")
                    .HasColumnType("ntext");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.SenderNote)
                    .HasColumnName("sender_note")
                    .IsUnicode(false);

                entity.Property(e => e.SigningKey)
                    .HasColumnName("signing_key")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TempSignerFile>(entity =>
            {
                entity.ToTable("temp_signer_file");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Accessibility)
                    .HasColumnName("accessibility")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.DocumentKey)
                    .HasColumnName("document_key")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Editted)
                    .HasColumnName("editted")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasColumnName("extension")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsTemplate)
                    .HasColumnName("is_template")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PublicPermissions)
                    .HasColumnName("public_permissions")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.SignReason)
                    .HasColumnName("sign_reason")
                    .HasColumnType("text");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateFields)
                    .HasColumnName("template_fields")
                    .HasColumnType("text");

                entity.Property(e => e.UploadedBy).HasColumnName("uploaded_by");

                entity.Property(e => e.UploadedOn)
                    .HasColumnName("uploaded_on")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TempSignerRequests>(entity =>
            {
                entity.ToTable("temp_signer_requests");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassificationId).HasColumnName("classification_id");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.NextOrder).HasColumnName("next_order");

                entity.Property(e => e.PdfCertified)
                    .HasColumnName("pdf_certified")
                    .HasColumnType("ntext");

                entity.Property(e => e.RequestorEmail)
                    .HasColumnName("requestor_email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendTime)
                    .HasColumnName("send_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sender).HasColumnName("sender");

                entity.Property(e => e.SenderNote)
                    .HasColumnName("sender_note")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowMethod)
                    .HasColumnName("workflow_method")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TempSignerWorkflow>(entity =>
            {
                entity.HasKey(e => e.WorkflowId)
                    .HasName("PK__signer_w__64A76B706D6183A0_copy1");

                entity.ToTable("temp_signer_workflow");

                entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");

                entity.Property(e => e.ChainEmails)
                    .HasColumnName("chain_emails")
                    .HasColumnType("ntext");

                entity.Property(e => e.ChainPositions)
                    .HasColumnName("chain_positions")
                    .HasColumnType("ntext");

                entity.Property(e => e.Document)
                    .HasColumnName("document")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderBy).HasColumnName("order_by");

                entity.Property(e => e.Positions)
                    .HasColumnName("positions")
                    .HasColumnType("ntext");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.SenderNote)
                    .HasColumnName("sender_note")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SigningKey)
                    .HasColumnName("signing_key")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Hiddenfiles)
                    .HasColumnName("hiddenfiles")
                    .IsUnicode(false);

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasColumnName("lang")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Lastnotification).HasColumnName("lastnotification");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("lname")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Permissions)
                    .IsRequired()
                    .HasColumnName("permissions")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Signature)
                    .HasColumnName("signature")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Timezone)
                    .IsRequired()
                    .HasColumnName("timezone")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWorkflow>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWorkflow");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("employee_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LanId)
                    .HasColumnName("lan_id")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.RequestorEmail)
                    .HasColumnName("requestor_email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorNote)
                    .HasColumnName("requestor_note")
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");
            });

            modelBuilder.Entity<ViewWorkflowById>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWorkflowById");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.RequestorEmail)
                    .HasColumnName("requestor_email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorNote)
                    .HasColumnName("requestor_note")
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
