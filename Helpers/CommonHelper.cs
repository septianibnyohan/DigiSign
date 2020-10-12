using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;



namespace DigiSign.Helpers
{
    public class CommonHelper
    {
        private readonly IConfiguration _config;
        
        public CommonHelper(IConfiguration config)
        {
            _config = config;
        }

        public IConfigurationSection Env {
            get{
                return _config.GetSection("AppSettings");
            }
            
        }

        public static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings == null) return false;
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }

        public dynamic Responder(string status, string title, string message, string callback = null,
            bool notify = true, string notifyType = null, string callbackTime = "onconfirm")
        {
            //_config.GetSection("MySettings");
            dynamic response = new ExpandoObject();
            response.status = status;
            response.title = title;
            response.message = message;

            if (!CommonHelper.IsPropertyExist(response, "callback"))
            {
                response.callback = callback;
            }

            if (!notify)
            {
                response.notify = false;
            }

            if (!CommonHelper.IsPropertyExist(response, "notifyType"))
            {
                if (notifyType != null)
                {
                    response.notifyType = notifyType;
                }
            }

            if (callbackTime == "instant")
            {
                response.callbackTime = callbackTime;
            }

            return response;
        }
    }
}