using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSorunBildirim.Globals
{
    public class BasvuruMail
    {
        public bool Send(List<string> To, List<string> CC, List<string> BCC, string Subject, string Message)
        {
            try
            {
                Email email = new Email();


                System.Text.StringBuilder strBody = new System.Text.StringBuilder();
                strBody.Append(@"<table width='100%' class='tbl_head'>
                                    <tr>
                                        <td valign='middle' height='50' class='bg-dark'><b>" + Subject + @"</b></td>
                                        <td valign='middle' class='bg-dark' style='text-align:right'>" + HttpContext.Current.Request.Url.Authority + @"</td>
                                    </tr>
                                </table>");
                strBody.Append(Message);
                email.BCC = BCC;
                email.To = To;
                email.CC = CC;
                email.From = System.Configuration.ConfigurationManager.AppSettings["SmtpEMailAdress"];

                email.Body = strBody.ToString();
                email.Subject = "Konut Tahsis " + Subject;
                System.Threading.Tasks.Task.Run(() => email.Send());
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}