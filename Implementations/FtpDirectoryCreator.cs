using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpDirectoryCreator : IFtpMakeDirectory
    {
        public string ErrorMessage { get; set; }
        public FtpCommandStatus CreateDirectoryOnServer(string ftpPath, string login, string password)
        {
            if (ftpPath == null)
            {
                ErrorMessage = @"Не задан путь до ftp-сервера";
                return FtpCommandStatus.NotOk;
            }
            try
            {
                var wRequest = (FtpWebRequest)WebRequest.Create(ftpPath);
                wRequest.Proxy = null;
                wRequest.Credentials = new NetworkCredential(login, password);
                wRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                wRequest.UseBinary = true;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Timeout = 20000;

                wRequest.GetResponse();
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse)wEx.Response;
                var errCode = ftpResponse.StatusCode;
                if (wEx.Status != WebExceptionStatus.ProtocolError)
                ErrorMessage =
                    $"Ошибка: {errCode}; Сообщение: {ftpResponse.StatusDescription}";
                return FtpCommandStatus.NotOk;
            }

            return FtpCommandStatus.Ok;
        }
    }
}