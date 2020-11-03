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
using DigiSign.Services;
using Microsoft.Extensions.Options;
using DigiSign.Settings;
using System.Dynamic;

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
        private LDAPService _ldapservice;
        private readonly IMailService _mailService;
        private readonly AppConfiguration _appConfig;

        public SignerDocumentController(ILogger<SignerDocumentController> logger, IDigiSignRepository repo, 
            AuthHelper authHelper, IHostingEnvironment _hostingEnvironment, IConfiguration config, 
            IMailService mailService, IOptions<AppConfiguration> appConfig, SignerHelper signerHelper)
        {
            _logger = logger;
            _repository = repo;

            _authHelper = authHelper;
            _authHelper.initRepo(repo);

            _app = new App(_hostingEnvironment);
            _signerHelper = signerHelper;
            _signerHelper.initRepo(authHelper, repo);
            _commonHelper = new CommonHelper(config);
            _mailService = mailService;
            _appConfig = appConfig.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Uploads()
        {
            var user = _authHelper.User();
            ViewBag.user = user;
            using (var context = new DigiSignContext())
            {
                var classification = context.SignerMClassification.ToList();

                ViewBag.classification = classification;
            }
            return View();
        }

        public ContentResult UploadFile()
        {
            dynamic resp;
            try
            {
                var user = _authHelper.User();

                if (user == null)
                {
                    resp = _commonHelper.Responder("error", "Oops!", "Please Relogin..!!!", "redirect('" + _appConfig.APP_ADDRESS + "/Auth/Get/');", true, "swal");
                }

                var requestor = new SignerRequests
                {
                    Company = 1,
                    Sender = user.EmployeeId,
                    RequestorEmail = user.EmployeeEmail,
                    DepartmentId = user.DepartmentId,
                    SenderNote = Request.Form["notes"].ToString().Replace("\r\n", "<br>").Replace("<", " ").Replace(">", " "),
                    NextOrder = 0,
                    Status = "Draft",
                    WorkflowMethod = Request.Form["workflowType"],
                    ClassificationId = Convert.ToInt32(Request.Form["classification"]),
                    CreatedTime = DateTime.Now
                };

                using (var context = new DigiSignContext())
                {
                    context.SignerRequests.Add(requestor);
                    //var insert_one = context.SaveChanges();
                    var docKey = RandomGenerator.RandomString(32, false);

                    dynamic data = new ExpandoObject();
                    //data.request_id = requestor.id;
                    data.company = 1;
                    data.folder = Request.Form["folder"];
                    data.name = Request.Form["name"].ToString().Replace("<", " ").Replace(">", " ");
                    data.category_id = Request.Form["category"];
                    data.editted = "No";
                    data.accessibility = "Everyone";
                    data.public_permission = "sign_edit";
                    data.source = "form";
                    data.file = Request.Form.Files["fileupload"];
                    data.document_key = docKey;
                    data.activity = "File uploaded by <span class='text-primary'>" + user.employee_name + "</span>.";

                    if (requestor.WorkflowMethod == "COMBINE")
                    {
                        //var num = Request.Form.GetValues("num[]");
                        var num = Request.Form["num[]"];
                        var tipe = Request.Form["tipe[]"];

                        if (String.IsNullOrEmpty(num))
                        {
                            resp = _commonHelper.Responder("error", "Oops!", "Please check your data..");
                            return Content(JsonConvert.SerializeObject(resp), "application/json");
                        }

                        List<string> userlist = new List<string>();

                        foreach (var valueNum in num)
                        {
                            var wfSequenceInput = Request.Form["workflowSequenceInput" + valueNum + "[]"];

                            if (!String.IsNullOrEmpty(wfSequenceInput))
                            {
                                foreach (var val in wfSequenceInput)
                                {
                                    if (userlist.Contains(val))
                                    {
                                        resp = _commonHelper.Responder("error", "Oops!", "Users in workflow must unique..");
                                        return Content(JsonConvert.SerializeObject(resp), "application/json");
                                    }

                                    userlist.Add(val);
                                }
                            }

                            var wfParalelInput = Request.Form["workflowParalelInput" + valueNum + "[]"];
                            if (!String.IsNullOrEmpty(wfParalelInput))
                            {
                                foreach (var val in wfParalelInput)
                                {
                                    if (userlist.Contains(val))
                                    {
                                        resp = _commonHelper.Responder("error", "Oops!", "Users in workflow must unique..");
                                        return Content(JsonConvert.SerializeObject(resp), "application/json");
                                    }
                                    userlist.Add(val);
                                }
                            }
                        }

                        if (!ComposeWorkflowCombine(num, tipe, requestor, context, data, docKey, user))
                        {
                            resp = _commonHelper.Responder("error", "Oops!", "Approver cannot be same as Uploader..");
                            return Content(JsonConvert.SerializeObject(resp), "application/json");
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private bool ComposeWorkflowCombine(string[] num, string[] tipe, SignerRequests requestor, DigiSignContext context, dynamic data, 
            string docKey, SignerEmployee user)
        {
            try
            {
                var x = 0;

                var numCounter = 0;
                foreach (var valueNum in num)
                {
                    if (num.Length == 1)
                    {
                        var wfSequenceInput = Request.Form["workflowSequenceInput" + valueNum + "[]"];

                        if (!String.IsNullOrEmpty(wfSequenceInput))
                        {
                            if (wfSequenceInput.Count == 1)
                            {
                                foreach (var val in wfSequenceInput)
                                {
                                    if (user.EmployeeEmail == val)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }

                        var wfParalelInput = Request.Form["workflowParalelInput" + valueNum + "[]"];
                        if (!String.IsNullOrEmpty(wfParalelInput))
                        {
                            if (wfParalelInput.Count == 1)
                            {
                                foreach (var val in wfParalelInput)
                                {
                                    if (user.EmployeeEmail == val)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }

                    }

                    if (numCounter == 0)
                    {
                        var insert_one = context.SaveChanges();
                        data.request_id = requestor.Id;
                    }

                    if (tipe[numCounter] == "SEQ")
                    {
                        ComposeWokflowSeq(valueNum, context, data, docKey, user, ref x);
                    }
                    //else if (tipe[Convert.ToInt32(valueNum)] == "PAR")
                    else if (tipe[numCounter] == "PAR")
                    {
                        var workflowParalelInput = Request.Form["workflowParalelInput" + valueNum + "[]"];
                        foreach (var value in workflowParalelInput)
                        {
                            var employee = context.SignerEmployee.Where(o => o.EmployeeEmail == value).FirstOrDefault();
                            var signer_workflow_item = new SignerWorkflow
                            {
                                RequestId = data.request_id,
                                EmployeeId = Convert.ToInt32(employee.EmployeeId),
                                Document = docKey,
                                Email = employee.EmployeeEmail,
                                Status = "PENDING",
                                Notes = "PARALEL",
                                UpdatedBy = Convert.ToInt32(user.EmployeeId),
                                OrderBy = x
                            };
                            context.SignerWorkflow.Add(signer_workflow_item);
                            //var insertWorkflow = context.SaveChanges();

                        }
                        context.SaveChanges();
                        x++;
                    }

                    ++numCounter;
                }


            }
            catch (Exception ex)
            {
                //SignerHelper.LogEx(ex);
                throw ex;
            }

            return true;
        }

        private void ComposeWokflowSeq(string valueNum, docsdevEntities context, dynamic data, string docKey, signer_employee user, ref int x)
        {
            var workflowSequenceInput = Request.Form["workflowSequenceInput" + valueNum + "[]"];

            foreach (var value in workflowSequenceInput)
            {
                var employee = context.signer_employee.Where(o => o.employee_email == value).FirstOrDefault();
                var signer_workflow_item = new signer_workflow
                {
                    request_id = data.request_id,
                    employee_id = Convert.ToInt32(employee.employee_id),
                    document = docKey,
                    email = employee.employee_email,
                    status = "PENDING",
                    notes = "SEQUENCE",
                    updated_by = Convert.ToInt32(user.employee_id),
                    order_by = x
                };
                context.signer_workflow.Add(signer_workflow_item);
                //var insertWorkflow = context.SaveChanges();
                x++;
            }
            context.SaveChanges();
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

        public IActionResult DocsMonitoring()
        {
            return View("DocsMonitoring");
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

        private bool ComposeWorkflowNonCombile(signer_requests requestor, docsdevEntities context, dynamic data, string docKey, signer_employee user)
        {
            try
            {
                var approval = Request.Form["approval[]"];
                if (!String.IsNullOrEmpty(approval))
                {
                    if (approval.Count == 1)
                    {
                        foreach (var val in approval)
                        {
                            if (user.employee_email == val)
                            {
                                return false;
                            }
                        }
                    }
                }

                context.SaveChanges();
                data.request_id = requestor.id;
                if (requestor.workflow_method == "SEQUENCE")
                {

                    int index = 0;
                    foreach (var val in approval)
                    {
                        var employee = context.signer_employee.Where(o => o.employee_email == val).FirstOrDefault();
                        var sw = new signer_workflow
                        {
                            request_id = data.request_id,
                            employee_id = Convert.ToInt32(employee.employee_id),
                            document = docKey,
                            email = employee.employee_email,
                            status = "PENDING",
                            notes = "SEQUENCE",
                            updated_by = Convert.ToInt32(user.employee_id),
                            order_by = index
                        };

                        context.signer_workflow.Add(sw);
                        //var insertWorkflow = context.SaveChanges();
                        index++;
                    }
                    context.SaveChanges();
                }

                if (requestor.workflow_method == "PARALEL")
                {
                    foreach (var val in approval)
                    {
                        var employee = context.signer_employee.Where(o => o.employee_email == val).FirstOrDefault();
                        var sw = new signer_workflow
                        {
                            request_id = data.request_id,
                            employee_id = Convert.ToInt32(employee.employee_id),
                            document = docKey,
                            email = employee.employee_email,
                            status = "PENDING",
                            notes = "PARALEL",
                            updated_by = Convert.ToInt32(user.employee_id),
                            order_by = 0
                        };

                        context.signer_workflow.Add(sw);
                        var insertWorkflow = context.SaveChanges();
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<IActionResult> Revoke()
        {
            var user = _authHelper.User();
            var username = HttpContext.Session.Get("username");
            var password = (string)Request.Form["password"];
            var ldap = _ldapservice.Login(username, password);

            var requestId = Convert.ToInt32(Request.Form["request_id"]);
            var reason = (string)Request.Form["reason-cancel"];

            var revoked = _repository.signer_requests.First(o => o.id == requestId);
            revoked.status = "REVOKED";
            revoked.reason = reason;
            _repository.SaveChanges();

            // var request = new MailRequest{
            //     ToEmail = "septianibnyohan@gmail.com",
            //     Body = "Test",
            //     Subject = "Keren Amat"
            // };
            // await _mailService.SendEmailAsync(request);

            var email_to = new EmailTo
            {
                To = new List<String>(),
                Cc = new List<String>()
            };

            var signer_file = _repository.signer_file.FirstOrDefault(o => o.request_id == requestId);
            var category = _repository.signer_m_docs_category.FirstOrDefault(o => o.category_id == signer_file.category_id);

            var documentLink = _appConfig.APP_URL + "/SignerDocument/Open/" + signer_file.document_key;
            var emails = _repository.signer_workflow.Where(o => o.request_id == requestId && o.status == "APPROVED").Select(o => o.email).ToList();

            foreach (var email in emails)
            {
                email_to.To.Add(email);
            }

            var employee = _repository.signer_employee.FirstOrDefault(o => o.employee_id == user.employee_id);

            var send = await _mailService.Send(email_to, "[ONESIGN] Document Cancel by " + employee.employee_name,
                new MailBasic
                {
                    Title = "Signing invitation declined.",
                    SubTitle = "Click the link below to view document.",
                    ButtonText = "View Document",
                    ButtonLink = documentLink,
                    Message = employee.employee_email + " has cancel the document you had signed. " +
                        "<br><br> Request ID : 000000" + requestId +
                    "<br>Requestor : " + employee.employee_name +
                    " </br>Document Name : " + signer_file.name + "<br> Category : " +
                    category.category_name +
                    "<br><br>Reason : <br>" +
                    reason.Replace("\r\n", "<br/>") +
                    "<br><br>Click the link below to view the document.<br><br>Cheers!<br>" + _appConfig.APP_NAME + " Team"
                },
            "withbutton");

            var resp = _commonHelper.Responder("success", "Alright!", "Document successfully saved.",
                                    "redirect('"+_commonHelper.Env.GetValue<string>("APP_URL")+"/SignerDocument/DocsMonitoring');", true, "swal");
            return Content(JsonConvert.SerializeObject(resp), "application/json");
        }
    }
}
