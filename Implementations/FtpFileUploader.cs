using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpFileUploader:IFtpUpload
    {
        public string ErrorMessage { get; set; }

        public void UploadFile(string file, string ftpPath, string login, string password)
        {
            if (file == null) return;
            var uplFile = new FileInfo(file);
            try
            {
                var wRequest = (FtpWebRequest)WebRequest.Create(string.Format(@"{0}\{1}", ftpPath, uplFile.Name));
                wRequest.Proxy = null;
                wRequest.Credentials = new NetworkCredential(login, password);
                wRequest.Method = WebRequestMethods.Ftp.UploadFile;
                wRequest.UseBinary = true;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Timeout = 20000;
                wRequest.ContentLength = uplFile.Length;

                const int bSize = 2048;
                var buffer = new byte[bSize];
                using (var uplStream = uplFile.OpenRead())
                {
                    using (var ftpStream = wRequest.GetRequestStream())
                    {
                        int rBytes;
                        do
                        {
                            rBytes = uplStream.Read(buffer, 0, bSize);
                            ftpStream.Write(buffer, 0, rBytes);
                        } while (rBytes != 0);
                    }
                }
                    
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse)wEx.Response;
                var errCode = ftpResponse.StatusCode;
                if (wEx.Status == WebExceptionStatus.ProtocolError)
                {
                    ErrorMessage = string.Format("Ошибка: {0}; Файл: {1}; Размер: {2}; Сообщение: {3}", errCode,
                        Path.GetFileName(file),
                        uplFile.Length, ftpResponse.StatusDescription);
                }
            }
        }
    }
}