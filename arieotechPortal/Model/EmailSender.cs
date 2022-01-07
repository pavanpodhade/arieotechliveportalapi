
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using ArieotechLive.Repository;
using System;
using MailKit.Net.Smtp;

namespace Model
{

    public class EmailSender

    {
        #region Private Vars
        public IConfiguration configuration;
        private readonly ILoggerManager loggerManager;
        #endregion

        #region ctor

        public EmailSender(IConfiguration configuration, ILoggerManager loggerManager)
        {
            this.configuration = configuration;
            this.loggerManager = loggerManager;
        }
        #endregion


        #region Public Methods for send , SendForgotPasswordMail and ResetPassword
        public void Send(string to)
        {

            try
            {
                loggerManager.LogInfo(string.Format("Email send method is called , to:{0}", to));

                string from = this.configuration.GetSection("EmailSender").GetSection("From").Value.ToString();
               // string from = "priyankaghodekar32@gmail.com";
                string subject = this.configuration.GetSection("EmailSender").GetSection("Subject").Value.ToString();
                string html = this.configuration.GetSection("EmailSender").GetSection("Html").Value.ToString();
                string password = this.configuration.GetSection("EmailSender").GetSection("Password").Value.ToString();
                string server = this.configuration.GetSection("Smtp").GetSection("Server").Value.ToString();
                string port1 = this.configuration.GetSection("Smtp").GetSection("Port").Value.ToString();
                int port = int.Parse(port1);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(server, port, SecureSocketOptions.StartTls);
                smtp.Authenticate(from, password);
                smtp.Send(email);
                loggerManager.LogInfo(string.Format("Email send method is completed , to:{0}", to));

                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while sending email  --> {0}, details --> {1}, to-->{2}", ex.Message, ex.StackTrace, to));

            }


        }
        public void SendForgotPasswordMail(string to, string fpToken)
        {

            try
            {
                loggerManager.LogInfo(string.Format("Email send forgot password method is called , to:{0} , Token:{1}", to, fpToken));

                string from = this.configuration.GetSection("EmailForgotPassword").GetSection("From").Value.ToString();
                //string from = "priyankaghodekar23@gmail.com";
                string subject = this.configuration.GetSection("EmailForgotPassword").GetSection("Subject").Value.ToString();
                string html = this.configuration.GetSection("EmailForgotPassword").GetSection("Html").Value.ToString();
                string password = this.configuration.GetSection("EmailSender").GetSection("Password").Value.ToString();
                string server = this.configuration.GetSection("Smtp").GetSection("Server").Value.ToString();
                string port1 = this.configuration.GetSection("Smtp").GetSection("Port").Value.ToString();
                int port = int.Parse(port1);
              
                /* var email = new MimeMessage();
                 email.From.Add(MailboxAddress.Parse(from));
                 email.To.Add(MailboxAddress.Parse(to));
                 email.Subject = subject;
                 html = html.Replace("#FP_TOKEN#", fpToken);
                 email.Body = new TextPart(TextFormat.Html) { Text = html };*/

                // send email
                /*
                                using var smtp = new SmtpClient();
                                smtp.Connect(server, port, SecureSocketOptions.StartTls);
                                smtp.Authenticate(from, password);
                                smtp.Send(email);
                                smtp.Disconnect(true);
                                loggerManager.LogInfo(string.Format("Email send forgot password method is completed , to:{0} , Token:{1}", to, fpToken));*/

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = "Test Email Subject";
                html = html.Replace("#FP_TOKEN#", fpToken);
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(server,port, SecureSocketOptions.StartTls);
                smtp.Authenticate(from,password);
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {

                this.loggerManager.LogError(string.Format("Error while sending forgot password email  --> {0}, details --> {1}, to-->{2}", ex.Message, ex.StackTrace, to));
            }

        }
        public void ResetPassword(string to)
        {

            try
            {
                loggerManager.LogInfo(string.Format("Email reset password method is called , to:{0}", to));
               // string from ="priyankaghodekar32@gmail.com";
                
                string from = this.configuration.GetSection("ResetPassword").GetSection("From").Value.ToString();
                string subject = this.configuration.GetSection("ResetPassword").GetSection("Subject").Value.ToString();
                string html = this.configuration.GetSection("ResetPassword").GetSection("Html").Value.ToString();
                string password = this.configuration.GetSection("EmailSender").GetSection("Password").Value.ToString();
                string server = this.configuration.GetSection("Smtp").GetSection("Server").Value.ToString();
                string port1 = this.configuration.GetSection("Smtp").GetSection("Port").Value.ToString();
                int port = int.Parse(port1);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email

                using var smtp = new SmtpClient();
                smtp.Connect(server, port, SecureSocketOptions.StartTls);
                smtp.Authenticate(from, password);
                smtp.Send(email);
                smtp.Disconnect(true);
                loggerManager.LogInfo(string.Format("Email reset password method is completed , to:{0}", to));


            }
            catch (Exception ex)
            {
                this.loggerManager.LogError(string.Format("Error while sending reset password email  --> {0}, details --> {1}, to-->{2}", ex.Message, ex.StackTrace, to));
            }

        }

        #endregion



    }
}
