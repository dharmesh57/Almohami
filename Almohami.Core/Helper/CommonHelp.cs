using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;


namespace Almohami.Core.Helper
{
    public class CommonHelp
    {
        /// <summary>
        /// Sending mail
        /// </summary>
        /// <param name="MailFrom">From email address</param>
        /// <param name="MailTo">To email address</param>
        /// <param name="MailCC">CC email address</param>
        /// <param name="MailBCC">BCC email address</param>
        /// <param name="Subject">Subject text for email</param>
        /// <param name="Body">Body text for email</param>
        /// <param name="Attachment">Attachment if any</param>
        /// <param name="IsBodyHtml">Body contain HTML code or not</param>
        /// <returns>Boolean response for sending mail</returns>
        public static bool SendMail(string MailFrom, string MailTo, string MailCC, string MailBCC, string Subject, string Body, string Attachment, bool IsBodyHtml)
        {
            MailFrom = "web@almohami.com";
            string str = string.Empty;
            System.Net.Mail.MailMessage MailMesg = new System.Net.Mail.MailMessage(MailFrom, MailTo);

            if (MailCC != string.Empty)
            {
                string[] mailCC = MailCC.Split(';');
                foreach (string email in mailCC)
                    MailMesg.CC.Add(email);
            }

            if (MailBCC != string.Empty)
            {
                MailBCC = MailBCC.Replace(";", ",");
                MailMesg.Bcc.Add(MailBCC);
            }

            if (!string.IsNullOrEmpty(Attachment))
            {
                string[] attachment = Attachment.Split(';');
                foreach (string attachFile in attachment)
                {
                    try
                    {
                        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachFile);
                        MailMesg.Attachments.Add(attach);
                    }
                    catch
                    { }
                }
            }

            MailMesg.Subject = Subject;
            MailMesg.Body = Body;
            MailMesg.IsBodyHtml = IsBodyHtml;

            System.Net.Mail.SmtpClient objSMTP = new System.Net.Mail.SmtpClient();
            //objSMTP.Host = ProjectConfiguration.SMTPGeneral.SMTPServer;
            //objSMTP.Credentials = new System.Net.NetworkCredential(ProjectConfiguration.SMTPGeneral.SMTPUser, ProjectConfiguration.SMTPGeneral.SMTPPassword);
            //objSMTP.EnableSsl = ProjectConfiguration.SMTPGeneral.SSL;

            objSMTP.Host = "mail.almohami.com";
            objSMTP.UseDefaultCredentials = true;
            objSMTP.Credentials = new System.Net.NetworkCredential("web@almohami.com", "Almohami@2017");
            objSMTP.EnableSsl = true;
            objSMTP.Port = 25;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
    delegate(object s, X509Certificate certificate,
             X509Chain chain, SslPolicyErrors sslPolicyErrors)
    { return true; };
                objSMTP.Send(MailMesg);
                return true;
            }
            catch (System.Exception ex)
            {
                str = ex.Message;
                MailMesg.Dispose();
                MailMesg = null;
            }
            return false;
        }

        /// <summary>
        /// GetUserPassword
        /// GetRandomNo
        /// </summary>
        /// <returns></returns>
        public static string GetRandomCode(int len)
        {
            string allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";

            char[] sep = { ',' };

            string[] arr = allowedChars.Split(sep);

            string passwordString = "";

            var rand = new Random();

            for (int i = 0; i < len-1; i++)
            {
                string temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        // This method helps to get the error information from the MVC "ModelState".
        // We can not directly send the ModelState to the client in Json. The "ModelState"
        // object has some circular reference that prevents it to be serialized to Json.


        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string GenerateRandomCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public static string GetVerificationCode()
        {

            string allowedChars = "1,2,3,4,5,6,7,8,9,0";

            char[] sep = { ',' };

            string[] arr = allowedChars.Split(sep);

            string passwordString = "";

            var rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                string temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }

        public static string Pass, FromEmailid, HostAdd;
        public static void Email_Without_Attachment(String ToEmail, String Subj, string Message)
        {
            
            //Reading sender Email credential from web.config file
            HostAdd = "mail.almohami.com";
            FromEmailid = "web@almohami.com";
            Pass = "Almohami@2017";

            //creating the object of MailMessage
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailid); //From Email Id
            mailMessage.Subject = Subj; //Subject of Email
            mailMessage.Body = Message; //body or message of Email
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(ToEmail));
            //Adding Multiple recipient email id logic

            //string[] Multi = ToEmail.TrimEnd(',').Split(','); //spiliting input Email id string with comma(,)
            //foreach (string Multiemailid in Multi)
            //{
            //    mailMessage.To.Add(new MailAddress(Multiemailid)); //adding multi reciver's Email Id
            //}
            SmtpClient smtp = new SmtpClient(); // creating object of smptpclient
            smtp.Host = HostAdd; //host of emailaddress for example smtp.gmail.com etc

            //network and security related credentials
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = Pass;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 25;
            try
            {
                smtp.Send(mailMessage); //sending Email
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public static class Enums
    {
        /// Get all values
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// Get all the names
        public static IEnumerable<T> GetNames<T>()
        {
            return Enum.GetNames(typeof(T)).Cast<T>();
        }

        /// Get the name for the enum value
        public static string GetName<T>(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        /// Get the underlying value for the Enum string
        public static int GetValue<T>(string enumString)
        {
            return (int)Enum.Parse(typeof(T), enumString.Trim());
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }

}