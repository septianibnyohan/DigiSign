using System.Linq;

namespace DigiSign.Models {
    public interface IDigiSignRepository {
        IQueryable<signer_employee> Signer_Employees { get; }
        IQueryable<signer_requests> Signer_Requests { get; }
        IQueryable<signer_file> Signer_Files { get; }
        IQueryable<signer_workflow> Signer_Workflows { get; }
        IQueryable<signer_files_share> Signer_Files_Shares { get; }
    }
}