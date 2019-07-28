using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

namespace CuatroCaminosMvcApplication
{
 
/// <summary>
/// Класс отправки почты
/// </summary>
    
    public class MyMail
    {
        private MailMessage message;

        public void MyMail2()
        {
//            message = new MailAddress("");

            message = new MailMessage();


            message.From = new MailAddress("caminos@vadim.info");
            message.To.Add(new MailAddress("vadim@vkc.ru"));

            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "subject";
            message.Body = "hello receiver";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail05.parking.ru";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("as-rusov", "134288Rus");
            smtp.EnableSsl = false;


            smtp.Send(message); 

        }

        public void Mail1()
        {


            MailAddress to = new MailAddress("vadim@vkc.ru");
            MailAddress from = new MailAddress("volgvam@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            // Use the application or machine configuration to get the 
            // host, port, and credentials.
            SmtpClient client = new SmtpClient();
            //Console.WriteLine("Sending an e-mail message to {0} at {1} by using the SMTP host={2}.",
            //    to.User, to.Host, client.Host);

            //client.Credentials = new System.Net.NetworkCredential("volgvam@gmail.com", "rhvac5ud8u"); ;
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;
            //client.EnableSsl = true; 

            client.Credentials = new System.Net.NetworkCredential("caminos@vadim.info", "84$KsnXdp#"); ;
            client.Host = "mail05.parking.ru";
            client.Port = 25;
            client.EnableSsl = false; 



//            client.Send(message);
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage3(): {0}",
                      ex.ToString());
            }



        }

    }
}