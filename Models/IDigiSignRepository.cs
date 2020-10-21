using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DigiSign.Models {
    public interface IDigiSignRepository {
        IQueryable<signer_employee> signer_employee { get; }
        IQueryable<signer_requests> signer_requests { get; }
        IQueryable<signer_file> signer_file { get; }
        IQueryable<signer_workflow> signer_workflow { get; }
        IQueryable<signer_files_share> signer_files_share { get; }
        IQueryable<signer_m_docs_category> signer_m_docs_category { get; }
        DbSet<signer_history> signer_history { get; }
        DbSet<signer_logs> signer_logs { get; }

        void SaveChanges();
    }
}