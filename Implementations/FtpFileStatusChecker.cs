using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpFileStatusChecker : IFtpCheckUploadingStatus
    {
        public string ErrorMessage { get; set; }
        public UploadedFileInfo FtpFileInfo { get; set; }

        public FtpCommandStatus CheckUploadingStatus(string fileName, string ftpPath, string login, string password)
        {
            try
            {
                var wRequest = (FtpWebRequest)WebRequest.Create(fileName);
                wRequest.Proxy = null;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Credentials = new NetworkCredential(_login, _password);
                wRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                using (var wResponse = wRequest.GetResponse())
                {
                    if (wResponse.ContentLength != 0)
                    {
                        result = OkResult;
                    }
                }
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse)wEx.Response;
                var errCode = ftpResponse.StatusCode;
                if (wEx.Status != WebExceptionStatus.ProtocolError) return result;
                result = errCode == FtpStatusCode.ActionNotTakenFileUnavailable ?
                    null : string.Format(@"{0}: {1}", errCode, ftpResponse.StatusDescription);
                ftpResponse.Close();
            }
            return result;

        }
    }
}