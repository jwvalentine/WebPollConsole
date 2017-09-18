using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Http;

namespace WebPollConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeWebRequests();
        }


        // MakeWebRequests Constructor -- THIS WILL CALL SendMail or SendText by Time Condition
        static void MakeWebRequests()
        {

            // Time Conditions
            TimeSpan start = new TimeSpan(08, 0, 0); //8 AM
            TimeSpan end = new TimeSpan(18, 0, 0); //6 PM
            TimeSpan now = DateTime.Now.TimeOfDay;

            // Web Request & Response
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            httpReq.AllowAutoRedirect = false;
            HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();

            if (httpRes.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("An error occured. Details below:");
                Console.WriteLine(httpRes.StatusCode);
                Console.WriteLine(httpRes.StatusDescription);
                Console.WriteLine("Email Sent.");

                if (start < now && now < end)
                {
                    SendMail();
                }
                SendText();
            }
            httpRes.Close();
            Console.ReadLine();
        }

        // SendMail Constructor
        static void SendMail()
        {
            MailMessage email = new MailMessage();
            email.From = new System.Net.Mail.MailAddress("sender@whatevs.com");

            // Configure the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;   // Set SMTP Port
            smtp.EnableSsl = true; // Enable SSL
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // Delivery Method for SMTP
            smtp.UseDefaultCredentials = false; // Don't use default credentials!
            smtp.Credentials = new NetworkCredential(email.From.ToString(), "password here");  // This password is sent encrypted. Wireshark it if you want to test!
            smtp.Host = "smtp.whatevs.com"; // This is the mail server you will use as the CLIENT (think this is like Outlook)

            //Recipient address
            email.To.Add(new MailAddress("recipient@whatevs.com"));

            //Format the email body + subject
            email.IsBodyHtml = true;
            string subSt = "WebPoll Detected a Failure!";
            email.Subject = subSt;
            string bodySt = "Something went wrong!";
            email.Body = bodySt;
            smtp.Send(email);
        }

        // SendText Constructor
        static void SendText()
        {
            MailMessage email = new MailMessage();
            email.From = new System.Net.Mail.MailAddress("sender@whatevs.com");

            // Configure the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;   // Set SMTP Port
            smtp.EnableSsl = true; // Enable SSL
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // Delivery Method for SMTP
            smtp.UseDefaultCredentials = false; // Don't use default credentials!
            smtp.Credentials = new NetworkCredential(email.From.ToString(), "password here");  // This password is sent encrypted. Wireshark it if you want to test!
            smtp.Host = "smtp.whatevs.com"; // This is the mail server you will use as the CLIENT (think this is like Outlook)

            //Recipient address
            email.To.Add(new MailAddress("TELNUMBER@vtext.com"));

            //Format the email body + subject
            email.IsBodyHtml = true;
            string subSt = "WebPoll Detected a Failure!";
            email.Subject = subSt;
            string bodySt = "Something went wrong!";
            email.Body = bodySt;
            smtp.Send(email);
        }
    }
}
