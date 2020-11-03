using DigiSign.Configs;
using DigiSign.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
namespace DigiSign.Helpers
{
    public class AuthHelper
    {
        private Microsoft.AspNetCore.Http.HttpContext _httpContext;
        private IDigiSignRepository _repository;
        private IHttpContextAccessor _contextAccessor;
        //public HttpContext Current => new HttpContextAccessor().HttpContext;
        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            //public static HttpContext Current => new HttpContextAccessor().HttpContext;
            //_repository = repository;
        }

        public void initRepo(IDigiSignRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Get the authenticated user
        /// </summary>
        /// <returns>\Std</returns>
        public SignerEmployee User()
        {
            _httpContext  = _contextAccessor.HttpContext;
            if (_httpContext == null)
            {
                return null;
            }
            if (_httpContext.Session.GetString(Auth.Session) == null)
            {
                return null;
            }

            //var userid = HttpContext.Current.Session[Auth.Session].ToString();
            var employee_id = _httpContext.Session.GetString(Auth.Session);
            using (var context = new DigiSignContext())
            {
                var query = context.SignerEmployee.Where(s => s.EmployeeId == employee_id).FirstOrDefault();
                return query;
            }
        }
    }
}