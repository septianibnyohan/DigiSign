using System.Collections;
using DigiSign.Helpers;
using DigiSign.Models;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using System;

namespace DigiSign.Services
{
    public class LDAPService
    {
        private readonly IConfiguration _config;
        private CommonHelper _commonHelper;

        public LDAPService(IConfiguration config)
        {
            _config = config;
            _commonHelper = new CommonHelper(config);
        }

        public LDAP Login(string username, string password)
        {
            string domainName = _commonHelper.Env.GetValue<string>("LDAP");
            string path = "LDAP://" + domainName;
            string userDomain = domainName + @"\" + username;


            string DisplayName = "";
            string SAMaccountName = "";
            string Name = "";
            ArrayList member = new ArrayList();

            string userPrincipalName = "";
            string givenName = "";
            string description = "";
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(path, userDomain, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        //searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
                        searcher.Filter = "(SAMAccountName=" + username + ")";
                        searcher.PropertiesToLoad.Add("displayName");
                        searcher.PropertiesToLoad.Add("SAMAccountName");
                        searcher.PropertiesToLoad.Add("name");
                        searcher.PropertiesToLoad.Add("memberOf");
                        searcher.PropertiesToLoad.Add("userPrincipalName");
                        searcher.PropertiesToLoad.Add("givenName");
                        searcher.PropertiesToLoad.Add("description");
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            var displayName_ = result.Properties["displayName"];
                            var samAccountName_ = result.Properties["SAMAccountName"];
                            var name_ = result.Properties["name"];
                            var memberOf_ = result.Properties["memberOf"];
                            var userPrincipalName_ = result.Properties["userPrincipalName"];
                            var givenName_ = result.Properties["givenName"];
                            var description_ = result.Properties["description"];

                            DisplayName = displayName_ == null || displayName_.Count <= 0 ? null : displayName_[0].ToString();
                            SAMaccountName = samAccountName_ == null || samAccountName_.Count <= 0 ? null : samAccountName_[0].ToString();
                            Name = name_ == null || name_.Count <= 0 ? null : name_[0].ToString();

                            if (memberOf_.Count > 0)
                            {
                                foreach (var memberOff_ in memberOf_)
                                {
                                    member.Add(memberOff_);
                                }
                            }
                            else
                            {
                                member.Add(null);
                            }

                            //memberOf = memberOf_ == null || memberOf_.Count <= 0 ? null : memberOf_[0].ToString();
                            //string memberOf = memberOf_.ToString();
                            userPrincipalName = userPrincipalName_ == null || userPrincipalName_.Count <= 0 ? null : userPrincipalName_[0].ToString();
                            givenName = givenName_ == null || givenName_.Count <= 0 ? null : givenName_[0].ToString();
                            description = description_ == null || description_.Count <= 0 ? null : description_[0].ToString();

                            return new LDAP
                            {
                                status = true,
                                displayname = DisplayName,
                                samaccountname = SAMaccountName,
                                name = Name,
                                memberof = (string[])member.ToArray(typeof(string)),
                                userprincipalname = userPrincipalName,
                                givenname = givenName,
                                description = description,
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //SignerHelper.LogEx(ex);
                return new LDAP
                {
                    status = false,
                    message = ex.Message
                };
                // if we get an error, it means we have a login failure.
                // Log specific exception
            }

            return new LDAP
            {
                status = false,
                message = "False"
            };
        }
    }
}