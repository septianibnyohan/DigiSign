using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DigiSign.Models;
using DigiSign.Helpers;
using DigiSign.Configs;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using DigiSign.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DigiSign.Controllers
{
    public class SignerDocumentController : Controller
    {
        private readonly ILogger<SignerDocumentController> _logger;
        private IDigiSignRepository _repository;
        private AuthHelper _authHelper;
        private App _app;
        private SignerHelper _signerHelper;
        private CommonHelper _commonHelper;

        public SignerDocumentController(ILogger<SignerDocumentController> logger, IDigiSignRepository repo, 
            AuthHelper authHelper, IHostingEnvironment _hostingEnvironment, IConfiguration config)
        {
            _logger = logger;
            _repository = repo;

            _authHelper = authHelper;
            _authHelper.initRepo(repo);

            _app = new App(_hostingEnvironment);
            _signerHelper = new SignerHelper(authHelper, repo);
            _commonHelper = new CommonHelper(config);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Open(string id)
        {
            HttpContext.Session.Set(Auth.Session, "1");

            string document_key = id;
            var user = _authHelper.User();

            string activity = "SignerDocument Was Opened By <span class=\"text-primary\">" + user.employee_name + "</span>.";

            string requestPositions = "";
            int requestWidth = 0;
            dynamic template_fields;
            var saveWidth = 0;
            string lauchLabel;

            var document = _repository.signer_file.Where(o => o.document_key == document_key).FirstOrDefault();
            var s_request = _repository.signer_requests.Where(o => o.id == document.request_id).FirstOrDefault();

            bool isShowBtnDownload = true;

            var check = _repository.signer_workflow.Where(o => o.request_id == document.request_id && o.employee_id.ToString() == user.employee_id).FirstOrDefault();

            if (check == null)
            {
                //var check2 = context.signer_requests.Where(o => o.id == document.request_id && o.sender == user.employee_id).FirstOrDefault();
                //if (check2 == null)
                if (s_request.sender != user.employee_id)
                {
                    isShowBtnDownload = false;
                    var check_share = _repository.signer_files_share.Where(o => o.request_id == document.request_id && o.employee_id == user.employee_id).FirstOrDefault();

                    if (check_share == null)
                    {
                        return Redirect("~/Dashboard");
                    }

                }
            }

            if (s_request.status == "Draft" && s_request.sender != user.employee_id)
            {
                return Redirect("~/Dashboard");
            }

            var requestDocs = _repository.signer_requests.Where(o => o.id == document.request_id).FirstOrDefault();

            if (requestDocs.status == "COMPLETED")
            {
                string file_check = _app.Storage + "completed\\" + document.filename;

                if (String.IsNullOrEmpty(document.guid))
                {
                    var token = await _signerHelper.GetToken();
                    _signerHelper.StoreToFileNet(document.filename, document.filename, requestDocs.id, token);
                }

                if (!System.IO.File.Exists(file_check))
                {
                    using (var client = new System.Net.WebClient())
                    {
                        int request_id = requestDocs.id;
                        var token = await _signerHelper.GetToken();
                        string url_filenet = _signerHelper.GetFileNet(request_id, token);
                        client.DownloadFile(url_filenet, file_check);
                    }
                }
            }

            if (document == null)
            {
                return View("/Errors/Error404");
            }

            if (document.is_template == "Yes")
            {
                lauchLabel = "Manage Fields & Edit";
                template_fields = JsonConvert.DeserializeObject(document.template_fields);
                saveWidth = template_fields[0];
            }
            else
            {
                lauchLabel = "Sign & Edit";
                template_fields = "";
                saveWidth = 0;
            }

            ViewBag.requestDocs_status = "PROGRESS";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Download(string id)
        {
            string document_key = id;

            var document = _repository.signer_file.Where(o => o.document_key == document_key).FirstOrDefault();
            var request = _repository.signer_requests.Where(o => o.id == document.request_id).FirstOrDefault();

            var net = new System.Net.WebClient();
            //http://103.195.31.221/filenet/MPnCF4uaZscG2oJLJ5JcyU94VoPt4A05.pdf
            var data = net.DownloadData(request.pdf_certified);
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            var fileName = document.filename;
            return File(content, contentType, fileName);
        }

        public IActionResult Revoke()
        {
            var requestId = Convert.ToInt32(Request.Form["request_id"]);
            var reason = (string)Request.Form["reason"];

            var revoked = _repository.signer_requests.First(o => o.id == requestId);
            revoked.status = "Revoked";
            _repository.SaveChanges();

            var resp = _commonHelper.Responder("success", "Alright!", "Document successfully saved.",
                                    "redirect('"+_commonHelper.Env.GetValue<string>("APP_URL")+"/SignerDocument/DocsMonitoring');", true, "swal");
            return Content(JsonConvert.SerializeObject(resp), "application/json");
        }
    }
}
