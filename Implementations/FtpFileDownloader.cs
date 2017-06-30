using System;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpFileDownloader : IFtpDownload
    {
        public string ErrorMessage
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void DownloadFile(string file, string ftpPath, string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}