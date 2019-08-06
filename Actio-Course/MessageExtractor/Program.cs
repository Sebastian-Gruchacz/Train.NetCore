using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;

namespace MessageExtractor
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string menuCommand = string.Format("\"{0}\" \"%V\"", typeof(Program).Assembly.Location);
                FileShellExtension.Register("Outlook.File.msg.15", "Extract", "Extract attachments here", menuCommand);

                return;
            }


            foreach (var s in args)
            {
                var folder = Environment.CurrentDirectory;

                var info = new FileInfo(s);
                if (info.Exists && info.Extension == ".msg")
                {
                    Application oApp = new Application();
                    MailItem mail = null;
                    try
                    {
                        mail = (MailItem) oApp.Session.OpenSharedItem(info.FullName);

                        foreach (Attachment mailAttachment in mail.Attachments)
                        {
                            string targetFile = Path.Combine(folder, mailAttachment.FileName);
                            mailAttachment.SaveAsFile(targetFile);
                        }
                    }
                    finally
                    {
                        if (mail != null)
                        {
                            Marshal.ReleaseComObject(mail);
                        }
                    }
                }



            }
        }
    }
}


