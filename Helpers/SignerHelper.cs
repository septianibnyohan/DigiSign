using DigiSign.Configs;
using DigiSign.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace DigiSign.Helpers
{
    public class SignerHelper
    {
        private Microsoft.AspNetCore.Http.HttpContext _httpContext;
        private IHttpContextAccessor _contextAccessor;
        private IDigiSignRepository _repository;
        private AuthHelper _authHelper;
        private string _apiBaseUrl;
        public SignerHelper(IHttpContextAccessor contextAccessor)
        {
            //_httpContext = httpContext;
            _apiBaseUrl = "";

            _contextAccessor = contextAccessor;
        }

        public void initRepo(AuthHelper authHelper, IDigiSignRepository repository)
        {
            _repository = repository;
            //_authHelper = new AuthHelper(repository, httpContext);
            _authHelper = authHelper;
            _authHelper.initRepo(repository);
        }

        public bool KeepHistory(string document_key, string activity, string type = "default")
        {

            _repository.signer_history.Add(
                new signer_history { 
                    files = document_key, 
                    activity = activity, 
                    type = type, 
                    time_ = DateTime.Now 
                }
            );
            _repository.SaveChanges();

            return true;
        }

        public async Task<string> GetToken()
        {
            string token = null;
            try
            {
                var jsonData = new
                {
                    username = "DIGITAL_SIGNER_ISM",
                    password = "12345"
                };

                using (HttpClient client = new HttpClient())  
                { 
                    StringContent content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
                    string endpoint = _apiBaseUrl + "/login";  
  
                    using (var response = await client.PostAsync(endpoint, content))  
                    {  
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var js = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                        token = js.token;
                    }  
                }
            }
            catch(Exception ex)
            {

            }

            return token;
        }

        public string GetFileNet(int requestID, string token)
        {
            string url = null;
            try
            {
                // if (CommonHelper.Env["BYPASS_FILENET"] != "TRUE")
                // {
                //     string guid;
                //     string pdffilename;
                //     using (var context = new docsdevEntities())
                //     {
                //         var signer_file = context.signer_file.FirstOrDefault(o => o.request_id == requestID);
                //         guid = signer_file.guid;
                //         pdffilename = signer_file.filename;
                //     }

                //     guid = guid.Replace("{", "").Replace("}", "");

                //     var jsonData = new
                //     {
                //         Guid = guid,
                //         Pdffilename = pdffilename
                //     };

                //     var authorization = "Bearer " + token;
                //     var reqsvc = JsonConvert.SerializeObject(jsonData);
                //     var endpoint = new RestClient(CommonHelper.Env["APP_URL_API"] + "/api/filenet/download");
                //     endpoint.Timeout = -1;
                //     var request = new RestRequest(Method.POST);
                //     request.AddParameter("application/json", reqsvc, ParameterType.RequestBody);
                //     request.AddHeader("Authorization", authorization);
                //     request.AddHeader("Content-Type", "application/json");
                //     request.AddHeader("Content-Length", reqsvc.Length.ToString());
                //     var response = endpoint.Execute(request);

                //     var data_svc = response.Content;
                //     var js = JsonConvert.DeserializeObject<dynamic>(data_svc);
                //     var _token = JsonConvert.SerializeObject(js);

                //     Logging("Get FileNet reqId = " + requestID + " DATA => " + JsonConvert.SerializeObject(jsonData) + " Response :" + JsonConvert.SerializeObject(_token));

                //     return js.url;
                // }

                return url;
            }
            catch (Exception ex)
            {
                //LogEx(ex);
                throw ex;
            }

            return url;
        }

        public bool StoreToFileNet(string pdffile, string document_name, int requestID, string token)
        {
            try
            {
                // if (CommonHelper.Env["BYPASS_FILENET"] != "TRUE")
                // {
                //     var jsonData = new
                //     {
                //         Pdffile = pdffile,
                //         DocumentTitle = document_name.Replace(".pdf", ""),
                //         RequestID = requestID
                //     };

                //     var authorization = "Bearer " + token;
                //     var reqsvc = JsonConvert.SerializeObject(jsonData);
                //     var endpoint = new RestClient(CommonHelper.Env["APP_URL_API"] + "/api/filenet/upload");
                //     endpoint.Timeout = -1;
                //     var request = new RestRequest(Method.POST);
                //     request.AddParameter("application/json", reqsvc, ParameterType.RequestBody);
                //     request.AddHeader("Authorization", authorization);
                //     request.AddHeader("Content-Type", "application/json");
                //     request.AddHeader("Content-Length", reqsvc.Length.ToString());
                //     var response = endpoint.Execute(request);

                //     var data_svc = response.Content;
                //     var js = JsonConvert.DeserializeObject<dynamic>(data_svc);
                //     var _token = JsonConvert.SerializeObject(js);

                //     Logging("Store To FileNet reqId = " + requestID + " DATA => " + JsonConvert.SerializeObject(jsonData) + " Response :" + JsonConvert.SerializeObject(_token));

                //     using (var context = new docsdevEntities())
                //     {
                //         var signer_file = context.signer_file.SingleOrDefault(o => o.request_id == requestID);
                //         signer_file.guid = js.guid;
                //         context.SaveChanges();
                //     }
                // }
                return true;
            }
            catch (Exception ex)
            {
                //LogEx(ex);
                throw ex;
            }

            return true;
        }

        private Uri GetUri()
        {
            var request = _contextAccessor.HttpContext.Request;
            var builder = new UriBuilder();
            builder.Scheme = request.Scheme;
            builder.Host = request.Host.Value;
            builder.Path = request.Path;
            builder.Query = request.QueryString.ToUriComponent();
            return builder.Uri;
        }

        public void LogEx(Exception ex)
        {
            string message = "Message : " + ex.Message + ", Stacktrace : " + ex.StackTrace;

            if (ex.InnerException != null)
            {
                message += "Inner Message : " + ex.InnerException.Message + ", Inner Stacktrace : " + ex.InnerException.StackTrace;
            }

            Logging(message);
        }

        private readonly object logging_lock = new object();

        public void Logging(string activity)
        {
            var user = _authHelper.User();
            using (var context = new DigiSignContext())
            {
                lock (logging_lock)
                {
                    context.SignerLogs.Add(new SignerLogs
                    {
                        Activity = activity,
                        //ip_address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
                        IpAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                        Users = user == null ? 0 : Convert.ToInt32(user.EmployeeId),
                        //uri = HttpContext.Current.Request.ServerVariables["REQUEST_URI"],
                        Uri = GetUri().ToString(),
                        Time = DateTime.Now
                    });
                    context.SaveChanges();
                }

            }
        }

        public string GetEmployeeName(string nik)
        {

            var signer_employee = _repository.signer_employee.FirstOrDefault(o => o.employee_id == nik);

            if (signer_employee == null) return null;

            return signer_employee.employee_name;
        }
    }
    
}