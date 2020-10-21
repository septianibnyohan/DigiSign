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
        public SignerHelper(AuthHelper authHelper, IDigiSignRepository repository, IHttpContextAccessor contextAccessor)
        {
            //_httpContext = httpContext;
            _repository = repository;
            //_authHelper = new AuthHelper(repository, httpContext);
            _authHelper = authHelper;
            _authHelper.initRepo(repository);
            _apiBaseUrl = "";

            _contextAccessor = contextAccessor;
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

        private readonly object logging_lock = new object();

        public void Logging(string activity)
        {
            var user = _authHelper.User();
            _httpContext  = _contextAccessor.HttpContext;
            var request = _httpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            var request_uri = uriBuilder.Uri.ToString();

            lock (logging_lock)
            {
                _repository.signer_logs.Add(new signer_logs
                {
                    activity = activity,
                    ip_address = _httpContext.Connection.RemoteIpAddress.ToString(),
                    users = user == null ? 0 : Convert.ToInt32(user.employee_id),
                    uri = request_uri,
                    time_ = DateTime.Now
                });
                _repository.SaveChanges();
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