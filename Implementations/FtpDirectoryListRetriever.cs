using System.Collections.Generic;
using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpDirectoryListRetriever : IFtpGetDirectories
    {
        public string ErrorMessage { get; set; }

        public List<string> GetDirectoryList(string ftpPath, string login, string password)
        {
            var result = new List<string>();
            if (ftpPath == null) return null;
            try
            {
                var wRequest = (FtpWebRequest) WebRequest.Create(ftpPath);
                wRequest.Proxy = null;
                wRequest.Credentials = new NetworkCredential(login, password);
                wRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                wRequest.UseBinary = true;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Timeout = 20000;

                using (var wResponse = wRequest.GetResponse())
                {
                    using (var responseStream = wResponse.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var dirReader = new StreamReader(responseStream))
                            {
                                while (!dirReader.EndOfStream)
                                {
                                    result.Add(dirReader.ReadLine());
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse) wEx.Response;
                var errCode = ftpResponse.StatusCode;
               ErrorMessage =
                    $"Ошибка: {errCode}; Сообщение: {ftpResponse.StatusDescription}";
                return null;
            }

            return result;
        }
    }
}