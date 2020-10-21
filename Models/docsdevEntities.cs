using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DigiSign.Models
{
    public partial class docsdevEntities : DbContext
    {
        public docsdevEntities(DbContextOptions<docsdevEntities> options) : base(options) { }
        public virtual DbSet<signer_employee> signer_employee { get; set; }
        public virtual DbSet<signer_file> signer_file { get; set; }
        public virtual DbSet<signer_requests> signer_requests { get; set; }
        public virtual DbSet<signer_workflow> signer_workflow { get; set; }
        public virtual DbSet<signer_files_share> signer_files_share {get;set;}
        public virtual DbSet<signer_m_docs_category> signer_m_docs_category {get;set;}
        public virtual DbSet<signer_history> signer_history {get;set;}
        public virtual DbSet<signer_logs> signer_logs {get;set;}
    
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=DigiSign;User Id=sa;password=RootOnline01;
        //             Trusted_Connection=False;MultipleActiveResultSets=true;");
        //     }
        // }
    }
}
