using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiSign.Configs
{
    public class Auth
    {
        public static string Locale
        {
            get
            {
                return "";
            }
        }

        public static string Remember
        {
            get
            {
                return "";
            }
        }

        public static string Secret
        {
            get
            {
                return "";
            }
        }

        public static string Session
        {
            get
            {
                return "user";
            }
        }

        public static string Mode
        {
            get
            {
                return "employee";
            }
        }

        public static string Table
        {
            get
            {
                return "users";
            }
        }

        public static string EmailColumn
        {
            get
            {
                return "email";
            }
        }

        public static string PasswordColumn
        {
            get
            {
                return "password";
            }
        }

        public static string PasswordTokenColumn
        {
            get
            {
                return "token";
            }
        }

        public static string StatusColumn
        {
            get
            {
                return "status";
            }
        }
    }
}