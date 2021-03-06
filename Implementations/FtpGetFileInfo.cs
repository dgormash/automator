﻿using System;
using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpGetFileInfo:IFtpGetFileInfo
    {
        public string ErrorMessage { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public FtpCommandStatus GetFileInfo(string ftpPath, string login, string password)
        {
            var wRequest = (FtpWebRequest)WebRequest.Create(ftpPath);
            wRequest.Proxy = null;
            wRequest.UsePassive = true;
            wRequest.KeepAlive = false;
            wRequest.Credentials = new NetworkCredential(login, password);
            wRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            try
            {
                using (var wResponse = (FtpWebResponse)wRequest.GetResponse())
                {
                    if (wResponse.ContentLength != -1)
                    {
                        Name = Path.GetFileName(ftpPath);
                        LastModified = wResponse.LastModified;
                    }
                }
            }
            catch (WebException wEx)
            {
                var ftpResponse = (FtpWebResponse)wEx.Response;
                var errCode = ftpResponse.StatusCode;
                ErrorMessage =
                    $"Ошибка: {errCode}; Сообщение: {ftpResponse.StatusDescription}";
                return FtpCommandStatus.NotOk;
            }
            return FtpCommandStatus.Ok;
        }
    }
}