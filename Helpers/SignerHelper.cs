using DigiSign.Configs;
using DigiSign.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
namespace DigiSign.Helpers
{
    public class SignerHelper
    {
        private Microsoft.AspNetCore.Http.HttpContext _httpContext;
        private IDigiSignRepository _repository;
        private AuthHelper _authHelper;
        public SignerHelper(AuthHelper authHelper, IDigiSignRepository repository)
        {
            //_httpContext = httpContext;
            _repository = repository;
            //_authHelper = new AuthHelper(repository, httpContext);
            _authHelper = authHelper;
            _authHelper.initRepo(repository);
        }
        public void Logging(string activity)
        {
            var user = _authHelper.User();
            // using (var context = new docsdevEntities())
            // {
            //     lock (logging_lock)
            //     {
            //         context.signer_logs.Add(new signer_logs
            //         {
            //             activity = activity,
            //             ip_address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
            //             users = user == null ? 0 : Convert.ToInt32(user.employee_id),
            //             uri = HttpContext.Current.Request.ServerVariables["REQUEST_URI"],
            //             time_ = DateTime.Now
            //         });
                    
            //         context.SaveChanges();
            //     }

            // }
        }
    }
    
}