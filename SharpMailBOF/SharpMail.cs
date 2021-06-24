using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Threading;

namespace SharpMail
{
    class SharpMailSend
    {
        public static string Send(string[] args)
        {
            StringWriter strWriter = new StringWriter();

            if (args.Length < 6)
            {
                strWriter.WriteLine(args.Length);
                return strWriter.ToString();
            }

            string mailserver = args[0];
            int port = Int32.Parse(args[1]);
            string mailrecipient = args[2];
            string mailfrom = args[3];
            string mailsubject = args[4];
            string mailbody = args[5];

            strWriter.WriteLine("Attempting to send email to: {0}", mailrecipient);

            try
            {
                if (args.Length == 7)
                {
                    Stream fileData = File.OpenRead(args[6]);
                    //int chunkSize = 4194304;

                    if (fileData.Length > (4194304))
                    {
                        const int BUFFER_SIZE = 4194304;
                        

                        int index = 0;
                        int bytesRead = 0;
                        byte[] buffer = new byte[BUFFER_SIZE];
                        while (fileData.Position < fileData.Length)
                        {
                            //while (remaining > 0 && (bytesRead = fileData.Read(buffer, 0, intNextChunk)) > 0)
                            while ((bytesRead = fileData.Read(buffer, 0, BUFFER_SIZE)) > 0)                            
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient(mailserver);
                                mail.From = new MailAddress(mailfrom);
                                mail.To.Add(mailrecipient);
                                mail.Subject = mailsubject;
                                mail.Body = mailbody;
                                using (MemoryStream ms = new MemoryStream(buffer, 0, bytesRead))
                                {
                                    mail.Attachments.Add(new Attachment(ms, String.Format("crash_part{0}.log", index)));
                                    SmtpServer.Port = port;
                                    strWriter.WriteLine("Sending the email.");
                                    SmtpServer.Send(mail);
                                }
                                mail.Dispose();
                                index++;
                                // Delay between emails in miliseconds
                                Thread.Sleep(600000);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strWriter.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
                return strWriter.ToString();
            }
            strWriter.WriteLine("Successfully send email.");
            return strWriter.ToString();
        }
    }
}
