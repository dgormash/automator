using System;
using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpUploader:IFtpUploader
    {
        
        public UploadInfo UploadFiles(string[] files)
        {
            var result = new UploadInfo();
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName == null) continue;
                if ((fileName.StartsWith(@"a") || fileName.StartsWith(@"f")))
                {
                    //todo Создание экземпляра ftp-сервера для ИЦ
                }

                if (fileName.StartsWith(@"v"))
                {
                    
                }
            }
            return result;
        }
    }
}