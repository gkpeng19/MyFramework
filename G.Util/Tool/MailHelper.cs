using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Tool
{
    public static class MailHelper
    {
        public static void SendMail(string host, int port, string send, string pwd, string recieve, string subject, string mailbody, params string[] attachments)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(recieve);
            //这个地方可以发送给多人，但是没有实现，可以用“，”分割，之后取得每个收件人的地址。
            //也可以抄送给多人。
            msg.From = new MailAddress(send);
            if (subject != null && subject.Length > 0)
            {
                msg.Subject = subject;
            }

            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码
            msg.Body = mailbody;
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            foreach (var attach in attachments)
            {
                Attachment data = new Attachment(attach);
                msg.Attachments.Add(data);
            }

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(send, pwd);
            client.Host = host;
            client.Port = port;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                client.Send(msg);
            }
            catch (System.Exception ex)
            {
                LogHelper.Logger.Fatal(ex.Message, ex);
            }
        }

        public static void SendQQMail(string send, string pwd, string recieve, string subject, string mailbody, params string[] attachments)
        {
            SendMail("smtp.qq.com", 25, send, pwd, recieve, subject, mailbody, attachments);
        }

        public static void Send163Mail(string send, string pwd, string recieve, string subject, string mailbody, params string[] attachments)
        {
            SendMail("smtp.163.com", 25, send, pwd, recieve, subject, mailbody, attachments);
        }
    }
}
