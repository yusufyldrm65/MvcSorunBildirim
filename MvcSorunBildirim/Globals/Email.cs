using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MvcSorunBildirim.Globals
{
    public class Email
    {
        private const string HtmlEmailHeader = "<html><head><title></title><style type='text/css'>body{font-family:Arial, sans-serif;font-size:14px;}.tbl_head,.tg{border-collapse:collapse;border-spacing:0}.tg td,.tg th{border-style:solid;border-width:1px;overflow:hidden;word-break:normal}.font-xl{font-size:24px}.font-lg{font-size:20px}.font-md{font-size:16px}.font-sm{font-size:14px}.font-xs{font-size:12px}.font-xxs{font-size:10px}.tbl_head,.tg td,.tg th{font-family:Arial,sans-serif;font-size:13px}.tbl_head{padding:8px 5px}.dark{background:#999;color:#FFF}.tbl_head td{padding:4px 3px}.tg td{padding:5px}.tg th{padding:7px 2px;background-color:#454545;color:#FFF;font-weight:700}.tg .tg-yw4l{vertical-align:top}.center{text-align:center}.right{text-align:right}.tg tbody tr:nth-child(even){background-color:#F4F4F4}.bg-dark{background-color:#000000;color:#FFFFFF;padding:10px;font-size:14px;}</style></head><body style='font-family:arial; font-size:13px;'>";
        private const string HtmlEmailFooter = "<p></p><hr size='2' width='100%' align='center' /><font face='verdana' size='1' color='gray'>Bu e-posta ve ekleri kişi ya da kuruma özeldir ve yasal olarak gizlilik gerektirebilir. Eğer mesajın gönderilmek istendiği alıcı siz değilseniz bu mesajın herhangi bir parçasını iletme, kopyalama, dağıtma, açıklama, saklama veya kullanma hakkına sahip olamazsınız. Bu mesajı bir hata sonucu aldıysanız lütfen mesajı ve sisteminizdeki tüm kopyalarını siliniz ve gönderene derhal bildiriniz. Bu mesajin zamanında, virüssüz veya hatasız gönderilmesi garanti edilmemektedir. Gönderen, meydana gelebilecek hatalardan veya eksikliklerden dolayı sorumluluk kabul etmemektedir.<br>This e-mail and included attachments are confidential and may also be legally privileged. You may not forward, copy, distribute, disclose, retain or use any part of it, if you are not the intended addressee. If you have received this correspondence in error, please delete it and all copies of it from your system and notify the sender immediately. The delivery of this message cannot be guaranteed to be timely secure, error or virus-free. The sender does not accept liability for any errors or omissions that may occur. </font></body></html>";

        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Email()
        {
            To = new List<string>();
            CC = new List<string>();
            BCC = new List<string>();
        }

        public void Send()
        {
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                if (To == null && CC == null && BCC == null)
                    return;
                if (To != null)
                    foreach (var x in To)
                    {
                        message.To.Add(x);
                    }
                if (CC != null)
                    foreach (var x in CC)
                    {
                        message.CC.Add(x);
                    }
                if (BCC != null)
                    foreach (var x in BCC)
                    {
                        if (!string.IsNullOrEmpty(x))
                            message.Bcc.Add(x);
                    }

                message.Subject = Subject;
                message.Body = string.Concat(HtmlEmailHeader, Body, HtmlEmailFooter);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.From = new System.Net.Mail.MailAddress(From);
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                System.Net.Mail.SmtpClient client = GetSmtpClient();

                client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                var a = 5;
            }

        }
        public async System.Threading.Tasks.Task SendForTask()
        {
            using (System.Net.Mail.SmtpClient client = GetSmtpClient())
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                foreach (var x in To)
                {
                    message.To.Add(x);
                }
                foreach (var x in CC)
                {
                    message.CC.Add(x);
                }
                foreach (var x in BCC)
                {
                    message.Bcc.Add(x);
                }
                message.Subject = Subject;
                message.Body = string.Concat(HtmlEmailHeader, Body, HtmlEmailFooter);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.From = new System.Net.Mail.MailAddress(From);
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                await client.SendMailAsync(message);
            }
        }
        public System.Net.Mail.SmtpClient GetSmtpClient()
        {
            return this.GetSmtpClient(ConfigurationManager.AppSettings["SmtpServer"], ConfigurationManager.AppSettings["SmtpUserName"], ConfigurationManager.AppSettings["SmtpPassword"], Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]), Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]));
        }
        public System.Net.Mail.SmtpClient GetSmtpClient(string MailServer, string UserName, string Password, int Port, bool EnableSSL)
        {
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            smtpClient.Host = MailServer;
            smtpClient.Port = Port;
            smtpClient.EnableSsl = EnableSSL;
            if (!string.IsNullOrEmpty(UserName))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(UserName, Password);
            }
            return smtpClient;
        }
    }
}