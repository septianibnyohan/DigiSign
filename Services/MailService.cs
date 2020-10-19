using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using DigiSign.Models;
using DigiSign.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorLight;

namespace DigiSign.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly AppConfiguration _appConfig;
        public MailService(IOptions<MailSettings> mailSettings, IOptions<AppConfiguration> appConfig)
        {
            _mailSettings = mailSettings.Value;
            _appConfig = appConfig.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task<bool> Send(EmailTo email_to, string subject, MailBasic view_data, string view_name = "Basic", string from = null, string[] attachments = null)
        {
            try
            {

                MailMessage mail = new MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(_mailSettings.Host);

                // mail.To.Add("to_address");
                //mail.Subject = "Test Mail";
                //mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = (int)_mailSettings.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                SmtpServer.EnableSsl = true;

                view_data.AppUrl = _appConfig.APP_URL;
                view_data.AppLogo = _appConfig.APP_URL + "/uploads/app/" + _appConfig.APP_LOGO;
                view_data.CopyRight = "&copy; " + DateTime.Now.Year + " " + _appConfig.APP_NAME + " | All Rights Reserved.";

                if (from == null)
                {
                    from = _mailSettings.Mail;
                }

                var arrayEmail = from.Replace(">", "").Split('<');
                var from_name = arrayEmail[0].Trim();
                var from_email = arrayEmail[1].Trim();

                from_email = from_email == null ? from_name : from_email;
                mail.From = new MailAddress(from_email);

                var to = new
                {
                    To = email_to.To,
                    //CC = CommonHelper.IsPropertyExist(email_to, "CC") ? email_to.CC : new string[0],
                    CC = email_to.Cc,
                    //Bcc = CommonHelper.IsPropertyExist(email_to, "Bcc") ? email_to.Bcc : new string[0],
                    Bcc = email_to.Bcc,
                    //Attachments = CommonHelper.IsPropertyExist(email_to, "Attachments") ? email_to.Attachments : new string[0],
                    Attachments = email_to.Attachments
                };

                foreach (var recipient in to.To)
                {
                    //var arrayRecipient = recipient.Replace(">", "").Split('<');
                    //var to_name = arrayRecipient[1].Trim();
                    //var to_email = arrayRecipient[0].Trim();
                    mail.To.Add(recipient);
                }

                if (to.CC != null)
                {
                    foreach (var recipient in to.CC)
                    {
                        //var arrayRecipient = recipient.Replace(">", "").Split('<');
                        //var cc_name = arrayRecipient[1].Trim();
                        //var cc_email = arrayRecipient[0].Trim();
                        mail.CC.Add(recipient);
                    }
                }

                if (to.Bcc != null)
                {
                    foreach (var recipient in to.Bcc)
                    {
                        //var arrayRecipient = recipient.Replace(">", "").Split('<');
                        //var bcc_name = arrayRecipient[1].Trim();
                        //var bcc_email = arrayRecipient[0].Trim();
                        mail.Bcc.Add(recipient);
                    }
                }

                if (to.Attachments != null)
                {
                    foreach (var value in to.Attachments)
                    {
                        mail.Attachments.Add(value);
                    }
                }

                mail.IsBodyHtml = true;
                mail.Subject = subject;
                //mail.Body = ConvertViewToString("~/Views/Emails/Html/" + view_name + ".cshtml", view_data);
                var body_html = await GetParsedContent("~/Views/Emails/Html/" + view_name + ".cshtml", view_data);
                body_html = body_html.Replace("\r\n", "");
                mail.Body = body_html;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                //SignerHelper.LogEx(ex);
                return false;
            }

            return true;
        }

        public async Task<string> ParseTemplate<T>(T model, string content)
        {
            //return Razor.Parse(content, model);
            var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(Program))
                .UseMemoryCachingProvider()
                .Build();

            string result = await engine.CompileRenderStringAsync("templateKey", content, model);

            return result;
        }

        public async Task<string> GetParsedContent(string viewName, object model)
        {
            //Get the template content from file system (optional)
            //var templatePath = HttpContext.Current.Server.MapPath(viewName);
            var app_path = _appConfig.APP_PATH;
            var templatePath = viewName.Replace("~", app_path);

            var templateContent = string.Empty;
            using (var reader = new StreamReader(templatePath))
            {
                templateContent = reader.ReadToEnd();
            }

            //Just call ParseTemplate() method
            var result = await ParseTemplate(model, templateContent);
            return result;
        }
    }
    
}