using DigiSign.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DigiSign.Models;
using System.Linq;

namespace DigiSign.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification

        private readonly IConfiguration _config;
        private IDigiSignRepository repository;
        private AuthHelper _authHelper;
        
        public NotificationController(IConfiguration config, IDigiSignRepository repo, AuthHelper authHelper)
        {
            _config = config;
            repository = repo;
            _authHelper = authHelper;
            _authHelper.initRepo(repo);
        }

        // public NotificationController()
        // {
            
        // }

        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Count()
        {   
            var user = _authHelper.User();

            var count = 0;

            var resp = new CommonHelper(_config).Responder("success", "", "", "updateNotificationsCount(" + count + ")", false);
            //return Json(resp, JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(resp), "application/json");
        }
    }
}