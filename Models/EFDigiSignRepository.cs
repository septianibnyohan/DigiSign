using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DigiSign.Models {
    public class EFDigiSignRepository : IDigiSignRepository {
        private docsdevEntities context;
        public EFDigiSignRepository(docsdevEntities ctx) {
            context = ctx;
        }
        public IQueryable<signer_employee> signer_employee => context.signer_employee;
        public IQueryable<signer_requests> signer_requests => context.signer_requests;
        public IQueryable<signer_file> signer_file => context.signer_file;
        public IQueryable<signer_workflow> signer_workflow => context.signer_workflow;
        public IQueryable<signer_files_share> signer_files_share => context.signer_files_share;
        public IQueryable<signer_m_docs_category> signer_m_docs_category => context.signer_m_docs_category;
        public DbSet<signer_history> signer_history => context.signer_history;
        public DbSet<signer_logs> signer_logs => context.signer_logs;

        public void SaveChanges() {
            context.SaveChanges();
        }

    }
}