using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpFileDownloader : IFtpDownload
    {
        /// <summary>
        /// Класс реализует интерфейс IFtpDownload.
        /// Предназначен для скачивания файла с ftp-сервера.
        /// Метод UpdateFile в качестве первого параметра принимает путь до файла на ftp-сервере (тип string),
        /// в качестве второго - путь до файла (с указанием имени), куда мы копируем файл с ftp-сервера.
        /// Третий и четвёртый параметры метода login и password, требуемый для авторизации на ftp-сервере.
        /// Метод возвращает FtpCommandStatus. Если Ok, то ошибок не произошло, если NotOk, то при загрузке произошла ошибка
        /// и необходимо смотреть свойство ErrorMessage (обрабатывается только WebException).
        /// </summary>
        public string ErrorMessage { get; set; }

        public FtpCommandStatus DownloadFile(string what, string where, string login, string password)
        {
            try
            {
                var wRequest = (FtpWebRequest) WebRequest.Create(what);
                wRequest.Proxy = null;

                wRequest.Credentials = new NetworkCredential(login, password);
                wRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                wRequest.UseBinary = true;
                wRequest.UsePassive = true;
                wRequest.KeepAlive = false;
                wRequest.Timeout = 20000;
                using (WebResponse wResponse = wRequest.GetResponse())
                {
                    using (FileStream dFile = new FileStream(where, FileMode.Create))
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
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse) wEx.Response;
                var errCode = ftpResponse.StatusCode;
                ErrorMessage =
                    $"Ошибка: {errCode}; Сообщение: {ftpResponse.StatusDescription}";
                return FtpCommandStatus.NotOk;
            }
            return FtpCommandStatus.Ok;
        }
    }
}