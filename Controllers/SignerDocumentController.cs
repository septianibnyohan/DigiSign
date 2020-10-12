using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DigiSign.Models;
using DigiSign.Helpers;

namespace DigiSign.Controllers
{
    public class SignerDocumentController : Controller
    {
        private readonly ILogger<SignerDocumentController> _logger;
        private IDigiSignRepository _repository;
        private AuthHelper _authHelper;

        public SignerDocumentController(ILogger<SignerDocumentController> logger, IDigiSignRepository repo, AuthHelper authHelper)
        {
            _logger = logger;
            _repository = repo;

            _authHelper = authHelper;
            _authHelper.initRepo(repo);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Open(string id)
        {
            string document_key = id;
            var user = _authHelper.User();

            string activity = "SignerDocument Was Opened By <span class=\"text-primary\">" + user.employee_name + "</span>.";

            string requestPositions = "";
            int requestWidth = 0;
            dynamic template_fields;
            var saveWidth = 0;
            string lauchLabel;

            var document = _repository.Signer_Files.Where(o => o.document_key == document_key).FirstOrDefault();
            var s_request = _repository.Signer_Requests.Where(o => o.id == document.request_id).FirstOrDefault();

            bool isShowBtnDownload = true;

            var check = _repository.Signer_Workflows.Where(o => o.request_id == document.request_id && o.employee_id.ToString() == user.employee_id).FirstOrDefault();

            if (check == null)
            {
                //var check2 = context.signer_requests.Where(o => o.id == document.request_id && o.sender == user.employee_id).FirstOrDefault();
                //if (check2 == null)
                if (s_request.sender != user.employee_id)
                {
                    isShowBtnDownload = false;
                    var check_share = _repository.Signer_Files_Shares.Where(o => o.request_id == document.request_id && o.employee_id == user.employee_id).FirstOrDefault();

                    if (check_share == null)
                    {
                        return Redirect("~/Dashboard");
                    }

                }
            }

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

            var document = _repository.Signer_Files.Where(o => o.document_key == document_key).FirstOrDefault();
            var request = _repository.Signer_Requests.Where(o => o.id == document.request_id).FirstOrDefault();

            var net = new System.Net.WebClient();
            //http://103.195.31.221/filenet/MPnCF4uaZscG2oJLJ5JcyU94VoPt4A05.pdf
            var data = net.DownloadData(request.pdf_certified);
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            var fileName = document.filename;
            return File(content, contentType, fileName);
        }
    }
}
