using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpFileUploader:IFtpUpload
    {
        public string ErrorMessage { get; set; }
        public FtpCommandStatus UploadFile(string file, string ftpPath, string login, string password)
        {
            if (file == null)
            {
                ErrorMessage = @"class: FtpFileUploader; Возможно файл, который вы хотите выгрузить на ftp не существует или путь к нему задан не правильно...";
                return FtpCommandStatus.NotOk;
            }
            var uplFile = new FileInfo(file);
            try
            {
                var wRequest = (FtpWebRequest)WebRequest.Create($@"{ftpPath}\{uplFile.Name}");
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
                    ErrorMessage =
                        $"Ошибка: {errCode}; Файл: {Path.GetFileName(file)}; Размер: {uplFile.Length}; Сообщение: {ftpResponse.StatusDescription}";
                    return FtpCommandStatus.NotOk;
                }
            }
            return FtpCommandStatus.Ok;
        }
    }
}