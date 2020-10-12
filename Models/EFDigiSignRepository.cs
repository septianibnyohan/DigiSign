using System.Linq;

namespace DigiSign.Models {
    public class EFDigiSignRepository : IDigiSignRepository {
        private docsdevEntities context;
        public EFDigiSignRepository(docsdevEntities ctx) {
            context = ctx;
        }
        public IQueryable<signer_employee> Signer_Employees => context.signer_employee;
        public IQueryable<signer_requests> Signer_Requests => context.signer_requests;
        public IQueryable<signer_file> Signer_Files => context.signer_file;
        public IQueryable<signer_workflow> Signer_Workflows => context.signer_workflow;
        public IQueryable<signer_files_share> Signer_Files_Shares => context.signer_files_share;

    }
}