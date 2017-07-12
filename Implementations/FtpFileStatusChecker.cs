using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpFileStatusChecker : IFtpCheckUploadingStatus
    {
        public string ErrorMessage { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }


        public FtpCommandStatus CheckUploadingStatus(string fileName, string ftpPath, string login, string password)
        {
            try
            {
                var wRequest = (FtpWebRequest)WebRequest.Create($@"{ftpPath}\{fileName}");
                wRequest.Proxy = null;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Credentials = new NetworkCredential(login, password);
                wRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                using (var wResponse = wRequest.GetResponse())
                {
                    if (wResponse.ContentLength != 0)
                    {
                        FileName = fileName;
                        FileSize = wResponse.ContentLength;
                    }
                }
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse)wEx.Response;
                var errCode = ftpResponse.StatusCode;
                if (wEx.Status == WebExceptionStatus.ProtocolError)
                {
                    ErrorMessage =
                        $"Ошибка: {errCode};  Сообщение: {ftpResponse.StatusDescription}";
                }

                if (errCode != FtpStatusCode.ActionNotTakenFileUnavailable) return FtpCommandStatus.NotOk;
                ErrorMessage =
                    $"Ошибка: {errCode}; Файл: {fileName}; Сообщение: {ftpResponse.StatusDescription}";
                ftpResponse.Close();
                return FtpCommandStatus.NotOk;
            }
            return FtpCommandStatus.Ok;

        }
    }
}