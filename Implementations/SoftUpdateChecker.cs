using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class SoftUpdateChecker:ISoftUpdateCheck
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsFtpFileNewerThanLocalFile(FileInfo localFile, string ftpFile)
        {
           
            var wRequest = (FtpWebRequest)WebRequest.Create(ftpFile);
            wRequest.Proxy = null;
            wRequest.UsePassive = true;
            wRequest.KeepAlive = false;
            wRequest.Credentials = new NetworkCredential(Login, Password);
            wRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            try
            {
                using (var wResponse = wRequest.GetResponse())
                {
                    if (wResponse.ContentLength != 0)
                    {
                        //wResponse
                    }
                }
            }
            catch (WebException e)
            {
                //var ftpResponse = (FtpWebResponse)wEx.Response;
                //FtpStatusCode errCode = ftpResponse.StatusCode;
                //if (wEx.Status == WebExceptionStatus.ProtocolError)
                //{
                //    if (errCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Yellow;
                //        Console.WriteLine(@"Обновлений не обнаружено.");
                //        Console.ResetColor();
                //    }
                //    else
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.WriteLine(@"Ошибка: {0}", errCode);
                //        Console.WriteLine(@"Запрашиваемый файл: {0}", Path.GetFileName(fileName));
                //        Console.WriteLine(@"Ответ: {0}", ftpResponse.StatusDescription);
                //        Console.ResetColor();
                //    }
                //    ftpResponse.Close();
                //}
            }
            return false;
        }
    }
}