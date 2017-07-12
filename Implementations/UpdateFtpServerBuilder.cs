using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class UpdateFtpServerBuilder : IFtpServerBuilder
    {
        private readonly FtpServer _ftp;

        public UpdateFtpServerBuilder()
        {
            _ftp = new FtpServer();
        }
        public void BuildUploadMethod()
        {
            throw new System.NotImplementedException();
        }

        public void BuildCheckUpdatesMethod()
        {
            _ftp.SetCheckUpdatesMethod(new FtpGetFileInfo());
        }

        public void BuildDownloadMethod()
        {
            _ftp.SetDownloadMethod(new FtpFileDownloader());
        }

        public void BuildCheckingMethod()
        {
            throw new System.NotImplementedException();
        }

        public void BuildGetDirectoriesMethod()
        {
            throw new System.NotImplementedException();
        }

        public void BuildMakeDirectoryMethod()
        {
            throw new System.NotImplementedException();
        }

        public void BuildGetCurrentDirectoryMethod()
        {
            throw new System.NotImplementedException();
        }

        public void BuildLogin()
        {
            _ftp.SetLogin(@"mrc_1165");
        }

        public void BuildPassword()
        {
            _ftp.SetPassword(@"mrc_ftp_1165");
        }

        public void BuildAddress()
        {
            _ftp.SetAddress(@"ftp://10.7.97.20/software/registr");
        }

        public FtpServer GetFtpServer()
        {
            return _ftp;
        }
    }
}