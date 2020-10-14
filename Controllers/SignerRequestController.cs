using DigiSign.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DigiSign.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using DigiSign.Configs;
using DigiSign.Infrastructure;

namespace DigiSign.Controllers
{
    public class SignerRequestController : Controller
    {
        // GET: Notification

        private readonly IConfiguration _config;
        private IDigiSignRepository repository;
        private AuthHelper _authHelper;
        private SignerHelper _signerHelper;
        
        public SignerRequestController(IConfiguration config, IDigiSignRepository repo, AuthHelper authHelper)
        {
            _config = config;
            repository = repo;
            
            //_authHelper = new AuthHelper(repository);
            _authHelper = authHelper;
            _authHelper.initRepo(repo);
            _signerHelper = new SignerHelper(_authHelper, repository);

            
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ContentResult Send()
        {
            dynamic respo;
            try
            {
                HttpContext.Session.Set(Auth.Session, "1");
                
                var user = _authHelper.User();
                var mail = new MailHelper();

                if (!ValidateStamperPos(Request.Form["positions"]))
                {
                    var initResp = new CommonHelper(_config).Responder("error", "Oops!", "Stamper position is not valid", "reload()");

                    return Content(JsonConvert.SerializeObject(initResp), "application/json");
                }

                var form_document_key = Request.Form["document_key"].ToString();
                var document = repository.signer_file.Where(o => o.document_key == form_document_key).FirstOrDefault();
                _signerHelper.Logging("Assign Pointing Signature, The Request Signature Req ID:" + document.request_id);
                var requestsData = repository.signer_requests.Where(o => o.id == document.request_id).Where(o => o.next_order == 0)
                        .Where(o => o.status == "Draft").FirstOrDefault();
                var workflow = repository.signer_workflow.Where(o => o.request_id == document.request_id).Select(o => o.email).ToList();
                var name = JsonConvert.DeserializeObject(Request.Form["name"]);
                var emails = JsonConvert.DeserializeObject<dynamic>(Request.Form["emails"]);

                var message = Request.Form["message"];
                var documentKey = document.document_key;
                var duplicate = Request.Form["duplicate"];
                var chain = Request.Form["chain"];
                dynamic chainEmails = null;
                dynamic chainPositions = null;

                var positions = new List<dynamic>();
                string activity = "";
                //if (emails.Count == 1)
                if (Enumerable.Count(emails) == 1)
                {
                    var first_email = System.Linq.Enumerable.FirstOrDefault(emails);
                    activity = "Signing request sent to <span class=\"text - primary\">" + first_email.employee_email + "</span> by <span class=\"text-primary\">" + user.employee_name + "</span>";
                }
                else
                {
                    activity = "Signing request sent to <span class=\"text - primary\">" + String.Join(",", emails) + "</span> by <span class=\"text-primary\">" + user.employee_name + "</span>";
                }

                if (!string.IsNullOrWhiteSpace(Request.Form["positions"]) && document.is_template == "No")
                {
                    var positionsOriginal = JsonConvert.DeserializeObject<dynamic>(Request.Form["positions"]);

                    if (emails != null)
                    {
                        foreach (string positionsSingle in positionsOriginal)
                        {
                            var obj_positionsSingle = JsonConvert.DeserializeObject<dynamic>(positionsSingle);
                            if (obj_positionsSingle != null)
                            {
                                if (Request.Form["docWidth"] != "set")
                                {
                                    var docWidth = (string)Request.Form["docWidth"];
                                    obj_positionsSingle.Insert(0, docWidth);
                                }
                                positions.Add(JsonConvert.SerializeObject(obj_positionsSingle));
                            }
                            else
                            {
                                positions.Add("");
                            }
                        }
                    }
                    else
                    {
                        var obj_positionsSingle = JsonConvert.DeserializeObject<dynamic>(positionsOriginal[0].ToString());
                        if (Request.Form["docWidth"] != "set")
                        {
                            var docWidth = (string)Request.Form["docWidth"];
                            obj_positionsSingle.Insert(0, docWidth);
                        }
                        positions[0] = JsonConvert.SerializeObject(obj_positionsSingle);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(Request.Form["positions"]) && document.is_template == "Yes")
                {
                    foreach (var email in emails)
                    {
                        positions.Add(Request.Form["positions"]);
                    }
                }
                else
                {
                    foreach (var email in emails)
                    {
                        positions.Add("");
                    }
                }

                if (chain == "Yes")
                {
                    chainEmails = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(emails));
                    chainEmails.RemoveAt(0);
                    chainEmails = JsonConvert.SerializeObject(chainEmails);
                    chainPositions = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(positions));
                    chainPositions.RemoveAt(0);
                    chainPositions = JsonConvert.SerializeObject(chainPositions);
                }

                var index = 0;
                foreach (var email in emails)
                {
                    var signingKey = RandomGenerator.RandomString(32);
                    string employee_email = email.employee_email;

                    var result = repository.signer_workflow.Where(o => o.request_id == document.request_id && 
                        o.document == document.document_key && o.email == employee_email).ToList();
                    result.ForEach(o =>
                    {
                        o.sender_note = requestsData.sender_note;
                        o.chain_emails = chainEmails;
                        o.chain_positions = chainPositions;
                        o.signing_key = signingKey;
                        o.positions = positions[index];
                        o.updated_at = DateTime.Now;
                        o.updated_by = Convert.ToInt32(user.employee_id);
                    });
                    index++;
                    //context.saveChanges()
                }
                //context.saveChanges()

                if (requestsData.workflow_method == "SEQUENCE")
                {
                    var sendEmail = repository.signer_workflow.Where(o => o.request_id == document.request_id).Where(o => o.status == "PENDING" && o.notes == "SEQUENCE" && o.order_by == requestsData.next_order);

                    foreach (var sent in sendEmail)
                    {
                        var employee = repository.signer_employee.FirstOrDefault(o => o.employee_id == requestsData.sender);
                        var employee_to = repository.signer_employee.Where(o => o.employee_id == sent.employee_id.ToString()).FirstOrDefault();
                        //var category = repository..signer_m_docs_category.Where(o => o.category_id == document.category_id).FirstOrDefault();
                        //var signingLink = CommonHelper.Env["APP_URL"] + "/Guest/Open/" + sent.document + "?signingkey=" + sent.signing_key;
                    }
                }
                else if (requestsData.workflow_method == "PARALEL")
                {

                }
                else if (requestsData.workflow_method == "COMBINE")
                {

                }


                respo = new CommonHelper(_config).Responder("success", "Sent!", "Request successfully sent.", "reload()");
                return Content(JsonConvert.SerializeObject(respo), "application/json");
            }
            catch (Exception ex)
            {
                //SignerHelper.LogEx(ex);
                respo = new CommonHelper(_config).Responder("error", "Oops!", "Something Error");
                return Content(JsonConvert.SerializeObject(respo), "application/json");
            }
        }

        public bool ValidateStamperPos(string positions)
        {
            try
            {
                var positionsOriginal = JsonConvert.DeserializeObject<dynamic>(positions);
                int image_counter = 0;

                foreach (string positionSingle in positionsOriginal)
                {
                    var obj_positionSingle = JsonConvert.DeserializeObject<dynamic>(positionSingle);

                    foreach (var item in obj_positionSingle)
                    {
                        if (item.type == "image")
                        {
                            ++image_counter;
                            var xPos = (double)item.xPos;
                            var yPos = (double)item.yPos;

                            if (xPos <= 0 || yPos <= 0)
                            {
                                return false;
                            }
                        }

                    }
                }

                return image_counter > 0;
            }
            catch (Exception ex)
            {
                //SignerHelper.LogEx(ex);
                return false;
            }
        }
    }
}