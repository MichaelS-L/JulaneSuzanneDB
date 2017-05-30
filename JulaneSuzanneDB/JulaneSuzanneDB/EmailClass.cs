using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;

namespace JulaneSuzanneDB
{
    class EmailClass {
        String emailTo;
        String emailFrom;
        String emailPW;

        public String[] sendMessage(String subject, String attach, String body = "") {
            String result1;
            String result2;

            result1 = "P";
            result2 = "";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailFrom);
            mail.To.Add(emailTo);
            mail.Subject = "Sending picture " + subject;

            if (body.Length == 0) {
                mail.Body = "Sending picture " + attach.Substring(attach.LastIndexOf("\\") + 1);
            }
            else {
                mail.Body = body;
            }

            Attachment attachment = new Attachment(attach);
            mail.Attachments.Add(attachment);

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, emailPW);
            //SmtpServer.UseDefaultCredentials = true;
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ee)
            {
                result1 = "F";
                result2 = ee.ToString();
            }

            return new String[] {result1, result2};
        } // public String sendMessage(String subject, String attach, String body = "")

        public String getFrom()
        {
            return emailFrom;
        }

        public void setFrom(String val)
        {
            emailFrom = val;
        }

        public String getPw()
        {
            return emailPW;
        }

        public void setPw(String val)
        {
            emailPW = val;
        }

        public String getTo()
        {
            return emailTo;
        }

        public void setTo(String val) {
            emailTo = val;
        }
    }
}
