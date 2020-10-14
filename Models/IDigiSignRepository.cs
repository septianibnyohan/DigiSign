using System.Linq;

namespace DigiSign.Models {
    public interface IDigiSignRepository {
        IQueryable<signer_employee> signer_employee { get; }
        IQueryable<signer_requests> signer_requests { get; }
        IQueryable<signer_file> signer_file { get; }
        IQueryable<signer_workflow> signer_workflow { get; }
        IQueryable<signer_files_share> signer_files_share { get; }

        void SaveChanges();
    }
}