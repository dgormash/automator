using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutomatorPrg
{
    class FtpManager
    {
        private readonly string _login;
        private readonly string _password;

        public string ErrorrMessage { get; private set; }

        public FtpManager(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public bool DownloadFileFromFtp(string fileName)
        {
            bool result = false;
            try
            {
                var wRequest = (FtpWebRequest)WebRequest.Create(fileName);
                wRequest.Proxy = null;
               
                wRequest.Credentials = new NetworkCredential(_login, _password);
                wRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                wRequest.UseBinary = true;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Timeout = 20000;
                using (WebResponse wResponse = wRequest.GetResponse())
                {
                    using (FileStream dFile = new FileStream(string.Format(@"{0}\update\{1}",
                       Directory.GetCurrentDirectory(), Path.GetFileName(fileName)), FileMode.Create))
                    {
                        using (Stream rStream = wResponse.GetResponseStream())
                        {
                            const int bSize = 2048;
                            byte[] buffer = new byte[bSize];
                            if (rStream != null)
                            {
                                var readCount = rStream.Read(buffer, 0, bSize);
                                while (readCount > 0)
                                {
                                    dFile.Write(buffer, 0, readCount);
                                    readCount = rStream.Read(buffer, 0, bSize);
                                }
                            }
                        }
                    }
                }
                result = true;

                try
                {
                    var delRequest = (FtpWebRequest) WebRequest.Create(fileName);
                    delRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    delRequest.Credentials = new NetworkCredential(_login, _password);
                    delRequest.UsePassive = true;
                    delRequest.KeepAlive = false;
                    WebResponse resp = delRequest.GetResponse();
                    resp.Close();
                }
                catch (WebException delEx)
                {
                    var ftpResponse = (FtpWebResponse)delEx.Response;
                    FtpStatusCode errCode = ftpResponse.StatusCode;
                    if (delEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(@"Ошибка: {0}", errCode);
                        Console.WriteLine(@"Удаляемый файл: {0}", Path.GetFileName(fileName));
                        Console.WriteLine(@"Ответ: {0}", ftpResponse.StatusDescription);
                        Console.ResetColor();
                    }
                }
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse)wEx.Response;
                FtpStatusCode errCode = ftpResponse.StatusCode;
                if (wEx.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@"Ошибка: {0}", errCode);
                    Console.WriteLine(@"Запрашиваемый файл: {0}", Path.GetFileName(fileName));
                    Console.WriteLine(@"Ответ: {0}", ftpResponse.StatusDescription);
                    Console.ResetColor();
                }
            }
            return result;
        }

        public bool UploadFileToFtp(string fileName, string ftpPath)
        {
            bool result = false;
            if (fileName != null)
            {
                var uplFile = new FileInfo(fileName);
                try
                {
                    var wRequest = (FtpWebRequest) WebRequest.Create(string.Format(@"{0}\{1}", ftpPath, uplFile.Name));
                    wRequest.Proxy = null;
                    wRequest.Credentials = new NetworkCredential(_login, _password);
                    wRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    wRequest.UseBinary = true;
                    wRequest.UsePassive = true;
                    wRequest.KeepAlive = false;
                    wRequest.Timeout = 20000;
                    wRequest.ContentLength = uplFile.Length;

                    const int bSize = 2048;
                    byte[] buffer = new byte[bSize];
                    using (FileStream uplStream = uplFile.OpenRead())
                    {
                        using (Stream ftpStream = wRequest.GetRequestStream())
                        {
                            int rBytes;
                            do
                            {
                                rBytes = uplStream.Read(buffer, 0, bSize);
                                ftpStream.Write(buffer, 0, rBytes);
                            } while (rBytes != 0);
                        }
                    } 
                    result = true;
                }
                catch (WebException wEx)
                {
                    var ftpResponse = (FtpWebResponse) wEx.Response;
                    FtpStatusCode errCode = ftpResponse.StatusCode;
                    if (wEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(@"Ошибка: {0}", errCode);
                        Console.WriteLine(@"Записываемый файл: {0}", Path.GetFileName(fileName));
                        Console.WriteLine(@"Ответ: {0}", ftpResponse.StatusDescription);
                        Console.ResetColor();
                        ErrorrMessage = string.Format("Ошибка: {0}; Файл: {1}; Размер: {2}; Сообщение: {3}", errCode,
                            Path.GetFileName(fileName),
                            uplFile.Length, ftpResponse.StatusDescription);
                    }
                }
            }

        return result;
        }

        public bool CheckUpdate(string fileName)
        {
            bool result = false;
            try
            {
                var wRequest = (FtpWebRequest) WebRequest.Create(fileName);
                wRequest.Proxy = null;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Credentials = new NetworkCredential(_login, _password);
                wRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                //wRequest.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                using (WebResponse wResponse = wRequest.GetResponse())
                {
                    if (wResponse.ContentLength != 0)
                    {
                        result = true;
                    }              
                }
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse) wEx.Response;
                    FtpStatusCode errCode = ftpResponse.StatusCode;
                if (wEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (errCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(@"Обновлений не обнаружено.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(@"Ошибка: {0}", errCode);
                        Console.WriteLine(@"Запрашиваемый файл: {0}", Path.GetFileName(fileName));
                        Console.WriteLine(@"Ответ: {0}", ftpResponse.StatusDescription);
                        Console.ResetColor();
                    }
                    ftpResponse.Close();
                }
            }
        return result;
        }
    }
}
