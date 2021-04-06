using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace vaccine_watch
{
    public interface IVaccineAlert
    {
        public void SendAlert(List<string> locations);
    }
    public class VaccineAlert : IVaccineAlert
    {
        private readonly IConfigurationRoot  _config;
        
        public VaccineAlert(IConfigurationRoot  config)
        {
            _config = config;
        }
        public void SendAlert(List<string> locations)
        {
            var email = _config.GetSection("EmailInfo:Email").Value;
            var displayName = _config.GetSection("EmailInfo:DisplayName").Value;
            var pass = _config.GetSection("EmailInfo:Password").Value;
            
            var fromAddress = new MailAddress(email, "Vaccine Watch");
            var toAddress = new MailAddress(email, displayName);
            string fromPassword = pass;
            string subject = "Vaccine Available";
            string link = $"{Environment.NewLine}{Environment.NewLine}https://www.cvs.com/immunizations/covid-19-vaccine?icid=cvs-home-hero1-link2-coronavirus-vaccine";
            string body = "Vaccines available at";
            
            
            foreach(string location in locations)
            {
                body += $" {Environment.NewLine},{location}";
            }

            body += link;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 10000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}